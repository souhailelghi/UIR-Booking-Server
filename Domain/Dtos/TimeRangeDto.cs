using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class TimeRangeDto
    {
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }
    }
}
