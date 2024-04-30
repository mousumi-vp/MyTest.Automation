using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerDataDelConfigs = new HashSet<CustomerDataDelConfig>();
            JobRequirements = new HashSet<JobRequirement>();
            PreScreenCandidateMatches = new HashSet<PreScreenCandidateMatch>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string ContactPerson { get; set; }
        public int TypeId { get; set; }
        public string CompanyInfo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public bool? OutOfService { get; set; }
        public string AccountManger { get; set; }
        public string Notes { get; set; }
        public string BillingCycle { get; set; }
        public string PaymentOption { get; set; }
        public int? BillingRate { get; set; }
        public DateTime? BillingRateFrom { get; set; }
        public int? Subscription { get; set; }
        public string AccountLead { get; set; }
        public int Category { get; set; }
        public string Url { get; set; }
        public string EmailVericationCode { get; set; }
        public bool? IsVerified { get; set; }
        public int MaxUser { get; set; }
        public DateTime? AgreementDate { get; set; }
        public bool IsEmailConfigured { get; set; }
        public bool IsPvaSubscribed { get; set; }
        public int? PvabillingRate { get; set; }
        public bool? IsRanqitSubscribed { get; set; }
        public int? RanqitBillingRate { get; set; }
        public bool? IsTechnicalRoundSubscribed { get; set; }
        public bool IsApienabled { get; set; }
        public Guid? Code { get; set; }

        public virtual UserType Type { get; set; }
        public virtual ICollection<CustomerDataDelConfig> CustomerDataDelConfigs { get; set; }
        public virtual ICollection<JobRequirement> JobRequirements { get; set; }
        public virtual ICollection<PreScreenCandidateMatch> PreScreenCandidateMatches { get; set; }
    }
}
