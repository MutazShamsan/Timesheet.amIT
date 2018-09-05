using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Models
{
    public class DateTimeSheetModel
    {
        public DateTime CurrentDay;
        public Type DataType;
        public object Value;

        public DateTimeSheetModel(DateTime currentDay, Type dataType, object value)
        {
            CurrentDay = currentDay;
            DataType = dataType;
            Value = value;
        }
    }
}
