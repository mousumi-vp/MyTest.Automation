using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CustomerSelectedExpert
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public int JobId { get; set; }
    }
}
