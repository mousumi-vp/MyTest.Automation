using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PreScreenCandidateMatch
    {
        public int Id { get; set; }
        public int? PreScreenCandidateId { get; set; }
        public int? PreScreenJobId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? Location { get; set; }
        public decimal? Experience { get; set; }
        public decimal? MandatorySkill { get; set; }
        public decimal? PreferSkills { get; set; }
        public decimal? ProgrammingLanguages { get; set; }
        public decimal? DatabaseManagement { get; set; }
        public decimal? FrameworksAndLibraries { get; set; }
        public decimal? ToolsAndTechnologies { get; set; }
        public decimal? OperatingSystems { get; set; }
        public decimal? SecurityTools { get; set; }
        public decimal? TestingQa { get; set; }
        public decimal? VersionControl { get; set; }
        public decimal? Qualification { get; set; }
        public decimal? Certifications { get; set; }
        public decimal? WorkMode { get; set; }
        public decimal? ImmediateJoiner { get; set; }
        public decimal? NoticePeriod { get; set; }
        public decimal? TeamCollaboration { get; set; }
        public decimal? ProjectContributions { get; set; }
        public decimal? LeadershipExperience { get; set; }
        public decimal? ClientInteraction { get; set; }
        public decimal? ValidLinkedUrl { get; set; }
        public decimal? GithubProfile { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
