using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.OModels;
using static Database.AllEnum;

namespace Database.Contracts
{
    public interface ITechnicalDAL
    {
        Candidate GetCandidateDetails(int candidateId);
        
        List<InterviewSchedule> GetComing90InterviewSchedules();
        Customer GetCustomerDetails(int id);
        Spoc GetSpocDetails(int id);
        bool IsReschedule(int id);
        bool CheckConfirmUniqueCode(string uniqcode);
        int AddUniqCode(int IId, string ucode);
        List<string> GetCandidateOwnerEmails(int candidateId, int jobId);
        JobRequirement GetJobRequirement(int Id);
        List<string> GetJobOwnersEmails(int jobId);
        CustomerEmailConfig GetEmailConfig(int cId);
        Expert GetExpertId(string Id);
        CandidateFeedback GetCandidateFeedbackBySchedule(int schduleId);
       
        List<CandidateDetails> GetAllParkedCandidate();
        int UpdateScheduleStatus(int schId, ScheduleStatusEnum interviewStatus);
        int ParkedToNewCandidate(Candidate candidate);
        int AddInterviwHistory(InterviewHistory history);
        
        int CheckScheduleStatus(int schid);
        List<Skill> GetAllSkills();
        List<string> GetJobSkills(int jobId);
        JobReqModel GetJobDetails(int id);
        User GetEmployeesDetails(string userid);
        void AddWhatsAppLog(WhatsAppLog obj);
        string GetAuthSchedulerSetting();
        bool ClientAgreementExpired(int cid, DateTime schon);
        bool CheckHoliday(DateTime schon);
        InterviewSchedule GetScheduleByCandidateId(int id);
        List<int> GetHostAvailbilityByTime(DateTime sch_time);
        string GetSPOCName(int spocId);
        int CheckCustomerExpert(int jobId);
        List<Expert> GetExpertByJobId(int jobId, string email, string phone, string schOn = "");
        List<TempExpertAvailibity> GetExpertAvailbilityByDomain(int domainId, DateTime date);
        int GetExpertCountBasedOnDomain(int domaindId);
        string GetLessAssignExpertCurrMonth(List<string> domainExpertsID, DateTime SchOn);
        int AddExpertRequirements(ExpertRequrement expertRequrement);
        void AddNotification(Notification notif);
        string GetExpertAllocationAllowOrNot(string expId, DateTime? schOn);
        Expert GetExpertDetailsById(string Id);
        InterviewSchedule CheckInterviewCode(string code);
        int GetCustomerCostByCndTimezone(int CId, string TimeZone, string expId);
        string AddSchedule(InterviewSchedule interview);
        int AddScheduledCodeChallanges(ScheduledCodeChallange challange);
        List<LanguageSkillMapping> GetCodingSkillsByJobID(int jobid);
        List<QuestionBank> GetCodingQuestionsBySkill(int skillid);
        int UpdateExprtReqBySchId(int schid, string resolvedby, DateTime resolvedon);
        InterviewSchedule GetInterviewSchData(int candid);
        int AddScheduleNotes(ScheduleNote schnote);
        bool IsReminderSentCand(int id);
        bool IsReminderSentExpert(int id);
        bool IsExpert(string phone, string email);
        bool IsCandidateExist(string phone, string email, int jobid);
        bool IsReminderMsgSentCand(int id);
        bool IsReminderMsgSentExpert(int id);
       
        bool IsScheduleNoteAdded(int id, string msg);
        List<ScheduleDetailsList> GetTodaysSchedules();
    }
}
