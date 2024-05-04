using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.OModels
{
    public class CandidateDetails
    {
        public int SchId { get; set; }
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
        public string ResumePath { get; set; }
        public DateTime? CvreceivedDate { get; set; }
        public int JobId { get; set; }
        public string AvailableTime { get; set; }
        public string Timezone { get; set; }
        public decimal? RelExp { get; set; }
    }
}
