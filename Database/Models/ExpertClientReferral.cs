using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertClientReferral
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string ClientName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ClientWebsite { get; set; }
        public bool? IsOnBoarded { get; set; }
        public DateTime? ReferredOn { get; set; }
        public int? ReferalAmount { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public string Comments { get; set; }
    }
}
