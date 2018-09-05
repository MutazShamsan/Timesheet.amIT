using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.HolidayProvider
{
    public class GoogleProvider : HolidayProviderBase
    {
        private readonly string[] _scopes = { CalendarService.Scope.CalendarReadonly };
        private readonly string _applicationName = "Google Calendar API .NET Quickstart";
        private readonly string _calenderId = "en.malaysia#holiday@group.v.calendar.google.com";
        private readonly string _credPath = "token.json";

        public override async Task<IEnumerable<HolidayModel>> GetHolidays(string country, string state, Stream file = null)
        {
            var result = new List<HolidayModel>();
            try
            {
                using (var service = await InitializeApiService(file))
                {
                    var request = SetupSearchCriteria(service);
                    var tmpResult = request.Execute();
                    result = FilterResult(tmpResult, state);
                }
            }
            catch (Exception ex)
            { }

            return result;
        }

        private async Task<CalendarService> InitializeApiService(Stream file)
        {
            UserCredential credential;

            using (file)
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(file).Secrets, _scopes, "user",
                    CancellationToken.None, new FileDataStore(_credPath, true));
            }

            // Create Google Calendar API service.
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        private EventsResource.ListRequest SetupSearchCriteria(CalendarService service)
        {
            var request = service.Events.List(_calenderId);
            request.TimeMin = StartDate.DateTime;
            request.TimeMax = EndDate.DateTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            return request;
        }

        private List<HolidayModel> FilterResult(Events events, string state)
        {
            List<HolidayModel> result = new List<HolidayModel>();
            DateTimeOffset holidayDate;

            if (events?.Items?.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    holidayDate = DateTimeOffset.Parse(eventItem.Start.Date);
                    if (eventItem.Summary.Contains("Valen") || eventItem.Summary.Contains("Easter Sunday") || holidayDate.DayOfWeek == DayOfWeek.Saturday)
                    { }
                    else
                    {
                        if (string.IsNullOrEmpty(eventItem.Description) || eventItem.Description.Contains(state))
                            result.Add(new HolidayModel() { Date = holidayDate, Name = eventItem.Summary });
                    }
                }
            }

            return result;
        }
    }
}
