using OfficeOpenXml;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Timesheet.BusinessLogic;

namespace Timesheet.amIT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private List<Models.HolidayModel> holidays = new List<Models.HolidayModel>();
        DateTime start, end;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            //GooCaleApi sss = new GooCaleApi();
            //sss.Test();

            start = new DateTime(2018, 9, 1);
            end = new DateTime(2018, 9, 30);

            Interfaces.IHolidayProvider ss = new HolidayProvider.GoogleProvider();
            ss.StartIn(new DateTime(2018, 1, 1)).EndIn(new DateTime(2018, 12, 31));
            holidays = ss.GetHolidays("My", "Kuala", ResourceManagement.GetResourceFileStream("Timesheet.amIT.credentials.json")).Result.ToList();

            Populate();
        }

        private void PopulateDateToSheet(DateTime currentDay, int columnIndex, ExcelWorksheet workSheet)
        {
            var cell = workSheet.Cells[9, columnIndex];
            cell.Value = currentDay.Day;
        }

        private void PopulateDateCapToSheet(DateTime currentDay, int columnIndex, ExcelWorksheet workSheet)
        {
            var cell = workSheet.Cells[11, columnIndex];
            DateTimeSheet ss;// = new DateTimeSheet(currentDay, typeof(int), "5");

            switch (currentDay.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    ss = new DateTimeSheet(currentDay, typeof(string), "SA");
                    //cell.Value = "SA";
                    break;
                case DayOfWeek.Sunday:
                    ss = new DateTimeSheet(currentDay, typeof(string), "SU");
                    //cell.Value = "SU";
                    break;
                default:
                    if (holidays.Any(st => st.Date.Date == currentDay.Date))
                        ss = new DateTimeSheet(currentDay, typeof(string), "H");
                    else
                        ss = new DateTimeSheet(currentDay, typeof(int), 8);
                    break;
            }

            cell.Value = ss.Value;

        }

        private void Populate()
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MY_amIT_Timesheet.xlsx"))))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var columnIndex = 2;

                while (start <= end)
                {
                    PopulateDateToSheet(start, columnIndex, worksheet);
                    PopulateDateCapToSheet(start, columnIndex, worksheet);

                    start = start.AddDays(1);
                    columnIndex++;
                }
                worksheet.Calculate();
                package.Save();
            }
        }
    }
}
