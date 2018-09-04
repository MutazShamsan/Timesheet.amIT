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

namespace Timesheet.HolidayProvider
{
    public class GooCaleApi
    {
        //C:\Users\mutazm-c\source\repos\Timesheet.amIT\Timesheet.amIT\bin\Debug\credentials.json
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public void Test()
        {
            UserCredential credential;

            using (var stream = new FileStream(@"C:\Users\mutazm-c\source\repos\Timesheet.amIT\Timesheet.amIT\bin\Debug\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("en.malaysia#holiday@group.v.calendar.google.com");
            request.TimeMin = new DateTime(2018, 1, 1);
            request.TimeMax = new DateTime(2018, 12, 31);

            request.ShowDeleted = false;
            request.SingleEvents = true;
            //request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();
        }
    }
}
