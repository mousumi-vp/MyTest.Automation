using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class JobRequirement
    {
        public JobRequirement()
        {
            JobSkills = new HashSet<JobSkill>();
            PreScreeningJobs = new HashSet<PreScreeningJob>();
        }

        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string EndCustomer { get; set; }
        public int DomainId { get; set; }
        public bool IsAskExpert { get; set; }
        public bool IsPsychometric { get; set; }
        public bool OneWayVideo { get; set; }
        public bool FullVideo { get; set; }
        public int? CountofCvs { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Referral { get; set; }
        public bool? IsCvsourceing { get; set; }
        public bool? IsCandidateEmail { get; set; }
        public string CreatedBy { get; set; }
        public string Hremail { get; set; }
        public string Remarks { get; set; }
        public bool? IsCandidateAvailablity { get; set; }
        public string NoteToQuality { get; set; }
        public string Questions { get; set; }
        public bool? IsCodingTest { get; set; }
        public int? PositionCount { get; set; }
        public int? Budget { get; set; }
        public int JobType { get; set; }
        public int? NoOfChallanges { get; set; }
        public bool? IsInstructionUpload { get; set; }
        public string Notes { get; set; }
        public string Department { get; set; }
        public string Round { get; set; }
        public string ReferenceCode { get; set; }
        public string CuratedJdText { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Domain Domain { get; set; }
        public virtual ICollection<JobSkill> JobSkills { get; set; }
        public virtual ICollection<PreScreeningJob> PreScreeningJobs { get; set; }
    }
}
