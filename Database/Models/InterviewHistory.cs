using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class InterviewHistory
    {
        public int Id { get; set; }
        public int InterviewId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        public string ExpertId { get; set; }
        public int? StatusId { get; set; }
        public int? SpocId { get; set; }
    }
}
