using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CandidateSoftSkillScore
    {
        public string CandidateId { get; set; }
        public int SoftSkillId { get; set; }
        public int Score { get; set; }
    }
}
