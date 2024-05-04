using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.OModels;
using Database.Contracts;
using static Database.AllEnum;
using System.Globalization;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace Database.Implementation
{
    public class TechnicalDAL : ITechnicalDAL
    {
        private readonly VProPleContext _context;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public TechnicalDAL(VProPleContext context)
        {
            this._context = context;
        }
        public List<InterviewSchedule> GetComing90InterviewSchedules()
        {
            DateTime utcNow = DateTime.UtcNow;

            // Convert to Indian Standard Time (IST)
            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            // Get IST time now
            DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);
            // current time
            DateTime fromDate = istNow;
            // add 90 minutes to current time
            DateTime toDate = istNow.AddMinutes(90);
            var data = (from i in _context.InterviewSchedules
                        where i.Status == 2 && i.ScheduledOn >= fromDate && i.ScheduledOn <= toDate
                        select new InterviewSchedule
                        {
                            Id = i.Id,
                            CandidateId = i.CandidateId,
                            ScheduledOn = Convert.ToDateTime(i.ScheduledOn != null ? i.ScheduledOn.ToString() : null),
                            JobId = i.JobId,
                            VproPleSpoc = i.VproPleSpoc,
                            UniqueCode = i.UniqueCode,
                            IsCandidateConfirmByS = i.IsCandidateConfirmByS,
                            IsCandidateConfirm = i.IsCandidateConfirm,
                            ExpertId = i.ExpertId,
                            IsExpertConfirmByS = i.IsExpertConfirmByS,
                            IsExpertConfirm = i.IsExpertConfirm,
                        }).ToList();
            return data;
        }
        public Candidate GetCandidateDetails(int Id)
        {
            Candidate c = _context.Candidates.Where(x => x.CandidateId == Id).FirstOrDefault();
            return c;
        }

        public Customer GetCustomerDetails(int id)
        {
            return _context.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
        }
        public Spoc GetSpocDetails(int id)
        {
            return _context.Spocs.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool IsReschedule(int id)
        {
            List<InterviewHistory> hstdatasch = _context.InterviewHistories.Where(y => y.Notes.Contains("Interview Scheduled On") && y.InterviewId == id).ToList();
            if (hstdatasch.Count() >= 1)
                return true;
            else
                return false;
        }
        public bool CheckConfirmUniqueCode(string uniqcode)
        {
            InterviewSchedule schedule = _context.InterviewSchedules.Where(x => x.UniqueCode == uniqcode).FirstOrDefault();
            if (schedule != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int AddUniqCode(int IId, string ucode)
        {
            InterviewSchedule sch = _context.InterviewSchedules.Where(x => x.Id == IId).FirstOrDefault();
            sch.UniqueCode = ucode;
            int issave = _context.SaveChanges();
            return issave;
        }
        public List<string> GetCandidateOwnerEmails(int candidateId, int jobId)
        {
            List<string> emails = new List<string>();

            var email = (from p in _context.Candidates
                         join q in _context.Users on
                         p.Owner equals q.UserId
                         where p.CandidateId == candidateId
                         select q.Email.ToLower()).ToList();
            if (email.Count > 0)
                emails.Add(email[0].Trim());
            JobRequirement jb = GetJobRequirement(jobId);
            if (jb.Hremail != null)
            {
                jb.Hremail.Split(',').ToList().ForEach(c =>
                {
                    if (c.Trim() != "")
                        emails.Add(c.Trim().ToLower());
                });
            }
            if (emails.Count() == 0)
                emails.AddRange(GetJobOwnersEmails(jobId));
            return emails.Distinct().ToList();
        }
        public JobRequirement GetJobRequirement(int Id)
        {
            var job = _context.JobRequirements.Where(x => x.JobId == Id).FirstOrDefault();
            //for getting domain name
            if (job != null)
                job.Domain = _context.Domains.Where(x => x.DomainId == job.DomainId).FirstOrDefault();
            return job;
        }
        public List<string> GetJobOwnersEmails(int jobId)
        {
            return (from p in _context.JobOwners
                    join q in _context.Users on p.UserId equals q.UserId
                    where p.JobId == jobId
                    select q.Email.Trim().ToLower()).ToList();
        }
        public CustomerEmailConfig GetEmailConfig(int cId)
        {
            CustomerEmailConfig emailConfig = new CustomerEmailConfig();
            var data = (from p in _context.Customers
                        join q in _context.CustomerEmailConfigs on p.CustomerId equals q.CustomerId
                        where q.CustomerId == cId && q.IsActive == true && p.IsEmailConfigured == true
                        select q).FirstOrDefault();
            if (data != null) { return data; }
            return null;
        }
        private DateTime EndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
        private DateTime StartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public Expert GetExpertId(string Id)
        {
            return _context.Experts.Where(x => x.Id == Id).FirstOrDefault();
        }
        public CandidateFeedback GetCandidateFeedbackBySchedule(int schduleId)
        {
            return _context.CandidateFeedbacks.Where(x => x.ScheduleId == schduleId).FirstOrDefault();
        }


        public int AddScheduleNotes(ScheduleNote schnote)
        {
            _context.ScheduleNotes.Add(schnote);
            return _context.SaveChanges();
        }

        public List<CandidateDetails> GetAllParkedCandidate()
        {

            var data = (from i in _context.InterviewSchedules
                        join c in _context.Candidates on i.CandidateId equals c.CandidateId
                        where i.Status == 7
                        orderby c.CvreceivedDate descending
                        select new CandidateDetails
                        {
                            SchId = i.Id,
                            CandidateId = c.CandidateId,
                            Name = c.Name,
                            Phone = c.Phone,
                            CountryCode = c.CountryCode,
                            Email = c.Email,
                            ResumePath = c.ResumePath,
                            AvailableTime = c.AvailableTime != null ? c.AvailableTime.ToString() : null,
                            JobId = i.JobId,
                            Timezone = c.TimeZone,
                            RelExp = c.RelExp
                        }).ToList();
            return data;
        }
        public int CheckScheduleStatus(int schid)
        {
            return (int)_context.InterviewSchedules.Where(y => y.Id == schid).Select(x => x.Status).FirstOrDefault();
        }

        public int AddInterviwHistory(InterviewHistory history)
        {
            history.Date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            _context.InterviewHistories.Add(history);
            _context.SaveChanges();
            return 1;
        }
        public int ParkedToNewCandidate(Candidate candidate)
        {
            Candidate cd = _context.Candidates.Where(x => x.CandidateId == candidate.CandidateId).FirstOrDefault();
            Expert chk = _context.Experts.Where(x => x.EmailId.Trim().ToLower() == candidate.Email.Trim().ToLower() || x.Phone.Trim() == candidate.Phone.Trim()).FirstOrDefault();
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            cd.Name = textInfo.ToTitleCase(candidate.Name);
            cd.Status = candidate.Status;
            cd.CreatedBy = "VEmp101";
            if (chk != null)
                cd.IsExpert = true;
            _context.Candidates.Update(cd);
            _context.SaveChanges();
            return cd.CandidateId;
        }
        public int UpdateScheduleStatus(int schId, ScheduleStatusEnum interviewStatus)
        {
            InterviewSchedule interview = _context.InterviewSchedules.Where(x => x.Id == schId).FirstOrDefault();
            interview.Status = (int)interviewStatus;

            if (interviewStatus == ScheduleStatusEnum.New)
            {
                interview.ExpertId = null;
                setAvilableScheduler(interview.CandidateId);
            }
            return _context.SaveChanges();

        }
        private void setAvilableScheduler(int candId)
        {
            var curtime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            Candidate candidate = _context.Candidates.Where(x => x.CandidateId == candId).FirstOrDefault();
            if (candidate != null)
            {
                var avalScheduers = _context.Spocavailabilities.Where(x => x.Date == curtime.Date
                && curtime.TimeOfDay >= x.FromTime
                && x.ToTime >= curtime.TimeOfDay).ToList();

                var grpAssignTo = (from p in _context.Candidates
                                   join q in _context.InterviewSchedules on p.CandidateId equals q.CandidateId
                                   where q.Status == (int)ScheduleStatusEnum.New
                                   group p by p.AssignedTo into grp
                                   select new
                                   {
                                       Name = grp.Key,
                                       Count = grp.Count()
                                   }).ToList();

                var sch = (from a in avalScheduers
                           join b in grpAssignTo on a.CreatedBy equals b.Name into gj
                           from grp in gj.DefaultIfEmpty()
                           select new
                           {
                               AssignTo = a.CreatedBy,
                               Count = (grp == null ? 0 : grp.Count)
                           }).ToList();

                if (sch.Count() > 0)
                    candidate.AssignedTo = sch[0].AssignTo;
                else if (avalScheduers.Count() > 0)
                    candidate.AssignedTo = avalScheduers[0].CreatedBy;
                else
                    candidate.AssignedTo = null;
                _context.SaveChanges();
            }
        }

        public List<Skill> GetAllSkills()
        {
            return _context.Skills.ToList();
        }
        public List<string> GetJobSkills(int jobId)
        {
            var jobSkills = (from p in _context.JobSkills
                             join q in _context.Skills on p.SkillId equals q.Id
                             where p.JobId == jobId && p.IsActive == true
                             select q.Name.Trim()).ToList();
            return jobSkills;
        }
        public JobReqModel GetJobDetails(int id)
        {
            return _context.JobRequirements.Where(x => x.JobId == id).Select(x => new JobReqModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.Name,
                Description = x.Description,
                JobId = x.JobId,
                Position = x.Position,
                CustomerImg = x.Customer.Image,
                CustomerUrl = x.Customer.Url,
                DomainName = x.Domain.DomainName,
                DomainId = x.Domain.DomainId,
                IsCandidateEmail = (bool)x.IsCandidateEmail,
                JobType = x.JobType,
                CodingQuestions = x.NoOfChallanges != null ? (int)x.NoOfChallanges : 0,
            }).FirstOrDefault();
        }
        public User GetEmployeesDetails(string userid)
        {
            return _context.Users.Where(x => x.UserId == userid).FirstOrDefault();
        }
        public void AddWhatsAppLog(WhatsAppLog obj)
        {
            DateTime fromdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).AddHours(-48);
            var walog = _context.WhatsAppLogs.Where(x => x.CreatedOn < fromdt).ToList();
            if (walog.Count > 0)
            {
                _context.RemoveRange(walog);
                _context.SaveChanges();
            }
            _context.WhatsAppLogs.Add(obj);
            _context.SaveChanges();
        }
        public string GetAuthSchedulerSetting()
        {
            PortalConfig pc = _context.PortalConfigs.Where(x => x.Id == 1).FirstOrDefault();
            if (pc != null)
            {
                return pc.Value;
            }
            return "";
        }
        private DateTime StartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }
        private DateTime EndOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }
        public bool ClientAgreementExpired(int cid, DateTime schon)
        {
            DateTime fromDate = StartOfDay(schon);
            var customer = _context.Customers.Where(x => x.CustomerId == cid).FirstOrDefault();

            if (customer.AgreementDate == null)
            {
                return true;
            }
            if (customer.AgreementDate < fromDate)
            {
                return true;
            }
            return false;
        }
        public bool CheckHoliday(DateTime schon)
        {
            var data = _context.HolidayLists.Where(x => x.Date.Date == schon.Date && x.Type == (int)HolidayEnum.Mandatory).FirstOrDefault();

            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public InterviewSchedule GetScheduleByCandidateId(int id)
        {
            return _context.InterviewSchedules.Where(x => x.CandidateId == id).FirstOrDefault();
        }
        public List<int> GetHostAvailbilityByTime(DateTime sch_time)
        {
            TimeSpan ts = sch_time.TimeOfDay;
            var spocIds = _context.InterviewSchedules.Where(x => x.ScheduledOn == sch_time && x.Status == 2).Select(x => x.VproPleSpoc).ToList();
            DateTime dtStart = StartOfMonth(sch_time);
            DateTime deEnd = EndOfMonth(sch_time);

            List<int> data = (from s in _context.Spocs
                              join u in _context.Spocavailabilities on s.Id equals u.SpocId
                              where u.OnLeave == null && u.Date == sch_time.Date && ts >= u.FromTime
                              && u.ToTime >= ts && !spocIds.Contains(s.Id)
                              select s.Id).Distinct().ToList();
            var schData = new List<Data>();
            data.ForEach(x =>
            {
                Data dt = new Data();
                dt.Rescheduled = x;
                dt.Value = (from p in _context.InterviewSchedules
                            where ((DateTime)p.ScheduledOn).Date == sch_time.Date
                            && p.Status >= 2 && p.Status <= 5 && p.VproPleSpoc == x
                            select p.Id).Count();
                schData.Add(dt);
            });
            schData = schData.OrderBy(x => x.Value).ToList();

            return schData.Select(x => Convert.ToInt32(x.Rescheduled)).ToList();
        }
        public string GetSPOCName(int spocId)
        {
            return _context.Spocs.Where(x => x.Id == spocId).Select(x => x.Name).FirstOrDefault();
        }
        public int CheckCustomerExpert(int jobId)
        {
            var cuexpcount = _context.CustomerSelectedExperts.Where(x => x.JobId == jobId).Count();
            return cuexpcount;

        }
        public List<Expert> GetExpertByJobId(int jobId, string email, string phone, string schOn = "")
        {
            JobRequirement job = _context.JobRequirements.Where(x => x.JobId == jobId).FirstOrDefault();
            int domainId = job.DomainId;
            var cuexp = _context.CustomerSelectedExperts.Where(x => x.JobId == jobId).Count();
            List<Expert> lstExpert = new List<Expert>();
            if (cuexp == 0)
            {
                //Get the expert from domain
                lstExpert = (from p in _context.Experts
                             join ed in _context.ExpertDomains on p.Id equals ed.ExpertId
                             where ed.DomainId == domainId && p.StatusId != 107
                             && (p.EmailId.Trim().ToLower() != email.Trim().ToLower() || p.Phone.Trim() != phone.Trim())
                             select new Expert
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Phone = p.Phone,
                                 YrsExp = p.YrsExp,
                                 CurrentRate = p.CurrentRate
                             }).ToList();
            }
            else
            {
                lstExpert = (from p in _context.Experts
                             join ed in _context.ExpertDomains on p.Id equals ed.ExpertId
                             join cu in _context.CustomerSelectedExperts on p.Id equals cu.ExpertId
                             where ed.DomainId == domainId && p.StatusId != 107 && cu.JobId == jobId
                             && (p.EmailId.Trim().ToLower() != email.Trim().ToLower() || p.Phone.Trim() != phone.Trim())
                             select new Expert
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Phone = p.Phone,
                                 YrsExp = p.YrsExp,
                                 CurrentRate = p.CurrentRate
                             }).ToList();
            }
            if (schOn == "")
                return lstExpert;
            //Create exp id list
            List<string> expId = lstExpert.Select(x => x.Id).ToList();
            DateTime dt1 = Convert.ToDateTime(schOn).AddMinutes(-44);
            DateTime dt2 = Convert.ToDateTime(schOn).AddMinutes(44);
            DateTime interviewtime = Convert.ToDateTime(schOn);
            //Get Expert Sch
            List<string> data = _context.InterviewSchedules.Where(x => x.ScheduledOn >= dt1 && x.ScheduledOn < dt2 && x.Status != 6).Select(x => x.ExpertId).ToList();
            var ss = lstExpert.Where(x => x.Id == "VExp100").FirstOrDefault();
            if (ss != null)
                lstExpert.Remove(ss);
            DateTime start = StartOfDay(Convert.ToDateTime(schOn));
            DateTime end = EndOfDay(Convert.ToDateTime(schOn));

            lstExpert.ForEach(x =>
            {
                x.Rating = _context.InterviewSchedules.Where(y => y.ExpertId == x.Id && (y.ScheduledOn >= start && y.ScheduledOn <= end) && (y.Status >= 2 && y.Status <= 5)).Count();
                var listAvail = _context.ExpertAvailabilities.Where(e => e.ExpertId == x.Id && e.Date == interviewtime.Date && e.Time == "Available").ToList();
                var intervals = new List<TimeInterval>();
                foreach (var ev in listAvail)
                {
                    intervals.Add(new TimeInterval
                    {
                        FromTime = Convert.ToDateTime(new DateTime(ev.Date.Year, ev.Date.Month, ev.Date.Day) + ev.FromTime),
                        ToTime = Convert.ToDateTime(new DateTime(ev.Date.Year, ev.Date.Month, ev.Date.Day) + ev.ToTime)

                    });
                }
                var available = MergeConsecutiveIntervals(intervals);
                var expavail = available.Where(e => e.FromTime.TimeOfDay <= interviewtime.TimeOfDay && interviewtime.TimeOfDay <= e.ToTime.TimeOfDay).FirstOrDefault();
                if (expavail != null)
                {
                    DateTime sdate = expavail.FromTime;
                    DateTime edateObj = expavail.ToTime;
                    DateTime edate = Convert.ToDateTime(edateObj).AddMinutes(-44);

                    if (interviewtime <= edate)
                    {
                        var avail = _context.InterviewSchedules.Where(y => y.ExpertId == x.Id && y.ScheduledOn >= dt1 && y.ScheduledOn < dt2).FirstOrDefault();
                        if (avail != null)
                        {
                            x.ProfileSummary = "Scheduled";
                        }
                        else
                        {
                            x.ProfileSummary = "Available";

                        }
                    }
                    else
                    {
                        x.ProfileSummary = "Not Available";
                    }
                }
                else
                {
                    x.ProfileSummary = "Not Available";
                }
            });
            List<string> calibration = _context.JobCalibrationSchedules.Where(x => x.CalibrationOn >= dt1 && x.CalibrationOn < dt2 && x.Status != (int)CalibrationSchStatusEnum.Cancelled).Select(x => x.ExpertId).ToList();

            lstExpert = lstExpert.Where(x => !calibration.Contains(x.Id)).OrderBy(x => x.ProfileSummary).ToList();
            return lstExpert;
        }
        private static List<TimeInterval> MergeConsecutiveIntervals(List<TimeInterval> intervals)
        {
            if (intervals == null || intervals.Count <= 1)
                return intervals;

            intervals.Sort((x, y) => x.FromTime.CompareTo(y.FromTime));

            var mergedIntervals = new List<TimeInterval>();
            var currentInterval = intervals[0];

            foreach (var interval in intervals)
            {
                if (interval.FromTime <= currentInterval.ToTime)
                {
                    if (interval.ToTime > currentInterval.ToTime)
                        currentInterval.ToTime = interval.ToTime;
                }
                else
                {
                    mergedIntervals.Add(currentInterval);
                    currentInterval = interval;
                }
            }

            mergedIntervals.Add(currentInterval);
            return mergedIntervals;
        }
        public List<TempExpertAvailibity> GetExpertAvailbilityByDomain(int domainId, DateTime date)
        {
            var lstExpert = new List<TempExpertAvailibity>();
            try
            {
                lstExpert = (from p in _context.Experts
                             join q in _context.ExpertAvailabilities on p.Id equals q.ExpertId
                             join e in _context.ExpertDomains on p.Id equals e.ExpertId
                             where (q.Date == date.Date) && p.StatusId != 107 && e.DomainId == domainId && q.FromTime != null && q.Time.Trim() == "Available"
                             select new TempExpertAvailibity
                             {
                                 ExpertId = p.Id,
                                 Date = q.Date,
                                 FromTime = DateTime.Today.Date.Add((TimeSpan)q.FromTime).TimeOfDay,
                                 ToTime = DateTime.Today.Date.Add((TimeSpan)q.ToTime).TimeOfDay,
                                 Cost = p.CurrentRate != null ? Convert.ToInt32(p.CurrentRate) : 0,
                                 Experience = p.YrsExp != null ? Convert.ToInt32(p.YrsExp) : 0,
                             }).Distinct().ToList();

            }
            catch (Exception ex)
            {

            }

            DateTime dtfTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).Date;
            DateTime dtTTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).Date;

            lstExpert = lstExpert.Where(x => date.TimeOfDay >= dtfTime.Add((TimeSpan)x.FromTime).TimeOfDay
                                            && date.TimeOfDay < dtTTime.Add((TimeSpan)x.ToTime).TimeOfDay).ToList();

            DateTime dt1 = Convert.ToDateTime(date).AddMinutes(-44);
            DateTime dt2 = Convert.ToDateTime(date).AddMinutes(44);

            //Get only distinct for that domain not for all
            List<string> dndexps = (from q in _context.ExpertAvailabilities
                                    join e in _context.ExpertDomains on q.ExpertId equals e.ExpertId
                                    where e.DomainId == domainId
                                    && (q.Time.Trim() == "Do Not Disturb" || q.Time == "Not Available") && q.Date == date.Date && q.FromTime < date.TimeOfDay && q.ToTime > date.TimeOfDay
                                    select q.ExpertId).Distinct().ToList();

            lstExpert.RemoveAll(x => dndexps.Contains(x.ExpertId));
            //Get Expert Sch
            List<string> bookedExperts = _context.InterviewSchedules.Where(x => x.ScheduledOn >= dt1 && x.ScheduledOn < dt2 && x.Status != 6).Select(x => x.ExpertId).ToList();

            bookedExperts.ForEach(x =>
            {
                List<TempExpertAvailibity> temp = lstExpert.Where(y => y.ExpertId == x).ToList();
                temp.ForEach(y =>
                {
                    lstExpert.Remove(y);
                });
            });
            return lstExpert;
        }
        public int GetExpertCountBasedOnDomain(int domaindId)
        {
            return _context.ExpertDomains.Where(x => x.DomainId == domaindId).Count();
        }
        public string GetLessAssignExpertCurrMonth(List<string> domainExpertsID, DateTime SchOn)
        {
            int[] statuses = { 2, 3, 4, 5 };

            DateTime dt1 = StartOfMonth(SchOn);
            DateTime dt2 = EndOfMonth(SchOn);
            var d = (from p in _context.Experts.Where(x => domainExpertsID.Contains(x.Id))
                     join c in _context.InterviewSchedules.Where(y => statuses.Contains((int)y.Status)
                        && y.ScheduledOn >= dt1 && y.ScheduledOn <= dt2)
                       on p.Id equals c.ExpertId into j1
                     from j2 in j1.DefaultIfEmpty()
                     select new
                     {
                         ExpertId = p.Id,
                         ChildId = j2 == null ? 0 : 1
                     })
                    .GroupBy(o => o.ExpertId)
                    .Select(o => new { ExpertId = o.Key, Count = o.Sum(p => p.ChildId) }).OrderBy(o => o.Count).ToList();
            if (d.Count() > 0)
            {
                return d[0].ExpertId;
            }
            else
            {
                return domainExpertsID[0];
            }

        }
        public int AddExpertRequirements(ExpertRequrement expertRequrement)
        {
            if (expertRequrement.Id == 0)
            {

                _context.ExpertRequrements.Add(expertRequrement);
                string domainname = (from j in _context.JobRequirements
                                     join d in _context.Domains on j.DomainId equals d.DomainId
                                     where j.JobId == expertRequrement.DomiaId
                                     select d.DomainName).FirstOrDefault();
                Notification notif = new Notification
                {
                    NotificatonTypeId = 4,
                    Title = "Expert Requirement",
                    Description = "Expert Requirement Added for " + domainname,
                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE),
                };
                AddNotification(notif);
                return _context.SaveChanges();
            }
            return 0;

        }
        public void AddNotification(Notification notif)
        {
            DateTime fromdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).AddHours(-24);
            var notiftodel = _context.Notifications.Where(x => x.CreatedOn < fromdt).ToList();
            if (notiftodel.Count > 0)
            {
                var notids = notiftodel.Select(n => n.Id).ToList();
                var notrcpt = _context.NotificationReceipts.Where(r => notids.Contains(r.NotificationId)).ToList();
                if (notrcpt.Count > 0)
                {
                    _context.RemoveRange(notrcpt);
                    _context.SaveChanges();
                }
                _context.RemoveRange(notiftodel);
            }
            _context.Notifications.Add(notif);
            _context.SaveChanges();
        }
        public string GetExpertAllocationAllowOrNot(string expId, DateTime? schOn)
        {
            if (expId == "VExp100")
                return "Free";

            DateTime dt1 = Convert.ToDateTime(schOn).AddMinutes(-44);
            DateTime dt2 = Convert.ToDateTime(schOn).AddMinutes(44);
            var data = _context.InterviewSchedules.Where(x => x.ExpertId == expId && x.ScheduledOn >= dt1 && x.ScheduledOn < dt2 && x.Status != 6).Count();

            if (data > 0)
            {
                return "Blocked";
            }
            return "Free";
        }
        public Expert GetExpertDetailsById(string Id)
        {
            return _context.Experts.Where(x => x.Id == Id).FirstOrDefault();
        }
        public InterviewSchedule CheckInterviewCode(string code)
        {
            return _context.InterviewSchedules.Where(x => x.InterviewCode == code).FirstOrDefault();
        }
        public int GetCustomerCostByCndTimezone(int CId, string TimeZone, string expId)
        {
            var cost = (from c in _context.ExpertRates
                        where c.CustomerId == CId && c.Timezone == TimeZone && c.ExpertId == expId
                        select c).FirstOrDefault();
            if (cost != null)
                return cost.Rate;
            else
                return 0;

        }
        public string AddSchedule(InterviewSchedule interview)
        {
            InterviewSchedule sch = _context.InterviewSchedules.Where(x => x.CandidateId == interview.CandidateId).FirstOrDefault();
            sch.ExpertId = interview.ExpertId;
            sch.ScheduledOn = interview.ScheduledOn;
            sch.Status = interview.Status;
            sch.Meetinglink = interview.Meetinglink;
            sch.VproPleSpoc = interview.VproPleSpoc;
            sch.ExpertCost = interview.ExpertCost;
            sch.CreatedBy = interview.CreatedBy;
            sch.CancelReason = null;
            sch.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            sch.InterviewCode = interview.InterviewCode;
            sch.IsCandidateConfirm = false;
            sch.IsCandidateConfirmByS = false;
            sch.IsExpertConfirm = false;
            sch.IsExpertConfirmByS = false;
            int id = _context.SaveChanges();
            CandidateFeedback feedback = new CandidateFeedback()
            {
                ScheduleId = sch.Id
            };
            CandidateFeedback fed = _context.CandidateFeedbacks.Where(x => x.ScheduleId == sch.Id).FirstOrDefault();
            var jobreq = _context.JobRequirements.Where(x => x.JobId == interview.JobId).Select(y => y.IsCodingTest).FirstOrDefault();
            var status = jobreq == true ? (int)CodingTestStatusEnum.NotYetDone : (int)CodingTestStatusEnum.NotRequired;
            if (fed == null)
            {
                fed = new CandidateFeedback()
                {
                    ScheduleId = sch.Id,
                    CodingTestStatus = status,
                    CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).Date
                };
                _context.CandidateFeedbacks.Add(fed);
                _context.SaveChanges();
            }
            else
            {
                fed.CodingTestStatus = status;
                _context.Update(fed);
                int fid = _context.SaveChanges();
            }
            return fed.Code.ToString();
        }
        public List<LanguageSkillMapping> GetCodingSkillsByJobID(int jobid)
        {
            var js = (from j in _context.JobCodingLanguages where j.JobId == jobid select j.LanguageId).ToList();
            var ljs = (from s in _context.LanguageSkillMappings where js.Contains(s.LanguageId) select s).ToList();
            return ljs;
        }
        public int AddScheduledCodeChallanges(ScheduledCodeChallange challange)
        {
            var data = _context.ScheduledCodeChallanges.Where(x => x.SkillId == challange.SkillId && x.ScheduleId == challange.ScheduleId).ToList();
            if (data.Count() > 0)
                _context.ScheduledCodeChallanges.RemoveRange(data);

            _context.ScheduledCodeChallanges.Add(challange);
            return _context.SaveChanges();
        }
        public List<QuestionBank> GetCodingQuestionsBySkill(int skillid)
        {
            return (from l in _context.QuestionBanks
                    where l.SkillId == skillid && l.IsCoding == true
                    select l).ToList();
        }
        public int UpdateExprtReqBySchId(int schid, string resolvedby, DateTime resolvedon)
        {

            Models.ExpertRequrement requirement = _context.ExpertRequrements.Where(x => x.SchId == schid && x.IsActive == true).FirstOrDefault();
            if (requirement != null)
            {
                requirement.IsActive = false;
                requirement.ResolveBy = resolvedby;
                requirement.ResolveOn = resolvedon;

                return _context.SaveChanges();
            }
            else
                return 0;
        }
        public InterviewSchedule GetInterviewSchData(int candid)
        {
            InterviewSchedule sch = _context.InterviewSchedules.Where(x => x.CandidateId == candid).FirstOrDefault();

            return sch;
        }
        public bool IsReminderSentCand(int id)
        {
            DateTime currentdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime fromDate = StartOfDay(currentdt);
            DateTime toDate = EndOfDay(currentdt);

            List<ScheduleNote> datasch = _context.ScheduleNotes.Where(y => y.Notes.Contains("Reminder Email Sent to Candidate") && y.SchId == id && (y.CreatedOn >= fromDate && y.CreatedOn <= toDate)).ToList();
            if (datasch.Count() >= 1)
                return true;
            else
                return false;
        }
        public bool IsReminderSentExpert(int id)
        {
            DateTime currentdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime fromDate = StartOfDay(currentdt);
            DateTime toDate = EndOfDay(currentdt);

            List<ScheduleNote> datasch = _context.ScheduleNotes.Where(y => y.Notes.Contains("Reminder Email Sent to Expert") && y.SchId == id && (y.CreatedOn >= fromDate && y.CreatedOn <= toDate)).ToList();
            if (datasch.Count() >= 1)
                return true;
            else
                return false;
        }

        public bool IsExpert(string phone, string email)
        {
            var emailCount = _context.Experts.Where(x => x.EmailId.ToLower().Trim() == email.ToLower().Trim()).Count();
            var phoneCount = _context.Experts.Where(x => x.Phone.ToLower().Trim() == phone.ToLower().Trim()).Count();
            if (emailCount > 0 || phoneCount > 0)
                return true;
            else
                return false;
        }

        public bool IsCandidateExist(string phone, string email, int jobid)
        {
            int custId = _context.JobRequirements.Where(x => x.JobId == jobid).Select(y => y.CustomerId).FirstOrDefault();
            var cdata = (from j in _context.JobRequirements
                         join cu in _context.Customers on j.CustomerId equals cu.CustomerId
                         join q in _context.InterviewSchedules on j.JobId equals q.JobId
                         join c in _context.Candidates on q.CandidateId equals c.CandidateId
                         where cu.CustomerId == custId && (c.Email.ToLower().Trim() == email.ToLower().Trim() || c.Phone.Trim() == phone.Trim())
                         select new
                         {
                             cu.CustomerId,
                             c.Name,
                             j.JobId
                         }
                         ).ToList();
            if (cdata.Count > 1)
                return true;
            else
                return false;
        }
        public bool IsReminderMsgSentCand(int id)
        {
            DateTime currentdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime fromDate = StartOfDay(currentdt);
            DateTime toDate = EndOfDay(currentdt);

            List<ScheduleNote> datasch = _context.ScheduleNotes.Where(y => y.Notes.Contains("Reminder Message Sent to Candidate") && y.SchId == id && (y.CreatedOn >= fromDate && y.CreatedOn <= toDate)).ToList();
            if (datasch.Count() >= 1)
                return true;
            else
                return false;
        }
        public bool IsReminderMsgSentExpert(int id)
        {
            DateTime currentdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime fromDate = StartOfDay(currentdt);
            DateTime toDate = EndOfDay(currentdt);

            List<ScheduleNote> datasch = _context.ScheduleNotes.Where(y => y.Notes.Contains("Reminder Message Sent to Expert") && y.SchId == id && (y.CreatedOn >= fromDate && y.CreatedOn <= toDate)).ToList();
            if (datasch.Count() >= 1)
                return true;
            else
                return false;
        }
        
        public bool IsScheduleNoteAdded(int id, string msg)
        {
            DateTime currentdt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime fromDate = StartOfDay(currentdt);
            DateTime toDate = EndOfDay(currentdt);

            List<ScheduleNote> datasch = _context.ScheduleNotes.Where(y => y.Notes.Contains(msg) && y.SchId == id && (y.CreatedOn >= fromDate && y.CreatedOn <= toDate)).ToList();
            if (datasch.Count() >= 1)
                return true;
            else
                return false;
        }
        public List<ScheduleDetailsList> GetTodaysSchedules()
        {
            DateTime currentdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
          
            DateTime fromDate = StartOfDay(currentdate);
            DateTime toDate = EndOfDay(currentdate);
           

            var ScheduleData = (from i in _context.InterviewSchedules
                                join j in _context.JobRequirements on i.JobId equals j.JobId
                                join c in _context.Candidates on i.CandidateId equals c.CandidateId
                                where i.Status == 2 && i.ScheduledOn >= fromDate && i.ScheduledOn <= toDate
                                select new ScheduleDetails
                                {
                                    SchId = i.Id,
                                    CandPhone = "+" + c.CountryCode + " " + c.Phone,
                                    CandName = c.Name,
                                    CandEmail = c.Email,
                                    InterviewOn = (DateTime)i.ScheduledOn,
                                    JobId = i.JobId,
                                    Status = i.IsCandidateConfirm == true || i.IsCandidateConfirmByS == true ? "Confirm" : "Not Confirm",
                                    Position = j.Position,
                                    Owner = c.Owner,
                                }).ToList();
            List<ScheduleDetailsList> list = new List<ScheduleDetailsList>();
            var groupedScheduleData = (from i in ScheduleData
                                       group i by new { Owner = i.Owner, JobId = i.JobId } into g
                                       select new 
                                       {
                                           Owner = g.Key.Owner,
                                           JobId = g.Key.JobId,
                                           ScheduleDetails = g.ToList()

                                       }).ToList();
            var users = _context.Users.Where(x => x.TypeId != 108).ToList();
            var jobowner = (from p in _context.JobOwners
                            join q in _context.Users on p.UserId equals q.UserId
                            where q.TypeId != 108
                            select new
                            {
                                p.JobId,
                                q.Email,
                                q.FirstName
                            }).ToList();
            var interviewhst = _context.InterviewHistories.ToList();
            foreach (var group in groupedScheduleData)
            {
                if (group.Owner != null)
                {
                    ScheduleDetailsList scheduleList = new ScheduleDetailsList();
                    scheduleList.OwnerMail = users.Where(x => x.UserId == group.Owner).Select(y => y.Email).FirstOrDefault();
                    scheduleList.OwnerName = users.Where(x => x.UserId == group.Owner).Select(y => y.FirstName).FirstOrDefault();
                    scheduleList.TimeZone = users.Where(x => x.UserId == group.Owner).Select(y => y.TimeZone).FirstOrDefault();
                    scheduleList.JobOwnerEmails=jobowner.Where(x=>x.JobId==group.JobId).Select(y=>y.Email).ToList();
                    group.ScheduleDetails.ForEach(z =>
                    {
                        int count = interviewhst.Where(y => y.Notes.Contains("Interview Scheduled On") && y.InterviewId == z.SchId).ToList().Count();
                        if (count > 1)
                        {
                            int countfinal = count - 1;
                            z.ScheduleCount = "<span style='color:red;font-weight:800;'> Yes (" + countfinal + ")</span>";
                        }
                        else
                            z.ScheduleCount = "<span style='color:green;font-weight:800;'> No </span>";
                        if(z.Status== "Confirm")
                            z.Status= "<span style='color:green;font-weight:800;'> Yes</span>";
                        else
                            z.Status = "<span style='color:#ffbf00;font-weight:800;'> No</span>";
                    });
                    scheduleList.ScheduleDetails = group.ScheduleDetails;
                    list.Add(scheduleList);
                }
            }
            return list;   
        }
        
    }

}
