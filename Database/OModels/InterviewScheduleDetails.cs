using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.OModels
{
    public class InterviewScheduleDetails
    {
    }
    public class Data
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int New { get; set; }
        public int Rescheduled { get; set; }
    }
    public class TimeInterval
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }


    }
}
