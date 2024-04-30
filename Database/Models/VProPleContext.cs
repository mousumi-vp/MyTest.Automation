using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Models
{
    public partial class VProPleContext : DbContext
    {
        public VProPleContext()
        {
        }

        public VProPleContext(DbContextOptions<VProPleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apiconfig> Apiconfigs { get; set; }
        public virtual DbSet<ApikeyConfig> ApikeyConfigs { get; set; }
        public virtual DbSet<Apilog> Apilogs { get; set; }
        public virtual DbSet<BlockedCompany> BlockedCompanies { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidateCodingEvalution> CandidateCodingEvalutions { get; set; }
        public virtual DbSet<CandidateCuratedFeedback> CandidateCuratedFeedbacks { get; set; }
        public virtual DbSet<CandidateCuratedFeedbackHistory> CandidateCuratedFeedbackHistories { get; set; }
        public virtual DbSet<CandidateFeedback> CandidateFeedbacks { get; set; }
        public virtual DbSet<CandidateFeedbackHistory> CandidateFeedbackHistories { get; set; }
        public virtual DbSet<CandidatePrimarySkillsScore> CandidatePrimarySkillsScores { get; set; }
        public virtual DbSet<CandidateSoftSkillScore> CandidateSoftSkillScores { get; set; }
        public virtual DbSet<CodingPlatformLanguage> CodingPlatformLanguages { get; set; }
        public virtual DbSet<CodingQuestionBank> CodingQuestionBanks { get; set; }
        public virtual DbSet<CoreCompetency> CoreCompetencies { get; set; }
        public virtual DbSet<CuratedJdquestion> CuratedJdquestions { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerApi> CustomerApis { get; set; }
        public virtual DbSet<CustomerCodingQuestion> CustomerCodingQuestions { get; set; }
        public virtual DbSet<CustomerDataDelConfig> CustomerDataDelConfigs { get; set; }
        public virtual DbSet<CustomerEmailConfig> CustomerEmailConfigs { get; set; }
        public virtual DbSet<CustomerExpert> CustomerExperts { get; set; }
        public virtual DbSet<CustomerExpertCost> CustomerExpertCosts { get; set; }
        public virtual DbSet<CustomerExpertSkill> CustomerExpertSkills { get; set; }
        public virtual DbSet<CustomerSelectedExpert> CustomerSelectedExperts { get; set; }
        public virtual DbSet<CustomerTimeZone> CustomerTimeZones { get; set; }
        public virtual DbSet<CustomerVenderUser> CustomerVenderUsers { get; set; }
        public virtual DbSet<CustomerVendor> CustomerVendors { get; set; }
        public virtual DbSet<DeletionLog> DeletionLogs { get; set; }
        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<DomainSkill> DomainSkills { get; set; }
        public virtual DbSet<EmailCampaign> EmailCampaigns { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<ExpertAvailability> ExpertAvailabilities { get; set; }
        public virtual DbSet<ExpertBonu> ExpertBonus { get; set; }
        public virtual DbSet<ExpertClientReferral> ExpertClientReferrals { get; set; }
        public virtual DbSet<ExpertCodingQuestion> ExpertCodingQuestions { get; set; }
        public virtual DbSet<ExpertDomain> ExpertDomains { get; set; }
        public virtual DbSet<ExpertFeedback> ExpertFeedbacks { get; set; }
        public virtual DbSet<ExpertHistory> ExpertHistories { get; set; }
        public virtual DbSet<ExpertPaymentStatus> ExpertPaymentStatuses { get; set; }
        public virtual DbSet<ExpertQuestion> ExpertQuestions { get; set; }
        public virtual DbSet<ExpertRate> ExpertRates { get; set; }
        public virtual DbSet<ExpertReferral> ExpertReferrals { get; set; }
        public virtual DbSet<ExpertRequrement> ExpertRequrements { get; set; }
        public virtual DbSet<ExpertSkill> ExpertSkills { get; set; }
        public virtual DbSet<ExpertStatus> ExpertStatuses { get; set; }
        public virtual DbSet<ExpertTimeZone> ExpertTimeZones { get; set; }
        public virtual DbSet<HolidayList> HolidayLists { get; set; }
        public virtual DbSet<InterviewHistory> InterviewHistories { get; set; }
        public virtual DbSet<InterviewMonitor> InterviewMonitors { get; set; }
        public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }
        public virtual DbSet<JobCalibrationSchedule> JobCalibrationSchedules { get; set; }
        public virtual DbSet<JobCodingLanguage> JobCodingLanguages { get; set; }
        public virtual DbSet<JobOwner> JobOwners { get; set; }
        public virtual DbSet<JobRequirement> JobRequirements { get; set; }
        public virtual DbSet<JobSkill> JobSkills { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<LanguageSkillMapping> LanguageSkillMappings { get; set; }
        public virtual DbSet<Mcq> Mcqs { get; set; }
        public virtual DbSet<MyIdea> MyIdeas { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationReceipt> NotificationReceipts { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<OpenAiconfig> OpenAiconfigs { get; set; }
        public virtual DbSet<PlatformPage> PlatformPages { get; set; }
        public virtual DbSet<PlatformTracker> PlatformTrackers { get; set; }
        public virtual DbSet<PortalConfig> PortalConfigs { get; set; }
        public virtual DbSet<PreScreenCandidate> PreScreenCandidates { get; set; }
        public virtual DbSet<PreScreenCandidateMatch> PreScreenCandidateMatches { get; set; }
        public virtual DbSet<PreScreenMatchingCriterion> PreScreenMatchingCriteria { get; set; }
        public virtual DbSet<PreScreeningJob> PreScreeningJobs { get; set; }
        public virtual DbSet<PrimaryColValue> PrimaryColValues { get; set; }
        public virtual DbSet<PublicEmail> PublicEmails { get; set; }
        public virtual DbSet<PvainterviewFeedback> PvainterviewFeedbacks { get; set; }
        public virtual DbSet<PvainterviewHistory> PvainterviewHistories { get; set; }
        public virtual DbSet<PvajobConfiguration> PvajobConfigurations { get; set; }
        public virtual DbSet<PvajobQuestion> PvajobQuestions { get; set; }
        public virtual DbSet<Pvaquestion> Pvaquestions { get; set; }
        public virtual DbSet<Pvaschedule> Pvaschedules { get; set; }
        public virtual DbSet<PvascheduleNote> PvascheduleNotes { get; set; }
        public virtual DbSet<PvauserVerification> PvauserVerifications { get; set; }
        public virtual DbSet<QualityChecklist> QualityChecklists { get; set; }
        public virtual DbSet<QuestionBank> QuestionBanks { get; set; }
        public virtual DbSet<ScheduleNote> ScheduleNotes { get; set; }
        public virtual DbSet<ScheduleQuestionsAsk> ScheduleQuestionsAsks { get; set; }
        public virtual DbSet<ScheduledCodeChallange> ScheduledCodeChallanges { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SpeakerTextTemp> SpeakerTextTemps { get; set; }
        public virtual DbSet<Spoc> Spocs { get; set; }
        public virtual DbSet<Spocavailability> Spocavailabilities { get; set; }
        public virtual DbSet<TempA> TempAs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<VpSkill> VpSkills { get; set; }
        public virtual DbSet<VpcurateJd> VpcurateJds { get; set; }
        public virtual DbSet<VpcuratedJdskill> VpcuratedJdskills { get; set; }
        public virtual DbSet<WhatsAppLog> WhatsAppLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-A4GFU0T;Initial Catalog=VProPle;Integrated Security=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Apiconfig>(entity =>
            {
                entity.ToTable("APIConfig");

                entity.Property(e => e.Apiname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APIName");

                entity.Property(e => e.Apitype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("APIType");
            });

            modelBuilder.Entity<ApikeyConfig>(entity =>
            {
                entity.ToTable("APIKeyConfig");

                entity.Property(e => e.Apikey)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APIKey");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Apilog>(entity =>
            {
                entity.HasKey(e => e.LoggerId);

                entity.ToTable("APILogs");

                entity.Property(e => e.LoggerId).HasColumnName("LoggerID");

                entity.Property(e => e.ContentType).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsHttps).HasMaxLength(50);

                entity.Property(e => e.LoggerApi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("LoggerAPI");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Protocol)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RemoteIpAddress).HasMaxLength(50);

                entity.Property(e => e.Scheme).HasMaxLength(250);
            });

            modelBuilder.Entity<BlockedCompany>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BlockedCompanies", "exp");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Expert)
                    .WithMany()
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedCompanies_Experts");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasIndex(e => e.CvreceivedDate, "Indx_Candidates_CVReceivedDate");

                entity.HasIndex(e => e.CvreceivedDate, "Indx_Candidates_CVReceivedDate_Name");

                entity.HasIndex(e => e.Email, "Indx_Candidates_Email");

                entity.HasIndex(e => e.PvaId, "UQ_PvaId_NotNull")
                    .IsUnique()
                    .HasFilter("([PvaId] IS NOT NULL)");

                entity.HasIndex(e => e.RanqitId, "UQ_RanqitId_NotNull")
                    .IsUnique()
                    .HasFilter("([RanqitId] IS NOT NULL)");

                entity.Property(e => e.AssignedTo).HasMaxLength(10);

                entity.Property(e => e.AvailableTime).HasColumnType("datetime");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .HasDefaultValueSql("((91))")
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(minute,(750),getdate()))");

                entity.Property(e => e.Ctc)
                    .HasMaxLength(10)
                    .HasColumnName("CTC")
                    .IsFixedLength(true);

                entity.Property(e => e.CurrentCompany).HasMaxLength(150);

                entity.Property(e => e.CurrentLocation).HasMaxLength(100);

                entity.Property(e => e.CvreceivedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CVReceivedDate")
                    .HasDefaultValueSql("(dateadd(minute,(750),getdate()))");

                entity.Property(e => e.Ectc)
                    .HasMaxLength(10)
                    .HasColumnName("ECTC")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Exp).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.GovtIdproofType)
                    .HasMaxLength(50)
                    .HasColumnName("GovtIDProofType");

                entity.Property(e => e.GovtIdproofVal)
                    .HasMaxLength(50)
                    .HasColumnName("GovtIDProofVal");

                entity.Property(e => e.HiringType).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NoticePeriod)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Owner).HasMaxLength(10);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Portfolio).HasMaxLength(200);

                entity.Property(e => e.PreferredLocation).HasMaxLength(100);

                entity.Property(e => e.RelExp).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ResumePath).HasMaxLength(200);

                entity.Property(e => e.TimeZone).HasMaxLength(200);
            });

            modelBuilder.Entity<CandidateCodingEvalution>(entity =>
            {
                entity.ToTable("CandidateCodingEvalution");

                entity.Property(e => e.LanguageId).HasColumnName("languageId");

                entity.Property(e => e.Memory).HasColumnName("memory");

                entity.Property(e => e.Time).HasColumnName("time");
            });

            modelBuilder.Entity<CandidateCuratedFeedback>(entity =>
            {
                entity.ToTable("CandidateCuratedFeedback");

                entity.Property(e => e.VpcurateJdid).HasColumnName("VPCurateJDId");
            });

            modelBuilder.Entity<CandidateCuratedFeedbackHistory>(entity =>
            {
                entity.ToTable("CandidateCuratedFeedbackHistory");

                entity.Property(e => e.VpcurateJdid).HasColumnName("VPCurateJDId");
            });

            modelBuilder.Entity<CandidateFeedback>(entity =>
            {
                entity.ToTable("CandidateFeedback");

                entity.HasIndex(e => e.ScheduleId, "CandidateFeedback_Index");

                entity.HasIndex(e => e.ScheduleId, "UQ__Candidat__9C8A5B480BA92E94")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdditionalComment).IsUnicode(false);

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CodeTestEndOn).HasColumnType("datetime");

                entity.Property(e => e.CodeTestStartOn).HasColumnType("datetime");

                entity.Property(e => e.CommentToQa)
                    .IsUnicode(false)
                    .HasColumnName("CommentToQA");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(dateadd(minute,(750),getdate()))");

                entity.Property(e => e.CustomerFeedbackOn).HasColumnType("datetime");

                entity.Property(e => e.InterviewDuration).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.MessageToVp).HasColumnName("MessageToVP");

                entity.Property(e => e.Notes).HasMaxLength(300);

                entity.Property(e => e.OneWayVideoPath).HasMaxLength(50);

                entity.Property(e => e.QcuserId)
                    .HasMaxLength(10)
                    .HasColumnName("QCUserId");

                entity.Property(e => e.Score).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.SubmittedOn).HasColumnType("datetime");

                entity.Property(e => e.TotalDuration).HasMaxLength(50);

                entity.Property(e => e.TranscriptPath).IsUnicode(false);
            });

            modelBuilder.Entity<CandidateFeedbackHistory>(entity =>
            {
                entity.ToTable("CandidateFeedbackHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(dateadd(minute,(750),getdate()))");

                entity.Property(e => e.Notes).HasMaxLength(300);

                entity.Property(e => e.Score).HasColumnType("decimal(3, 1)");
            });

            modelBuilder.Entity<CandidatePrimarySkillsScore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CandidatePrimarySkillsScore");

                entity.Property(e => e.CandidateId)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<CandidateSoftSkillScore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CandidateSoftSkillScore");

                entity.Property(e => e.CandidateId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CodingPlatformLanguage>(entity =>
            {
                entity.Property(e => e.ApiUrl)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Mode).HasMaxLength(50);
            });

            modelBuilder.Entity<CodingQuestionBank>(entity =>
            {
                entity.ToTable("CodingQuestionBank");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CoreCompetency>(entity =>
            {
                entity.HasKey(e => e.CompetencyId);

                entity.ToTable("CoreCompetency");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CuratedJdquestion>(entity =>
            {
                entity.ToTable("CuratedJDQuestions");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CuratedJdid).HasColumnName("CuratedJDId");

                entity.Property(e => e.Question).IsRequired();

                entity.HasOne(d => d.CuratedJd)
                    .WithMany(p => p.CuratedJdquestions)
                    .HasForeignKey(d => d.CuratedJdid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CuratedJDQuestions_VPCurateJD");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Currency");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Symbol).HasMaxLength(5);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Customer__737584F6218A81D8")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasComment("CU + Number");

                entity.Property(e => e.AccountLead).HasMaxLength(10);

                entity.Property(e => e.AccountManger).HasMaxLength(10);

                entity.Property(e => e.AgreementDate).HasColumnType("date");

                entity.Property(e => e.BillingAddress).HasMaxLength(300);

                entity.Property(e => e.BillingCycle).HasMaxLength(50);

                entity.Property(e => e.BillingRateFrom).HasColumnType("date");

                entity.Property(e => e.Category).HasDefaultValueSql("((3))");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmailVericationCode).HasMaxLength(100);

                entity.Property(e => e.IsApienabled).HasColumnName("IsAPIEnabled");

                entity.Property(e => e.IsRanqitSubscribed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTechnicalRoundSubscribed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MaxUser).HasDefaultValueSql("((10))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OutOfService).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentOption).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.PvabillingRate).HasColumnName("PVABillingRate");

                entity.Property(e => e.Url).HasMaxLength(100);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_UserTypes");
            });

            modelBuilder.Entity<CustomerApi>(entity =>
            {
                entity.ToTable("CustomerAPI");

                entity.Property(e => e.Apiid).HasColumnName("APIId");
            });

            modelBuilder.Entity<CustomerCodingQuestion>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Question).IsRequired();
            });

            modelBuilder.Entity<CustomerDataDelConfig>(entity =>
            {
                entity.HasKey(e => e.ConfigId);

                entity.ToTable("CustomerDataDelConfig");

                entity.Property(e => e.CandidateImages).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.DataRetention).HasDefaultValueSql("((200))");

                entity.Property(e => e.DeletionCycle)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'Monthly')");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Resumes).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Transcript).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Videos).HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerDataDelConfigs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerDataDelConfig_Customers");
            });

            modelBuilder.Entity<CustomerEmailConfig>(entity =>
            {
                entity.ToTable("CustomerEmailConfig");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EnableSsl).HasColumnName("EnableSSL");

                entity.Property(e => e.FromEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HostName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<CustomerExpert>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(10);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("EmailID");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CustomerExpertCost>(entity =>
            {
                entity.ToTable("CustomerExpertCost");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Timezone).HasMaxLength(250);

                entity.Property(e => e.UpdatedBy).HasMaxLength(10);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerExpertSkill>(entity =>
            {
                entity.Property(e => e.CustomerExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.CustomerExpert)
                    .WithMany(p => p.CustomerExpertSkills)
                    .HasForeignKey(d => d.CustomerExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerExpertSkills_CustomerExperts");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.CustomerExpertSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerExpertSkills_Skills");
            });

            modelBuilder.Entity<CustomerSelectedExpert>(entity =>
            {
                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<CustomerTimeZone>(entity =>
            {
                entity.ToTable("CustomerTimeZone");

                entity.Property(e => e.TimeZone)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<CustomerVenderUser>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<CustomerVendor>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<DeletionLog>(entity =>
            {
                entity.Property(e => e.DelCandidateImgsSize).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.DelResumesSize).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.DelTranscriptSize).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.DelVideosSize).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.DeletedBy).HasMaxLength(10);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Domain>(entity =>
            {
                entity.Property(e => e.DomainId).HasColumnName("DomainID");

                entity.Property(e => e.DomainName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DomainSkill>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmailCampaign>(entity =>
            {
                entity.ToTable("EmailCampaign");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Expert>(entity =>
            {
                entity.ToTable("Experts", "exp");

                entity.HasIndex(e => e.CreatedOn, "Indx_Experts_CreatedOn");

                entity.HasIndex(e => new { e.StatusId, e.CreatedOn }, "Indx_Experts_StatusId_CreatedOn");

                entity.HasIndex(e => e.StatusId, "Indx_Experts_StatusId_Name");

                entity.Property(e => e.Id).HasMaxLength(10);

                entity.Property(e => e.BlockedOn).HasColumnType("datetime");

                entity.Property(e => e.Category).HasDefaultValueSql("((3))");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(minute,(810),getdate()))");

                entity.Property(e => e.EmailCode).HasDefaultValueSql("(newid())");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LinkedInUrl).HasColumnName("LinkedInURL");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('VEmp101')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.Postion).HasMaxLength(150);

                entity.Property(e => e.ProfileSummary).HasColumnType("text");

                entity.Property(e => e.PseudoName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PwdSalt).IsRequired();

                entity.Property(e => e.Remarks).HasMaxLength(150);

                entity.Property(e => e.ResumeExt)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ResumeId).HasMaxLength(50);

                entity.Property(e => e.TimeZone).HasMaxLength(200);

                entity.Property(e => e.UpiAddress)
                    .HasMaxLength(100)
                    .HasColumnName("upiAddress");

                entity.Property(e => e.WhatsAppNo)
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Experts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Experts_ExpertStatus");
            });

            modelBuilder.Entity<ExpertAvailability>(entity =>
            {
                entity.ToTable("ExpertAvailability");

                entity.HasIndex(e => e.Date, "Indx_ExpertAvailability_Date");

                entity.HasIndex(e => new { e.ExpertId, e.Date }, "Indx_ExpertAvailability_ExptId_Date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("TIme");
            });

            modelBuilder.Entity<ExpertBonu>(entity =>
            {
                entity.Property(e => e.BonusType).HasMaxLength(200);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(minute,(810),getdate()))");

                entity.Property(e => e.ExpertId).HasMaxLength(10);

                entity.Property(e => e.PaidOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpertClientReferral>(entity =>
            {
                entity.ToTable("ExpertClientReferral");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ClientWebsite)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.ContactPersonEmail)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPersonName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPersonPhone)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.ExpertId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.PaidBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PaidOn).HasColumnType("datetime");

                entity.Property(e => e.ReferredOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpertCodingQuestion>(entity =>
            {
                entity.ToTable("ExpertCodingQuestion");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpertDomain>(entity =>
            {
                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.ExpertDomains)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpertDomains_Domains");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.ExpertDomains)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpertDomains_Experts");
            });

            modelBuilder.Entity<ExpertFeedback>(entity =>
            {
                entity.ToTable("ExpertFeedback");

                entity.Property(e => e.SubmittedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpertHistory>(entity =>
            {
                entity.ToTable("ExpertHistory");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Notes).IsRequired();
            });

            modelBuilder.Entity<ExpertPaymentStatus>(entity =>
            {
                entity.ToTable("ExpertPaymentStatus");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(dateadd(minute,(810),getdate()))");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.PaidBy).HasMaxLength(10);

                entity.Property(e => e.PaymentOn).HasColumnType("datetime");

                entity.Property(e => e.PaymentStatus).HasMaxLength(10);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.TransferId).HasMaxLength(50);

                entity.Property(e => e.Vpa)
                    .HasMaxLength(100)
                    .HasColumnName("VPA");
            });

            modelBuilder.Entity<ExpertQuestion>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.QuestionText).IsRequired();
            });

            modelBuilder.Entity<ExpertRate>(entity =>
            {
                entity.ToTable("ExpertRate");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ExpertId).HasMaxLength(10);

                entity.Property(e => e.Rate).HasDefaultValueSql("((300))");

                entity.Property(e => e.Timezone).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(10);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpertReferral>(entity =>
            {
                entity.Property(e => e.Comments).HasColumnName("COMMENTS");

                entity.Property(e => e.ExpertId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.PaidBy).HasMaxLength(250);

                entity.Property(e => e.PaidOn).HasColumnType("datetime");

                entity.Property(e => e.ReferredOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(hour,(12.30),getdate()))");

                entity.Property(e => e.ResumeId).HasMaxLength(50);

                entity.Property(e => e.SkillSet).HasMaxLength(250);
            });

            modelBuilder.Entity<ExpertRequrement>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsRequired();

                entity.Property(e => e.ResolveBy).HasMaxLength(10);

                entity.Property(e => e.ResolveOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpertSkill>(entity =>
            {
                entity.ToTable("ExpertSkills", "exp");

                entity.HasIndex(e => e.ExpertId, "Indx_ExpertSkills_ExptId");

                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.ExpertSkills)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpertSkills_Experts");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.ExpertSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpertSkills_Skills");
            });

            modelBuilder.Entity<ExpertStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK_Status");

                entity.ToTable("ExpertStatus", "exp");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExpertTimeZone>(entity =>
            {
                entity.ToTable("ExpertTimeZone");

                entity.Property(e => e.ExpertId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TimeZone)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<HolidayList>(entity =>
            {
                entity.ToTable("HolidayList");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.HolidayName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<InterviewHistory>(entity =>
            {
                entity.ToTable("InterviewHistory");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(minute,(810),getdate()))");

                entity.Property(e => e.ExpertId).HasMaxLength(10);

                entity.Property(e => e.Notes).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<InterviewMonitor>(entity =>
            {
                entity.ToTable("InterviewMonitor");

                entity.Property(e => e.EmpId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InterviewSchedule>(entity =>
            {
                entity.ToTable("InterviewSchedule");

                entity.HasIndex(e => e.CandidateId, "Indx_IntvSch_CandId");

                entity.HasIndex(e => e.ExpertId, "Indx_IntvSch_ExptId");

                entity.HasIndex(e => new { e.ExpertId, e.ScheduledOn }, "Indx_IntvSch_ExptId_SchOn");

                entity.HasIndex(e => new { e.ExpertId, e.Status }, "Indx_IntvSch_ExptId_Status");

                entity.HasIndex(e => e.JobId, "Indx_IntvSch_JobId");

                entity.HasIndex(e => e.JobId, "Indx_IntvSch_JobId_CandId");

                entity.HasIndex(e => e.JobId, "Indx_IntvSch_JobId_CandId_SchOn_Status");

                entity.HasIndex(e => new { e.JobId, e.Status }, "Indx_IntvSch_JobId_Status");

                entity.HasIndex(e => new { e.VproPleSpoc, e.CreatedBy, e.ScheduledOn, e.Status }, "Indx_IntvSch_SPOC_CreatedBy_SchOn_Status");

                entity.HasIndex(e => e.ScheduledOn, "Indx_IntvSch_SchOn");

                entity.HasIndex(e => e.ScheduledOn, "Indx_IntvSch_SchOn_JobId_CandId_Status_CR");

                entity.HasIndex(e => e.ScheduledOn, "Indx_IntvSch_SchOn_JobId_ExptId");

                entity.HasIndex(e => e.Status, "Indx_IntvSch_Status");

                entity.HasIndex(e => e.Status, "Indx_IntvSch_Status_CandId");

                entity.HasIndex(e => e.Status, "Indx_IntvSch_Status_ExptId_CandId");

                entity.HasIndex(e => e.Status, "Indx_IntvSch_Status_JobId_CandId_SchOn_CR");

                entity.HasIndex(e => new { e.Status, e.VproPleSpoc, e.ScheduledOn }, "Indx_IntvSch_Status_SPOC_SchOn");

                entity.HasIndex(e => new { e.Status, e.ScheduledOn }, "Indx_IntvSch_Status_SchOn");

                entity.HasIndex(e => new { e.Status, e.ScheduledOn }, "Indx_IntvSch_Status_SchOn_ExptId");

                entity.HasIndex(e => new { e.Status, e.ScheduledOn }, "Indx_IntvSch_Status_SchOn_JobId_CandId");

                entity.Property(e => e.CancelReason).HasMaxLength(250);

                entity.Property(e => e.CancelledBy).HasMaxLength(10);

                entity.Property(e => e.ChecklistAssignTo).HasMaxLength(10);

                entity.Property(e => e.ChecklistDoneOn).HasColumnType("datetime");

                entity.Property(e => e.CompletedBy).HasMaxLength(10);

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EnhancedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpertId).HasMaxLength(10);

                entity.Property(e => e.FeedbackOn).HasColumnType("datetime");

                entity.Property(e => e.InterviewCode).HasMaxLength(20);

                entity.Property(e => e.IsPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.MediaUploadOn).HasColumnType("datetime");

                entity.Property(e => e.PaidBy).HasMaxLength(10);

                entity.Property(e => e.PaidOn).HasColumnType("datetime");

                entity.Property(e => e.QualityAssigned).HasMaxLength(10);

                entity.Property(e => e.ScheduledOn).HasColumnType("datetime");

                entity.Property(e => e.UniqueCode).HasMaxLength(20);

                entity.Property(e => e.VproPleSpoc).HasColumnName("VProPleSPOC");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.InterviewSchedules)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewSchedule_Candidates");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.InterviewSchedules)
                    .HasForeignKey(d => d.ExpertId)
                    .HasConstraintName("FK_InterviewSchedule_Experts");

                entity.HasOne(d => d.VproPleSpocNavigation)
                    .WithMany(p => p.InterviewSchedules)
                    .HasForeignKey(d => d.VproPleSpoc)
                    .HasConstraintName("FK_InterviewSchedule_SPOC");
            });

            modelBuilder.Entity<JobCalibrationSchedule>(entity =>
            {
                entity.ToTable("JobCalibrationSchedule");

                entity.Property(e => e.CalibrationOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpertId).HasMaxLength(10);

                entity.Property(e => e.PaidBy).HasMaxLength(10);

                entity.Property(e => e.PaidOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobOwner>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<JobRequirement>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.HasIndex(e => e.CustomerId, "Indx_Jobreqt_CustomerId");

                entity.HasIndex(e => e.Status, "Indx_Jobreqt_Status");

                entity.Property(e => e.CountofCvs).HasColumnName("CountofCVs");

                entity.Property(e => e.CreatedBy).HasMaxLength(10);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CuratedJdText).IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.EndCustomer).HasMaxLength(50);

                entity.Property(e => e.Hremail).HasColumnName("HREmail");

                entity.Property(e => e.IsCvsourceing).HasColumnName("IsCVSourceing");

                entity.Property(e => e.IsInstructionUpload).HasDefaultValueSql("((0))");

                entity.Property(e => e.JobType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ReferenceCode).HasMaxLength(20);

                entity.Property(e => e.Referral).HasMaxLength(100);

                entity.Property(e => e.Round)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.JobRequirements)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequirements_Customers");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.JobRequirements)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequirements_Domains");
            });

            modelBuilder.Entity<JobSkill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobSkills)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobSkills_JobRequirements");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.JobSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobSkills_Skills");
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JobStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LanguageSkillMapping>(entity =>
            {
                entity.ToTable("LanguageSkillMapping");
            });

            modelBuilder.Entity<Mcq>(entity =>
            {
                entity.ToTable("MCQ");

                entity.Property(e => e.CreatedBy).HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MyIdea>(entity =>
            {
                entity.Property(e => e.AdminComments)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CommentedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.NotificatonType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificatonTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_NotificationTypes");
            });

            modelBuilder.Entity<NotificationReceipt>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.NotificationId }, "unq_user_notification")
                    .IsUnique();

                entity.Property(e => e.ReadOn).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.NotificationReceipts)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotifcationReceipts_Notifications");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeName).HasMaxLength(150);
            });

            modelBuilder.Entity<OpenAiconfig>(entity =>
            {
                entity.ToTable("OpenAIConfig");

                entity.Property(e => e.Apiurl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("APIUrl");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(minute,(750),getdate()))");

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prompt).IsRequired();
            });

            modelBuilder.Entity<PlatformPage>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PlatformTracker>(entity =>
            {
                entity.ToTable("PlatformTracker");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<PortalConfig>(entity =>
            {
                entity.Property(e => e.Configuration).IsRequired();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PreScreenCandidate>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NoticePeriod).HasMaxLength(50);

                entity.Property(e => e.Score).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TestingQa).HasColumnName("TestingQA");

                entity.Property(e => e.TotalExperience).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(200);
            });

            modelBuilder.Entity<PreScreenCandidateMatch>(entity =>
            {
                entity.ToTable("PreScreenCandidateMatch");

                entity.Property(e => e.Certifications).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClientInteraction).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DatabaseManagement).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Experience).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FrameworksAndLibraries).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GithubProfile).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ImmediateJoiner).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LeadershipExperience).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Location).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MandatorySkill).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NoticePeriod).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OperatingSystems).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PreferSkills).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProgrammingLanguages).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProjectContributions).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qualification).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SecurityTools).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TeamCollaboration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TestingQa)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TestingQA");

                entity.Property(e => e.ToolsAndTechnologies).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValidLinkedUrl)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ValidLinkedURL");

                entity.Property(e => e.VersionControl).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WorkMode).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PreScreenCandidateMatches)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_PreScreenCandidateMatch_Customers");
            });

            modelBuilder.Entity<PreScreenMatchingCriterion>(entity =>
            {
                entity.Property(e => e.MatchingCriteria)
                    .HasMaxLength(300)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PreScreeningJob>(entity =>
            {
                entity.ToTable("PreScreeningJob");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.TestingQa).HasColumnName("TestingQA");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PreScreeningJobs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreScreeningJob_JobRequirements");
            });

            modelBuilder.Entity<PrimaryColValue>(entity =>
            {
                entity.HasKey(e => e.TableName);

                entity.Property(e => e.TableName).HasMaxLength(50);
            });

            modelBuilder.Entity<PublicEmail>(entity =>
            {
                entity.ToTable("PublicEmail");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EmailDomain).HasMaxLength(50);
            });

            modelBuilder.Entity<PvainterviewFeedback>(entity =>
            {
                entity.ToTable("PVAInterviewFeedback");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerExpertId).HasMaxLength(10);

                entity.Property(e => e.FeedbackCode).HasDefaultValueSql("(newid())");

                entity.Property(e => e.VaschId).HasColumnName("VASchId");
            });

            modelBuilder.Entity<PvainterviewHistory>(entity =>
            {
                entity.ToTable("PVAInterviewHistory");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.VaschId).HasColumnName("VASchId");
            });

            modelBuilder.Entity<PvajobConfiguration>(entity =>
            {
                entity.ToTable("PVAJobConfiguration");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Mcq).HasColumnName("MCQ");

                entity.Property(e => e.Notes).HasMaxLength(250);

                entity.Property(e => e.Status).HasDefaultValueSql("((2))");

                entity.Property(e => e.Validity).HasDefaultValueSql("((3))");
            });

            modelBuilder.Entity<PvajobQuestion>(entity =>
            {
                entity.ToTable("PVAJobQuestions");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Pvaquestion>(entity =>
            {
                entity.ToTable("PVAQuestions");

                entity.Property(e => e.PvajobQuestionId).HasColumnName("PVAJobQuestionId");

                entity.Property(e => e.SaveOn).HasColumnType("datetime");

                entity.Property(e => e.VaschId).HasColumnName("VASchId");

                entity.Property(e => e.VerifyBy).HasMaxLength(15);

                entity.Property(e => e.VerifyOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Pvaschedule>(entity =>
            {
                entity.HasKey(e => e.VaschId);

                entity.ToTable("PVASchedules");

                entity.HasIndex(e => e.UniqueId, "UQ__PVASched__A2A2A54B69D5CDD8")
                    .IsUnique();

                entity.Property(e => e.VaschId).HasColumnName("VASchId");

                entity.Property(e => e.CompletedBy).HasMaxLength(15);

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.MarksObtained).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Otp).HasMaxLength(10);

                entity.Property(e => e.ScheduledOn).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TotalMarks).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.UniCode).HasMaxLength(10);

                entity.Property(e => e.UniqueId).HasMaxLength(20);

                entity.Property(e => e.VideoLink).HasMaxLength(300);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Pvaschedules)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PVASchedules_Candidates");
            });

            modelBuilder.Entity<PvascheduleNote>(entity =>
            {
                entity.ToTable("PVAScheduleNotes");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.VaschId).HasColumnName("VASchId");
            });

            modelBuilder.Entity<PvauserVerification>(entity =>
            {
                entity.ToTable("PVAUserVerification");

                entity.Property(e => e.BrowseName).HasMaxLength(50);

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.VaschId).HasColumnName("VASchId");
            });

            modelBuilder.Entity<QualityChecklist>(entity =>
            {
                entity.ToTable("QualityChecklist");

                entity.HasIndex(e => e.ScheduleId, "UQ__QualityC__9C8A5B480CBF8F53")
                    .IsUnique();

                entity.Property(e => e.CoQa)
                    .HasMaxLength(10)
                    .HasColumnName("Co_QA");

                entity.Property(e => e.Duration).HasMaxLength(50);

                entity.Property(e => e.LipsMisMatchTime).HasMaxLength(50);

                entity.Property(e => e.SubmittedBy).HasMaxLength(10);

                entity.Property(e => e.SubmittedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<QuestionBank>(entity =>
            {
                entity.ToTable("QuestionBank");

                entity.HasIndex(e => new { e.SkillId, e.IsCoding }, "Indx_QB_Skills_Iscoding");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<ScheduleNote>(entity =>
            {
                entity.HasIndex(e => new { e.SchId, e.IsExternal }, "Indx_ScheduleNotes_SchId");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsExternal)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ScheduleQuestionsAsk>(entity =>
            {
                entity.ToTable("ScheduleQuestionsAsk");

                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<ScheduledCodeChallange>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.QuestionIds)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<SpeakerTextTemp>(entity =>
            {
                entity.ToTable("SpeakerTextTemp");
            });

            modelBuilder.Entity<Spoc>(entity =>
            {
                entity.ToTable("SPOC");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(hour,(12.30),getdate()))");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.MeetingLink).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Spocavailability>(entity =>
            {
                entity.ToTable("SPOCAvailability");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ToTime).HasColumnName("ToTIme");

                entity.Property(e => e.UpdatedBy).HasMaxLength(10);
            });

            modelBuilder.Entity<TempA>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tempA");

                entity.Property(e => e.CancelReason).HasMaxLength(250);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PwdResetCode).HasMaxLength(100);

                entity.Property(e => e.PwdSalt).IsRequired();

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TimeZone)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('India Standard Time')");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VpSkill>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<VpcurateJd>(entity =>
            {
                entity.ToTable("VPCurateJD");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.VpskillDefination)
                    .IsRequired()
                    .HasMaxLength(350)
                    .HasColumnName("VPSkillDefination");

                entity.Property(e => e.VpskillName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("VPSkillName");
            });

            modelBuilder.Entity<VpcuratedJdskill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VPCuratedJDSkills");
            });

            modelBuilder.Entity<WhatsAppLog>(entity =>
            {
                entity.ToTable("WhatsAppLog");

                entity.Property(e => e.CampaignName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
