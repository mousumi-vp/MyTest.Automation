using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerEmailConfig
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string EmailType { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public bool IsActive { get; set; }
        public int PortNo { get; set; }
        public bool EnableSsl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
