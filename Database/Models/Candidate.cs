using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            InterviewSchedules = new HashSet<InterviewSchedule>();
            Pvaschedules = new HashSet<Pvaschedule>();
        }

        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
        public string ResumePath { get; set; }
        public string GovtIdproofType { get; set; }
        public string GovtIdproofVal { get; set; }
        public int? Status { get; set; }
        public string Notes { get; set; }
        public string Ctc { get; set; }
        public string Ectc { get; set; }
        public string NoticePeriod { get; set; }
        public decimal? Exp { get; set; }
        public decimal? RelExp { get; set; }
        public string CurrentLocation { get; set; }
        public string PreferredLocation { get; set; }
        public string CurrentCompany { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? CvreceivedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? AvailableTime { get; set; }
        public int? VendorId { get; set; }
        public string Portfolio { get; set; }
        public string Owner { get; set; }
        public string HiringType { get; set; }
        public string AssignedTo { get; set; }
        public string TimeZone { get; set; }
        public bool IsExpert { get; set; }
        public int? PvaId { get; set; }
        public int? RanqitId { get; set; }

        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; }
        public virtual ICollection<Pvaschedule> Pvaschedules { get; set; }
    }
}
