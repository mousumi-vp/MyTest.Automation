using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Expert
    {
        public Expert()
        {
            ExpertDomains = new HashSet<ExpertDomain>();
            ExpertSkills = new HashSet<ExpertSkill>();
            InterviewSchedules = new HashSet<InterviewSchedule>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PwdSalt { get; set; }
        public string Password { get; set; }
        public int StatusId { get; set; }
        public string ResumeId { get; set; }
        public string ResumeExt { get; set; }
        public string ProfileSummary { get; set; }
        public int? DomainId { get; set; }
        public int? YrsExp { get; set; }
        public string LinkedInUrl { get; set; }
        public bool IsAgreedTerms { get; set; }
        public bool? IsEmailVerified { get; set; }
        public Guid EmailCode { get; set; }
        public string Postion { get; set; }
        public int? Rating { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Phone { get; set; }
        public string UpiAddress { get; set; }
        public string CreatedBy { get; set; }
        public int? CurrentRate { get; set; }
        public string CountryCode { get; set; }
        public string WhatsAppNo { get; set; }
        public string BlockReason { get; set; }
        public string TimeZone { get; set; }
        public string PwdResetCode { get; set; }
        public bool? IsResetPwd { get; set; }
        public string Notes { get; set; }
        public int Category { get; set; }
        public string PrimarySkill { get; set; }
        public DateTime? BlockedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastLogin { get; set; }
        public string PseudoName { get; set; }

        public virtual ExpertStatus Status { get; set; }
        public virtual ICollection<ExpertDomain> ExpertDomains { get; set; }
        public virtual ICollection<ExpertSkill> ExpertSkills { get; set; }
        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; }
    }
}
