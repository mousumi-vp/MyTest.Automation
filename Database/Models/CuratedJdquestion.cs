using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class CuratedJdquestion
    {
        public int Id { get; set; }
        public int CuratedJdid { get; set; }
        public string Question { get; set; }
        public int Type { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual VpcurateJd CuratedJd { get; set; }
    }
}
