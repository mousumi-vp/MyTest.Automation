using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertAvailability
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int? JobId { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
    }
}
