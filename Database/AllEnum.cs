using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AllEnum
    {
        public enum ScheduleStatusEnum
        {
            New = 1,
            Scheduled = 2,
            [Description("Feedback Submitted")]
            FeedbackSubmitted = 3,
            [Description("MediaUploaded Submitted")]
            MediaUploaded = 4,
            Completed = 5,
            Cancelled = 6,
            Parked = 7,
            RescheduleRequested = 8,
            PendingWithClient = 9
        }
        public static class WhatsAppCampaign
        {
            public static readonly string ExpertAvailability = "ExpertAvailability";
            public static readonly string ExpertInterviewReminder = "ExpertInterviewReminder";
            public static readonly string ParkedExpertApproved = "ParkedExpertApproved";
            public static readonly string CandidateAvailability = "CandidateAvailabilityNew";
            public static readonly string ExpertInterviewCancel = "ExpertInterviewCancel";
            public static readonly string ExpertFeedbackReminder = "ExpertFeedbackReminder";
            public static readonly string ExpertInterviewInvite = "ExpertInterviewInvite";
            public static readonly string CandidateInterviewReminder = "CandidateInterviewReminder";
            public static readonly string CandidateInterviewSchedule = "CandidateInterviewSchedule";
            public static readonly string ExpertReminderForAllInterview = "ExpertReminderForAllInterview";
            public static readonly string CandidateSupportMsg = "CandidateSupportMsg";
            public static readonly string ExpertHelpMsg = "ExpertHelpMsg";


        }
        public enum HolidayEnum
        {
            [Description("Mandatory")]
            Mandatory = 1,
            [Description("Optional")]
            Optional = 2
        }
        public enum CalibrationSchStatusEnum
        {

            Scheduled = 1,
            Joined = 2,
            [Description("Not Joined")]
            NotJoined = 3,
            Cancelled = 4,
            Completed = 5,
        }
        public enum CodingTestStatusEnum
        {
            NotRequired = 0,
            NotYetDone = 1,
            Completed = 2
        }
        public enum JobTypeEnum
        {
            Normal = 1,
            [Display(Name = "Coding Round")]
            CodingRound = 2
        }
    }
}
