using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PvajobConfiguration
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Mcq { get; set; }
        public int Descriptive { get; set; }
        public int Coding { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int InterviewDuration { get; set; }
        public string Notes { get; set; }
        public int? Validity { get; set; }
        public int Status { get; set; }
    }
}
