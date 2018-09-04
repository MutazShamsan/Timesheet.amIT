using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
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
            using (var service = await InitializeApiService(file))
            {
                var request = SetupSearchCriteria(service);
                var result = await request.ExecuteAsync();
                FilterResult(result);
            }

            return new List<HolidayModel>();
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

        private List<HolidayModel> FilterResult(Events events)
        {
            List<HolidayModel> result = new List<HolidayModel>();

            if (events?.Items?.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    if (eventItem.Summary.Contains("Valen"))
                    { }
                    else
                    {
                        result.Add(new HolidayModel() { Date = DateTimeOffset.Parse(eventItem.Start.Date), Name = eventItem.Summary } );
                    }
                }
            }
            

            return result;

        }
    }
}
