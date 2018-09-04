using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheet.amIT
{
    public class GoogleCalenderHolidayProvider : HolidayProviderBase
    {
        string[] _scopes = { CalendarService.Scope.CalendarReadonly };
        string _applicationName = "Google Calendar API .NET Quickstart";
        string _calenderId = "en.malaysia#holiday@group.v.calendar.google.com";

        public IEnumerable<string> Scopes { get; private set; }

        public override async Task<IEnumerable<HolidayModel>> GetHolidays(string country, string state)
        {
            using (var service = await InitializeApiService())
            {
                var request = SetupSearchCriteria(service, DateTime.Now, DateTime.Now);
                var result = await request.ExecuteAsync();
                FilterResult(result);
            }

            return new List<HolidayModel>();
        }

        private async Task<CalendarService> InitializeApiService()
        {
            UserCredential credential;

            using (var stream = new FileStream(@"C:\Users\mutazm-c\source\repos\Timesheet.amIT\Timesheet.amIT\bin\Debug\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });

            return service;
        }

        private EventsResource.ListRequest SetupSearchCriteria(CalendarService service, DateTime startDate, DateTime endDate)
        {
            var request = service.Events.List(_calenderId);
            request.TimeMin = startDate;
            request.TimeMax = endDate;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return request;
        }

        private void FilterResult(Events events)
        {

        }
    }
}
