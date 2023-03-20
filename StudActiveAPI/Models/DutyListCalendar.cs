using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudActiveAPI.Models
{
    public class DutyListCalendar
    {
        public Guid DutyListCalendarId { get; set; }
        public string CreatorFio { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
