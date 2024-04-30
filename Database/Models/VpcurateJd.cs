using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class VpcurateJd
    {
        public VpcurateJd()
        {
            CuratedJdquestions = new HashSet<CuratedJdquestion>();
        }

        public int Id { get; set; }
        public int JobId { get; set; }
        public string VpskillName { get; set; }
        public string VpskillDefination { get; set; }
        public string Type { get; set; }
        public int? Weightage { get; set; }
        public string QuestionsText { get; set; }

        public virtual ICollection<CuratedJdquestion> CuratedJdquestions { get; set; }
    }
}
