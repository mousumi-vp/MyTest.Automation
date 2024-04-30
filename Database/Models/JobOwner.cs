using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class JobOwner
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
    }
}
