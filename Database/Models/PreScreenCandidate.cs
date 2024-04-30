using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PreScreenCandidate
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PreScreenJobId { get; set; }
        public int? CandidateId { get; set; }
        public string CurrentLocation { get; set; }
        public string LinkedinProfile { get; set; }
        public string GithubProfile { get; set; }
        public string Website { get; set; }
        public string ProfessionalSummary { get; set; }
        public string TotalExperience { get; set; }
        public string WorkExperience { get; set; }
        public string Degree { get; set; }
        public string University { get; set; }
        public string ProgrammingLanguages { get; set; }
        public string FrameworksAndLibraries { get; set; }
        public string DatabaseManagement { get; set; }
        public string ToolsAndTechnologies { get; set; }
        public string OperatingSystems { get; set; }
        public string SecurityTools { get; set; }
        public string TestingQa { get; set; }
        public string OtherSkills { get; set; }
        public string VersionControl { get; set; }
        public string Certifications { get; set; }
        public string TeamCollaboration { get; set; }
        public string ProjectContributions { get; set; }
        public string LeadershipExperience { get; set; }
        public string ClientInteraction { get; set; }
        public string NoticePeriod { get; set; }
        public int? Result { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsCompared { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public decimal? Score { get; set; }
        public bool IsProceedForInterviewBites { get; set; }
        public bool IsProceedForTechRound { get; set; }
        public string LinkedinData { get; set; }
    }
}
