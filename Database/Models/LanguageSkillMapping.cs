using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class LanguageSkillMapping
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int SkillId { get; set; }
    }
}
