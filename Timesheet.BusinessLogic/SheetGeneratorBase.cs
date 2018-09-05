using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Interfaces;
using Timesheet.Models;

namespace Timesheet.BusinessLogic
{
    public abstract class SheetGeneratorBase : ISheetGenerator
    {
        protected IHolidayProvider HolidayApi { get; set; }
        public List<HolidayModel> Holidays { get; set; }
        protected DateTime StartPeriod { get; set; }
        protected DateTime EndPeriod { get; set; }

        public SheetGeneratorBase(IHolidayProvider holidayApi, DateTime? start, DateTime? end)
        {
            StartPeriod = start.GetValueOrDefault();
            EndPeriod = end.GetValueOrDefault();
            HolidayApi = holidayApi;
            HolidayApi.StartIn(StartPeriod).EndIn(EndPeriod);
        }

        protected async Task GetHolidays()
        {
            Holidays = (await HolidayApi.GetHolidays("", ""))?.ToList();
        }

        public abstract void Start(string fileName);
    }
}
