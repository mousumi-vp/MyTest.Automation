using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CandidatePrimarySkillsScore
    {
        public string CandidateId { get; set; }
        public int JobId { get; set; }
        public int DomainId { get; set; }
        public int Score { get; set; }
    }
}
