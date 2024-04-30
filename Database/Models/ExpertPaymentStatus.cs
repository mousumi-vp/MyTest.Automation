using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertPaymentStatus
    {
        public int Id { get; set; }
        public string TransferId { get; set; }
        public string Name { get; set; }
        public string Vpa { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Amount { get; set; }
        public string Remarks { get; set; }
        public string InterviewsIds { get; set; }
        public string BounsIds { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PaymentOn { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentRemarks { get; set; }
        public string PaidBy { get; set; }
        public string ValidationMsg { get; set; }
        public string ReferralIds { get; set; }
        public string CalibrationIds { get; set; }
    }
}
