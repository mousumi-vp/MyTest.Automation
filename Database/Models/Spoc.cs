using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Spoc
    {
        public Spoc()
        {
            InterviewSchedules = new HashSet<InterviewSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MeetingLink { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; }
    }
}
