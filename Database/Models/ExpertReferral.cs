using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertReferral
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public double? YrsExp { get; set; }
        public string SkillSet { get; set; }
        public bool? CanExpertNameBeUsed { get; set; }
        public bool? IsOnboarded { get; set; }
        public int? ReferalAmount { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public DateTime? ReferredOn { get; set; }
        public string Comments { get; set; }
        public string ResumeId { get; set; }
    }
}
