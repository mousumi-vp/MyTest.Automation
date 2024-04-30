using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerExpertCost
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string Timezone { get; set; }
        public int? Cost { get; set; }
        public int? CurrencyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
