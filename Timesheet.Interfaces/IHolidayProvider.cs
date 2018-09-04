using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.Interfaces
{
    public interface IHolidayProvider
    {
        IHolidayProvider StartIn(DateTimeOffset startDate);
        IHolidayProvider EndIn(DateTimeOffset endDate);
        Task<IEnumerable<HolidayModel>> GetHolidays(string country, string state, Stream file = null);
    }
}
