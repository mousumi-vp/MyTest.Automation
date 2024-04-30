using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PublicEmail
    {
        public int Id { get; set; }
        public string EmailDomain { get; set; }
    }
}
