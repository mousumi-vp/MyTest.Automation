using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CodingPlatformLanguage
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public bool Status { get; set; }
        public string ApiUrl { get; set; }
        public string Mode { get; set; }
    }
}
