using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerVenderUser
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string UserId { get; set; }
        public bool OnlyTracker { get; set; }
    }
}
