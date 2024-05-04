using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.OModels
{
    public class TempExpertDetails
    {
    }
    public partial class TempExpertAvailibity
    {
        public string ExpertId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int Experience { get; set; }
        public int Cost { get; set; }
        public string TimeZone { get; set; }

    }
}
