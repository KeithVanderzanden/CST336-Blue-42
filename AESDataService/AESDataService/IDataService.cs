using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AESDataService
{
    
    [ServiceContract]
    public interface IDataService
    {
        #region Query Methods
        //Job is defined below in DataContract
        //input: int storeId
        //output: List<Job>
        [OperationContract]
        List<Job> getJobs(int storeId);

        //Store is defined in AESentity.tt->Store.cs
        //input: int storeId
        //output: Store
        [OperationContract]
        Store getStoreInfo(int storeId);

        //Question is defined in AESentity.tt->Question.tt
        //input: List<int> positionIds
        //output: List<Questions>
        [OperationContract]
        List<Question> getQuestions(List<int> positionIds);

        //get the city, state of the store that job identified by jobId is offered at
        //input: jobId
        //output: string (<city>, <stateAbrev>)
        //returns empty string if jobId does not exist
        [OperationContract]
        string getStoreLocation(int jobId);

        //checks if there is an entry in personalInfo with given ssn
        //returns applicantID or -1 if none is found.
        [OperationContract]
        int getApplicantId(string ssn);

        //authenticate an applicant
        //returns -1 if appicant record does not exist, or the applicantId for that ssn
        [OperationContract]
        int authenticateUser(string ssn, string password);

        [OperationContract]
        string authenticateManager(string userName, string password);

        [OperationContract]
        string getManagerName(string userName, string password);

        [OperationContract]
        List<int> getJobsAppliedFor(int applicantId);

        [OperationContract]
        PersonalInfo getPersonalInfo(int applicantId);

        [OperationContract]
        Availability getAvailability(int applicantId);

        [OperationContract]
        List<Education> getEducation(int applicantId);

        [OperationContract]
        List<Reference> getReferences(int applicantId);

        [OperationContract]
        List<JobHistory> getJobHistories(int applicantId);

        [OperationContract]
        ElectronicSig getESignature(int applicantId);

        [OperationContract]
        ApplicantApp getApplication(int applicantId);

        /*New Search Methods*/

        //get list<AppId> based off of storeID
        //input: int storeID
        //output: List<int> appIds
        //returns empty list if none available
        [OperationContract]
        List<int> getApplicationsWithStoreID(int storeId);

        //get list<AppId> based off of jobOpeningId
        //input: int jobOpeningId
        //output: List<int> appIds
        //returns empty list if none available
        [OperationContract]
        List<int> getApplicationsWithJobOpeningID(int jobOpeningId);

        //get list<AppId> based off of firstname & lastname
        //input: string firstname, string lastname
        //output: List<int> appIds
        //returns empty list if none available
        [OperationContract]
        List<int> getApplicationsWithName(string firstName, string lastName);

        [OperationContract]
        List<Applicant> getApplicationsWithStatus(string status);
        [OperationContract]
        string getNotes(int appId);
        [OperationContract]
        string getJobsfromID(int appId);
        /*******************/
        #endregion

        #region Create Methods
        //These methods have been replaced by update methods, the definitions for storing each type
        //still exist but are now private and are called by update methods.  This is done to simplify the 
        //interface an logic on the front end.
        #endregion

        #region Update Methods
        [OperationContract]
        int updatePersonalInfo(PersonalInfo personalInfo);

        [OperationContract]
        bool updateJobIds(int applicantId, int[] ids);

        [OperationContract]
        bool updateAvailability(Availability availability);

        [OperationContract]
        bool updateEducations(List<Education> educations);

        [OperationContract]
        bool updateJobHistory(List<JobHistory> jobHistory);

        [OperationContract]
        bool updateReferences(List<Reference> references);

        [OperationContract]
        bool updateElectronicSig(ElectronicSig electronicSig, int[] jobId);

        [OperationContract]
        bool updatePassword(ApplicantAuth auth);

        [OperationContract]
        bool updateStatus(int appId, int jobId, string status = "new");
        [OperationContract]
        bool updateNotes(int appId, string notes = "");
        [OperationContract]
        bool lockApp(int appId);
        [OperationContract]
        bool unlockApp(int appId);
        #endregion

        #region Delete Methods
        [OperationContract]
        bool deleteJobIds(int applicantId);
        #endregion
    }

    #region Data Contracts
    [DataContract]
    public class Job
    {
        [DataMember]
        public string location { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string requirements { get; set; }
        [DataMember]
        public string education { get; set; }
        [DataMember]
        public string pay { get; set; }
        [DataMember]
        public int availablePositionId { get; set; }
        [DataMember]
        public Nullable<TimeSpan> mondayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> mondayTo { get; set; }
        [DataMember]
        public Nullable<TimeSpan> tuesdayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> tuesdayTo { get; set; }
        [DataMember]
        public Nullable<TimeSpan> wednesdayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> wednesdayTo { get; set; }
        [DataMember]
        public Nullable<TimeSpan> thursdayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> thursdayTo { get; set; }
        [DataMember]
        public Nullable<TimeSpan> fridayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> fridayTo { get; set; }
        [DataMember]
        public Nullable<TimeSpan> saturdayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> saturdayTo { get; set; }
        [DataMember]
        public Nullable<TimeSpan> sundayFrom { get; set; }
        [DataMember]
        public Nullable<TimeSpan> sundayTo { get; set; }
    }

    [DataContract]
    public class ApplicantApp
    {
        [DataMember]
        public PersonalInfo personalInfo { get; set; }
        [DataMember]
        public List<Education> education { get; set; }
        [DataMember]
        public Availability availability {get; set;}
        [DataMember]
        public List<JobHistory> jobHistory { get; set; }
        [DataMember]
        public List<Reference> reference { get; set; }
        [DataMember]
        public ElectronicSig electronicSig { get; set; }
        [DataMember]
        public string notes { get; set; }
        [DataMember]
        public string jobsApplied { get; set; }
    }

    [DataContract]
    public class Applicant
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string fullName { get; set; }
        [DataMember]
        public bool locked { get; set; }
        [DataMember]
        public string note { get; set; }
    }
#endregion
}

     