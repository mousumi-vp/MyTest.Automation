using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertFeedback
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int? Feedback { get; set; }
        public DateTime? SubmittedOn { get; set; }
    }
}
