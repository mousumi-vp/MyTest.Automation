using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Spocavailability
    {
        public int Id { get; set; }
        public int SpocId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public bool? OnLeave { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
