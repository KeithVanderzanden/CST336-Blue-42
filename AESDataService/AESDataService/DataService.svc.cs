using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AESDataService
{
    public class DataService : IDataService
    {
        #region Query Methods
        public List<Job> getJobs(int storeId)
        {
            var jobs = new List<Job>();

            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                var city = (from c in context.AvailablePositions where storeId == c.storeId select c);

                foreach (var p in city)
                {
                    var temp = new Job();
                    temp.title = p.Position.title;
                    temp.description = p.Position.description;
                    temp.requirements = p.Position.requirements;
                    temp.education = p.Position.education;
                    temp.pay = p.Position.pay;
                    temp.mondayFrom = p.mondayFrom;
                    temp.mondayTo = p.mondayTo;
                    temp.tuesdayFrom = p.tuesdayFrom;
                    temp.tuesdayTo = p.tuesdayTo;
                    temp.wednesdayFrom = p.wednesdayFrom;
                    temp.wednesdayTo = p.wednesdayTo;
                    temp.thursdayFrom = p.thursdayFrom;
                    temp.thursdayTo = p.thursdayTo;
                    temp.fridayFrom = p.fridayFrom;
                    temp.fridayTo = p.fridayTo;
                    temp.saturdayFrom = p.saturdayFrom;
                    temp.saturdayTo = p.saturdayTo;
                    temp.sundayFrom = p.sundayFrom;
                    temp.sundayTo = p.sundayTo;
                    temp.availablePositionId = p.availablePosId;
                    temp.location = getStoreLocation(p.positionId);
                    jobs.Add(temp);
                }
                return jobs;
            }
        }

        public Store getStoreInfo(int storeId)
        {
            Store aStore = new Store();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                var store = (from p in context.Stores where storeId == p.storeId select p);
                foreach (var x in store)
                {
                    aStore.name = x.name;
                    aStore.street = x.street;
                    aStore.city = x.city;
                    aStore.zip = x.zip;
                    aStore.state = x.state;
                    aStore.storeId = x.storeId;
                }
            }
            return aStore;
        }

        public List<Question> getQuestions(List<int> positionIds)
        {
            if (positionIds == null)
                return null;

            List<Question> questions = new List<Question>();

            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                foreach (int id in positionIds)
                {
                    var question = (from q in context.Questionaires where q.positionId == id group q by new { q.Question } into g select g).ToList();

                    foreach (var x in question)
                    {
                        bool addme = true;
                        Question temp = new Question();
                        temp.theQuestion = x.Key.Question.theQuestion;
                        temp.theAnswer = x.Key.Question.theAnswer;
                        foreach (var ques in questions)
                        {
                            if (temp.theQuestion.CompareTo(ques.theQuestion) == 0)
                            {
                                addme = false;
                            }
                        }
                        if (addme)
                            questions.Add(temp);
                    }
                }
            }
            return questions;
        }

        public string getStoreLocation(int jobId)
        {
            string cityState;
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                var availablePos = (from l in context.AvailablePositions where l.positionId == jobId select l).First<AvailablePosition>();
                var location = (from l in context.Stores where availablePos.storeId == l.storeId select l).First<Store>();   //To get the store city related to the storeID passed in.
                cityState = new StringBuilder(location.city + ", " + location.state).ToString();
            }
            return cityState;
        }

        public int getApplicantId(string ssn)
        {
            int appId = -1;
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.PersonalInfoes.Any(o => o.socialNum == ssn))
                {
                    appId = (from p in context.PersonalInfoes
                             where p.socialNum == ssn
                             select p.applicantId).First();
                }
            }
            return appId;
        }

        public int authenticateUser(string ssn, string password)
        {
            int authenticated = -1;
            int applicantId = getApplicantId(ssn); //cannot directly return this or password is not checked, yes it is set to -1 if applicant does not exist.
            if (applicantId > 0)
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    if (context.ApplicantAuths.Any(o => o.applicantId == applicantId && o.password == password))
                    {
                        authenticated = applicantId;
                    }
                }
            }
            return authenticated;
        }


        public string authenticateManager(string userName, string password)
        {
            string perm = "None";
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.Managers.Any(o => o.userName == userName && o.password == password))
                {
                    var info = (from q in context.Managers where q.userName == userName && q.password == password select q).First();
                    perm = info.permission;
                }
            }
            return perm;
        }

        public string getManagerName(string userName, string password)
        {
            string managerName = String.Empty;
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.Managers.Any(o => o.userName == userName && o.password == password))
                {
                    managerName = (from q in context.Managers where q.userName == userName && q.password == password select q.userName).First();
                }
            }
            return managerName;
        }

        public List<int> getJobsAppliedFor(int applicantId)
        {
            var jobsAppliedFor = new List<int>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.JobsAppliedFors.Any(o => o.appId == applicantId))
                {
                    var ids = (from q in context.JobsAppliedFors where q.appId == applicantId select q.posId).ToList();
                    foreach (var id in ids)
                    {
                        jobsAppliedFor.Add(id);
                    }
                }
            }
            return jobsAppliedFor;
        }
        
        public PersonalInfo getPersonalInfo(int applicantId)
        {
            var personalInfo = new PersonalInfo();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.PersonalInfoes.Any(o => o.applicantId == applicantId))
                {
                    var info = (from q in context.PersonalInfoes where q.applicantId == applicantId select q).First();
                    personalInfo.alias = info.alias;
                    personalInfo.applicantId = info.applicantId;
                    personalInfo.city = info.city;
                    personalInfo.firstName = info.firstName;
                    personalInfo.lastName = info.lastName;
                    personalInfo.middleName = info.middleName;
                    personalInfo.Phone = info.Phone;
                    personalInfo.email = info.email;
                    personalInfo.socialNum = info.socialNum;
                    personalInfo.state = info.state;
                    personalInfo.street = info.street;
                    personalInfo.zip = info.zip;
                }
            }
            return personalInfo;
        }
        
        public Availability getAvailability(int applicantId)
        {
            var availability = new Availability();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.Availabilities.Any(o => o.applicantId == applicantId))
                {
                    var info = (from q in context.Availabilities where q.applicantId == applicantId select q).First();
                    availability.applicantId = info.applicantId;
                    availability.daysYN = info.daysYN;
                    availability.eveningsYN = info.eveningsYN;
                    availability.fridayFrom = info.fridayFrom;
                    availability.fridayTo = info.fridayTo;
                    availability.fullTimeYN = info.fullTimeYN;
                    availability.mondayFrom = info.mondayFrom;
                    availability.mondayTo = info.mondayTo;
                    availability.salaryExpected = info.salaryExpected;
                    availability.saturdayFrom = info.saturdayFrom;
                    availability.saturdayTo = info.saturdayTo;
                    availability.sundayFrom = info.sundayFrom;
                    availability.sundayTo = info.sundayTo;
                    availability.thursdayFrom = info.thursdayFrom;
                    availability.thursdayTo = info.thursdayTo;
                    availability.tuesdayFrom = info.tuesdayFrom;
                    availability.tuesdayTo = info.tuesdayTo;
                    availability.wednesdayFrom = info.wednesdayFrom;
                    availability.wednesdayTo = info.wednesdayTo;
                    availability.weekendsYN = info.weekendsYN;
                }
            }
            return availability;
        }
        
        public List<Education> getEducation(int applicantId)
        {
            var education = new List<Education>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.Educations.Any(o => o.applicantId == applicantId))
                {
                    var info = (from q in context.Educations where q.applicantId == applicantId select q).ToList();
                    foreach (var item in info)
                    {
                        var ed = new Education();
                        ed.applicantId = item.applicantId;
                        ed.city = item.city;
                        ed.degreeMajor = item.degreeMajor;
                        ed.educationId = item.educationId;
                        ed.graduatedYN = item.graduatedYN;
                        ed.name = item.name;
                        ed.stateAbrev = item.stateAbrev;
                        ed.street = item.street;
                        ed.yearFrom = item.yearFrom;
                        ed.yearTo = item.yearTo;
                        ed.zip = item.zip;
                        education.Add(ed);
                    }
                }
            }
            return education;
        }

        public List<Reference> getReferences(int applicantId)
        {
            var references = new List<Reference>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.References.Any(o => o.applicantId == applicantId))
                {
                    var refs = (from q in context.References where q.applicantId == applicantId select q).ToList();
                    foreach (var r in refs)
                    {
                        var refer = new Reference();
                        refer.applicantId = r.applicantId;
                        refer.company = r.company;
                        refer.name = r.name;
                        refer.phone = r.phone;
                        refer.referenceId = r.referenceId;
                        refer.title = r.title;
                        references.Add(refer);
                    }
                }
            }
            return references;
        }

        public List<JobHistory> getJobHistories(int applicantId)
        {
            var histories = new List<JobHistory>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.JobHistories.Any(o => o.applicantId == applicantId))
                {
                    var items = (from q in context.JobHistories where q.applicantId == applicantId select q).ToList();
                    foreach (var item in items)
                    {
                        var hist = new JobHistory();
                        hist.applicantId = item.applicantId;
                        hist.city = item.city;
                        hist.duties = item.duties;
                        hist.employer = item.employer;
                        hist.endSalary = item.endSalary;
                        hist.fromDate = item.fromDate;
                        hist.jobHistoryId = item.jobHistoryId;
                        hist.phone = item.phone;
                        hist.position = item.position;
                        hist.reasonLeave = item.reasonLeave;
                        hist.startSalary = item.startSalary;
                        hist.stateAbrev = item.stateAbrev;
                        hist.street = item.street;
                        hist.supervisor = item.supervisor;
                        hist.toDate = item.toDate;
                        hist.zip = item.zip;
                        histories.Add(hist);
                    }
                }
            }
            return histories;
        }

        public ElectronicSig getESignature(int applicantId)
        {
            var eSig = new ElectronicSig();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.ElectronicSigs.Any(o => o.applicantId == applicantId))
                {
                    var item = (from q in context.ElectronicSigs where q.applicantId == applicantId select q).First();
                    eSig.applicantId = item.applicantId;
                    eSig.date = item.date;
                }
            }
           return eSig;
        }

        public ApplicantApp getApplication(int applicantId)
        {
            var app = new ApplicantApp();
            try
            {
                app.availability = getAvailability(applicantId);
                app.education = getEducation(applicantId);
                app.electronicSig = getESignature(applicantId);
                app.jobHistory = getJobHistories(applicantId);
                app.personalInfo = getPersonalInfo(applicantId);
                app.reference = getReferences(applicantId);
                app.notes = getNotes(applicantId);
                app.jobsApplied = getJobsfromID(applicantId);
            }
            catch (Exception) { }
            return app;
        }

        /*New Search Methods
        [OperationContract]
        List<int> getApplicationsWithStoreID(int storeId);
        [OperationContract]
        List<int> getApplicationsWithJobOpeningID(int jobOpeningId);
        [OperationContract]
        List<int> getApplicationsWithName(string firstName, string lastName);
        */

        //Updated search methods to remove duplicate records.
        public List<int> getApplicationsWithStoreID(int storeId)
        {
            var applications = new List<int>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.Applications.Any(o => o.storeId == storeId))
                {
                    var apps = (from q in context.Applications where q.storeId == storeId select q);
                    var noDups = apps.GroupBy(x => x.applicantId).Select(y => y.FirstOrDefault());
                    foreach (var app in noDups)
                    {
                        applications.Add(app.applicantId);
                    }
                }
            }
            return applications;
        }
        public List<int> getApplicationsWithJobOpeningID(int jobOpeningId)
        {
            var applications = new List<int>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.Applications.Any(o => o.availablePosId == jobOpeningId))
                {
                    var apps = (from q in context.Applications where q.availablePosId == jobOpeningId select q);
                    var noDups = apps.GroupBy(x => x.applicantId).Select(y => y.FirstOrDefault());
                    foreach (var app in noDups)
                    {
                        applications.Add(app.applicantId);
                    }
                }
            }
            return applications;
        }
        public List<int> getApplicationsWithName(string firstName, string lastName)
        {
            var applications = new List<int>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                var apps = (from p in context.Applications where p.PersonalInfo.firstName == firstName && p.PersonalInfo.lastName == lastName select p);
                var noDups = apps.GroupBy(x => x.applicantId).Select(y => y.FirstOrDefault());
                foreach (var app in noDups)
                {
                    applications.Add(app.applicantId);
                }
            }
            return applications;
        }

        public List<Applicant> getApplicationsWithStatus(string status)
        {
            var applications = new List<Applicant>();
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                var apps = (from q in context.Applications where q.status == status && q.locked == false select q);
                var noDups = apps.GroupBy(x => x.applicantId).Select(y => y.FirstOrDefault());
                foreach (var app in noDups)
                {
                    Applicant a = new Applicant();
                    a.id = app.applicantId;
                    a.fullName = app.PersonalInfo.firstName + " " + app.PersonalInfo.lastName;
                    a.locked = app.locked;
                    a.note = app.notes;
                    applications.Add(a);
                }
            }

            return applications;
        }

        public string getNotes(int appId)
        {
            string appNotes = "";
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                appNotes = (from q in context.Applications where q.applicantId==appId select q.notes).First();
            }

            return appNotes;
        }
        /********************/

        public string getJobsfromID(int appId)
        {
            string temp = "";
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    var x = (from p in context.JobsAppliedFors where p.appId == appId select p).ToList();
                    foreach(var job in x)
                    {
                        temp += (from p in context.Positions where p.positionId == job.posId select p.title).First();
                        temp += ", ";
                    }
                }
            }
            catch (Exception) { }
            return temp;
        }
        #endregion

        #region Create Methods
        private int storePersonalInfo(PersonalInfo personalInfo)
        {
            int appId;

            if (personalInfo == null)
                return -1;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    context.PersonalInfoes.Add(personalInfo);
                    context.SaveChanges();
                    appId = (from p in context.PersonalInfoes
                             where p.socialNum == personalInfo.socialNum &&
                                 p.firstName == personalInfo.firstName &&
                                 p.lastName == personalInfo.lastName
                             select p.applicantId).First();
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return appId;
        }

        private bool storeJobIds(int applicantId, int[] ids)
        {
            bool result = true;

            if (ids == null)
                return false;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    foreach (var id in ids)
                    {
                        var temp = new JobsAppliedFor();
                        temp.appId = applicantId;
                        temp.posId = id;
                        context.JobsAppliedFors.Add(temp);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }


        private bool storeAvailability(Availability availability)
        {
            bool result = true;

            if (availability == null)
                return false;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    context.Availabilities.Add(availability);
                    context.SaveChanges();
                }

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        private bool storeEducations(List<Education> educations)
        {
            bool result = true;

            if (educations == null)
                return false;
            foreach (var education in educations)
            {
                try
                {
                    using (AESDatabaseEntities context = new AESDatabaseEntities())
                    {
                        context.Educations.Add(education);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool storeJobHistory(List<JobHistory> jobHistorys)
        {
            bool result = true;

            if (jobHistorys == null)
                return false;
            foreach (var jobHistory in jobHistorys)
            {
                try
                {
                    using (AESDatabaseEntities context = new AESDatabaseEntities())
                    {
                        context.JobHistories.Add(jobHistory);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool storeReferences(List<Reference> references)
        {
            bool result = true;

            if (references == null)
                return false;
            foreach (var reference in references)
            {
                try
                {
                    using (AESDatabaseEntities context = new AESDatabaseEntities())
                    {
                        context.References.Add(reference);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool storeElectronicSig(ElectronicSig electronicSig)
        {
            bool result = true;

            if (electronicSig == null)
                return false;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    context.ElectronicSigs.Add(electronicSig);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private bool storeAuthentication(ApplicantAuth auth)
        {
            bool result = true;

            if (auth == null)
                return false;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    context.ApplicantAuths.Add(auth);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region Update Methods
        public int updatePersonalInfo(PersonalInfo personalInfo)
        {
            int success = -1;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    if (context.PersonalInfoes.Any(o => o.applicantId == personalInfo.applicantId))
                    {
                        var entry = (from p in context.PersonalInfoes
                                     where p.applicantId == personalInfo.applicantId
                                     select p).First();
                        entry.alias = personalInfo.alias;
                        entry.city = personalInfo.city;
                        entry.firstName = personalInfo.firstName;
                        entry.lastName = personalInfo.lastName;
                        entry.middleName = personalInfo.middleName;
                        entry.email = personalInfo.email;
                        entry.socialNum = personalInfo.socialNum;
                        entry.state = personalInfo.state;
                        entry.street = personalInfo.street;
                        entry.zip = personalInfo.zip;
                        context.SaveChanges();
                        success = entry.applicantId;
                    }
                    else
                    {
                        success = storePersonalInfo(personalInfo);
                    }
                }
            }
            catch (Exception)
            {
                success = -1;
            }
            return success;
        }

        public bool updateJobIds(int applicantId, int[] ids)
        {
            bool success = false;
            success = deleteJobIds(applicantId);
            if (success)
                success = storeJobIds(applicantId, ids);
            return success;
        }

        public bool updateAvailability(Availability availability)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    if (context.Availabilities.Any(o => o.applicantId == availability.applicantId))
                    {
                        var entry = (from p in context.Availabilities
                                     where p.applicantId == availability.applicantId
                                     select p).First();
                        entry.daysYN = availability.daysYN;
                        entry.salaryExpected = availability.salaryExpected;
                        entry.eveningsYN = availability.eveningsYN;
                        entry.fridayFrom = availability.fridayFrom;
                        entry.fridayTo = availability.fridayTo;
                        entry.fullTimeYN = availability.fullTimeYN;
                        entry.mondayFrom = availability.mondayFrom;
                        entry.mondayTo = availability.mondayTo;
                        entry.saturdayFrom = availability.saturdayFrom;
                        entry.saturdayTo = availability.saturdayTo;
                        entry.sundayFrom = availability.sundayFrom;
                        entry.sundayTo = availability.sundayTo;
                        entry.thursdayFrom = availability.thursdayFrom;
                        entry.thursdayTo = availability.thursdayTo;
                        entry.tuesdayFrom = availability.tuesdayFrom;
                        entry.tuesdayTo = availability.tuesdayTo;
                        entry.wednesdayFrom = availability.wednesdayFrom;
                        entry.wednesdayTo = availability.wednesdayTo;
                        entry.weekendsYN = availability.weekendsYN;
                        context.SaveChanges();
                    }
                    else
                    {
                        success = storeAvailability(availability);
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updateEducations(List<Education> educations)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    foreach (var education in educations)
                    {
                        if (context.Educations.Any(o => o.applicantId == education.applicantId && o.educationId == education.educationId))
                        {
                            var entry = (from p in context.Educations
                                         where p.applicantId == education.applicantId &&
                                               p.educationId == education.educationId
                                         select p).First();
                            entry.city = education.city;
                            entry.degreeMajor = education.degreeMajor;
                            entry.graduatedYN = education.graduatedYN;
                            entry.name = education.name;
                            entry.stateAbrev = education.stateAbrev;
                            entry.street = education.street;
                            entry.yearFrom = education.yearFrom;
                            entry.yearTo = education.yearTo;
                            entry.zip = education.zip;
                            context.SaveChanges();
                        }
                        else
                        {
                            success = storeEducations(educations);
                        }
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updateJobHistory(List<JobHistory> jobHistory)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    foreach (var history in jobHistory)
                    {
                        if (context.JobHistories.Any(o => o.applicantId == history.applicantId && o.jobHistoryId == history.jobHistoryId))
                        {
                            var entry = (from p in context.JobHistories
                                         where p.applicantId == history.applicantId &&
                                               p.jobHistoryId == history.jobHistoryId
                                         select p).First();
                            entry.city = history.city;
                            entry.duties = history.duties;
                            entry.employer = history.employer;
                            entry.endSalary = history.endSalary;
                            entry.fromDate = history.fromDate;
                            entry.phone = history.phone;
                            entry.position = history.position;
                            entry.reasonLeave = history.reasonLeave;
                            entry.startSalary = history.startSalary;
                            entry.stateAbrev = history.stateAbrev;
                            entry.street = history.street;
                            entry.supervisor = history.supervisor;
                            entry.toDate = history.toDate;
                            entry.zip = history.zip;
                            context.SaveChanges();
                        }
                        else
                        {
                            success = storeJobHistory(jobHistory);
                        }
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updateReferences(List<Reference> references)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    foreach (var reference in references)
                    {
                        if (context.References.Any(o => o.applicantId == reference.applicantId && o.referenceId == reference.referenceId))
                        {
                            var entry = (from p in context.References
                                         where p.applicantId == reference.applicantId &&
                                               p.referenceId == reference.referenceId
                                         select p).First();
                            entry.company = reference.company;
                            entry.name = reference.name;
                            entry.phone = reference.phone;
                            entry.title = reference.title;
                            context.SaveChanges();
                        }
                        else
                        {
                            success = storeReferences(references);
                        }
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updateElectronicSig(ElectronicSig electronicSig, int[] jobId)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    //Create a list of Application entries based on appId & every jobId applied for
                    List<Application> applicationEntries = new List<Application>();
                    foreach (var x in jobId)
                    {
                        Application temp = new Application();
                        temp.applicantId = electronicSig.applicantId;
                        temp.availablePosId = (from p in context.AvailablePositions where p.availablePosId == x select p.availablePosId).First();
                        temp.storeId = (from p in context.AvailablePositions where p.availablePosId == x select p.storeId).First();
                        temp.status = "new";
                        applicationEntries.Add(temp);
                    }

                    Application[] applicationEntriesArray = applicationEntries.ToArray();
                    //remove entries from applicationEntries that already exist
                    int size = applicationEntriesArray.Count();
                    for (int counter = 0; counter < size; counter++)
                    {
                        Application temp = applicationEntriesArray.ElementAt(counter);
                        if (context.Applications.Any(o => o.availablePosId == temp.availablePosId && o.applicantId == temp.applicantId))
                            applicationEntriesArray[counter] = null;
                    }

                    applicationEntries = applicationEntriesArray.ToList();

                    //add new entries to Applications Table
                    foreach (var entry in applicationEntries)
                    {
                        if(entry != null)
                            context.Applications.Add(entry);
                    }

                    //save new entries
                    context.SaveChanges();

                    if (context.ElectronicSigs.Any(o => o.applicantId == electronicSig.applicantId))
                    {
                        var entry = (from p in context.ElectronicSigs
                                     where p.applicantId == electronicSig.applicantId
                                     select p).First();
                        entry.date = electronicSig.date;
                        context.SaveChanges();
                    }
                    else
                    {
                        success = storeElectronicSig(electronicSig);
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updatePassword(ApplicantAuth auth)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    if (context.ApplicantAuths.Any(o => o.applicantId == auth.applicantId))
                    {
                        var entry = (from p in context.ApplicantAuths
                                     where p.applicantId == auth.applicantId
                                     select p).First();
                        entry.password = auth.password;
                        context.SaveChanges();
                    }
                    else
                    {
                        success = storeAuthentication(auth);
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updateStatus(int appId, string status = "new")
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    var application = (from p in context.Applications where p.applicantId == appId select p);
                    foreach (var app in application)
                    {
                        app.status = status;
                    }
                    context.SaveChanges();
                }
            }
            catch(Exception)
            {
                success = false;
            }
            return success;
        }

        public bool updateNotes(int appId, string notes = "")
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    var application = (from p in context.Applications where p.applicantId == appId select p);
                    foreach (var x in application)
                    {
                        x.notes = notes;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool lockApp(int appId)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    var appLock = (from p in context.Applications where appId == p.applicantId select p);
                    foreach(var x in appLock)
                    {
                        x.locked = true;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception) { success = false; }
            return success;
        }
        public bool unlockApp(int appId)
        {
            bool success = true;
            try
            {
                using (AESDatabaseEntities context = new AESDatabaseEntities())
                {
                    var appLock = (from p in context.Applications where appId == p.applicantId select p);
                    foreach (var x in appLock)
                    {
                        x.locked = false;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception) { success = false; }
            return success;
        }
        #endregion

        #region Delete Methods
        public bool deleteJobIds(int applicantId)
        {
            bool success = false;
            using (AESDatabaseEntities context = new AESDatabaseEntities())
            {
                if (context.JobsAppliedFors.Any(o => o.appId == applicantId))
                {
                    var entry = (from q in context.JobsAppliedFors where q.appId == applicantId select q).ToList();
                    foreach (var jobAppliedFor in entry)
                    {
                        context.JobsAppliedFors.Remove(jobAppliedFor);
                        context.SaveChanges();
                    }
                    success = true;
                }
                else
                {
                    success = true;
                }
            }
            return success;
        }
        #endregion
    }
}
