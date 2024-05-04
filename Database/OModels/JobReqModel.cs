using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.OModels
{
    public class JobReqModel
    {
        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImg { get; set; }
        public string CustomerUrl { get; set; }
        public string DomainName { get; set; }
        public int DomainId { get; set; }
        public int JobType { get; set; }
        public bool IsCandidateEmail { get; set; }
        public int CodingQuestions { get; set; }
        public int Status { get; set; }
        public int? CountofCvs { get; set; }
        public string Location { get; set; }
        //public string HREmail { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public bool IsCodingTest { get; set; }
        //public string Remarks { get; set; }
        //public string NoteToQC { get; set; }
        //public List<CalibrationFeedback> ExpertFeedback { get; set; }
        //public string Question { get; set; }
    }
    //public class CalibrationFeedback
    //{
    //    public string Name { get; set; }
    //    public string ExpertFeedback { get; set; }
    //}
}
