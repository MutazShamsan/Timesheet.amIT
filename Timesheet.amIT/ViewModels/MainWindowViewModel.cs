using OfficeOpenXml;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace Timesheet.amIT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private List<DateTime> weekends = new List<DateTime>();
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

            //start = new DateTime(2018, 9, 1);
            //end = new DateTime(2018, 9, 30);

            Populate();
        }

        private void AssignWeekendDays(DateTime start, DateTime end)
        {
            while (start <= end)
            {
                switch (start.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        weekends.Add(start);
                        break;
                }
                start = start.AddDays(1);

            }
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
                    ss = new DateTimeSheet(currentDay, typeof(int), 8);
                    break;
            }

            cell.Value = ss.Value;

        }

        private void Populate()
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Users\mutazm-c\source\repos\Timesheet.amIT\Timesheet.amIT\bin\Debug\MY_amIT_Timesheet.xlsx")))
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
