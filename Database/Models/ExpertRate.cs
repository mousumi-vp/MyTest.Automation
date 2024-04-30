using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class ExpertRate
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string Timezone { get; set; }
        public int Rate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int? CustomerId { get; set; }
    }
}
