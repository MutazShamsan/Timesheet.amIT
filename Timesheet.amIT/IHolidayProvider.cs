using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.amIT
{
    public interface IHolidayProvider
    {
        Task<IEnumerable<HolidayModel>> GetHolidays(string country, string state, Stream file = null);
    }
}
