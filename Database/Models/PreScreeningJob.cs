using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class PreScreeningJob
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string PositionSummary { get; set; }
        public string PrimaryResponsibilities { get; set; }
        public string Location { get; set; }
        public string MandatorySkills { get; set; }
        public string PreferredSkills { get; set; }
        public string ProgrammingLanguages { get; set; }
        public string DatabaseManagement { get; set; }
        public string FrameworksAndLibraries { get; set; }
        public string ToolsAndTechnologies { get; set; }
        public string OperatingSystems { get; set; }
        public string Security { get; set; }
        public string TestingQa { get; set; }
        public string VersionControl { get; set; }
        public string Qualification { get; set; }
        public string MinimumExperience { get; set; }
        public string MaximumExperience { get; set; }
        public string Certifications { get; set; }
        public string WorkMode { get; set; }
        public string ImmediateJoiner { get; set; }
        public string NoticePeriod { get; set; }
        public string TeamCollaboration { get; set; }
        public string ProjectContributions { get; set; }
        public string LeadershipExperience { get; set; }
        public string ClientInteraction { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual JobRequirement Job { get; set; }
    }
}
