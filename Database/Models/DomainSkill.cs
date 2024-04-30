using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class DomainSkill
    {
        public int Id { get; set; }
        public int DomainId { get; set; }
        public string Name { get; set; }
    }
}
