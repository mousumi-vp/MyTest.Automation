using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertBonu
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string Description { get; set; }
        public int? BonusAmount { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaidBy { get; set; }
        public string BonusType { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
