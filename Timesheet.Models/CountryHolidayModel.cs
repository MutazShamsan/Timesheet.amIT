using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Models
{
    public class CountryHolidayModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
        IEnumerable<HolidayModel> Holidays { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
