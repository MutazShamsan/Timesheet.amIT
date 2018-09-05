using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.amIT
{
    public abstract class HolidayProviderBase : IHolidayProvider
    {
        protected string ApiUrl { get; set; }
        public abstract Task<IEnumerable<HolidayModel>> GetHolidays(string country, string state, Stream file = null);
    }
}
