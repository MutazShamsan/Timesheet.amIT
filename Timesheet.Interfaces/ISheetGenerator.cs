using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Interfaces;

namespace Timesheet.Interfaces
{
    public interface ISheetGenerator
    {
        Task Start(string fileName);
    }
}
