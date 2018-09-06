using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Interfaces;
using Timesheet.Models;

namespace Timesheet.BusinessLogic
{
    public class AmItSheetGenerator : SheetGeneratorBase
    {
        public AmItSheetGenerator(IHolidayProvider holidayApi, DateTime? start, DateTime? end) : base(holidayApi, start, end)
        { }

        public override async Task Start(string fileName)
        {
            await GetHolidays();

            using (var package = new ExcelPackage(new System.IO.FileInfo(fileName)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var columnIndex = 2;

                while (StartPeriod <= EndPeriod)
                {
                    PopulateDateToSheet(StartPeriod, columnIndex, worksheet);
                    PopulateDateCapToSheet(StartPeriod, columnIndex, worksheet);

                    StartPeriod = StartPeriod.AddDays(1);
                    columnIndex++;
                }
                worksheet.Calculate();
                package.Save();
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
            DateTimeSheetModel ss;

            switch (currentDay.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    ss = new DateTimeSheetModel(currentDay, typeof(string), "SA");
                    //cell.Value = "SA";
                    break;
                case DayOfWeek.Sunday:
                    ss = new DateTimeSheetModel(currentDay, typeof(string), "SU");
                    //cell.Value = "SU";
                    break;
                default:
                    if (Holidays.Any(st => st.Date.Date == currentDay.Date))
                        ss = new DateTimeSheetModel(currentDay, typeof(string), "H");
                    else
                        ss = new DateTimeSheetModel(currentDay, typeof(int), 8);
                    break;
            }

            cell.Value = ss.Value;
        }
    }
}
