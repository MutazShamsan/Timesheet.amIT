using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.amIT
{
    public class DateTimeSheet
    {
        public DateTime CurrentDay;
        public Type DataType;
        public object Value;

        public DateTimeSheet(DateTime currentDay, Type dataType, object value)
        {
            CurrentDay = currentDay;
            DataType = dataType;
            Value = value;
        }
    }
}
