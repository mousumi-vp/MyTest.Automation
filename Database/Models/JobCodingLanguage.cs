using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class JobCodingLanguage
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int LanguageId { get; set; }
    }
}
