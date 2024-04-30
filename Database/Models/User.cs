using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PwdSalt { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? CustomerId { get; set; }
        public string ImagePath { get; set; }
        public string PwdResetCode { get; set; }
        public bool? IsResetPwd { get; set; }
        public string TimeZone { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsEmailConfigured { get; set; }
        public bool IsWhatsAppConfigured { get; set; }
    }
}
