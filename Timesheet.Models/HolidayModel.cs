using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Models
{
    public class HolidayModel
    {
        public string Name { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
