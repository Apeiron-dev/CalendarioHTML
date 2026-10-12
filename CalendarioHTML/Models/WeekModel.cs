using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioHTML.Models
{
    public class WeekModel
    {

        public List<DayCell> Days { get; set; } = [];
        public bool IsCompact { get; set; } = false;
    }
}
