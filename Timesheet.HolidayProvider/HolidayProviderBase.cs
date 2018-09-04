using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Timesheet.Interfaces;
using Timesheet.Models;

namespace Timesheet.HolidayProvider
{
    public abstract class HolidayProviderBase : IHolidayProvider
    {
        protected DateTimeOffset StartDate { get; set; }
        protected DateTimeOffset EndDate { get; set; }
        public virtual IHolidayProvider StartIn(DateTimeOffset startDate)
        {
            StartDate = startDate;
            return this;
        }
        public virtual IHolidayProvider EndIn(DateTimeOffset endDate)
        {
            EndDate = endDate;
            return this;
        }

        public abstract Task<IEnumerable<HolidayModel>> GetHolidays(string country, string state, Stream file = null);
    }
}
