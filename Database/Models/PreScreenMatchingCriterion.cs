using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PreScreenMatchingCriterion
    {
        public int Id { get; set; }
        public string MatchingCriteria { get; set; }
        public int? Points { get; set; }
    }
}
