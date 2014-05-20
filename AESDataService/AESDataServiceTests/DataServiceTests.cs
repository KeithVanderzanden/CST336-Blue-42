using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AESDataService;
using NUnit.Framework;
using AESDataServiceTests.DataServiceReference;

namespace AESDataService.Tests
{
    [TestFixture()]
    public class DataServiceTests
    {
        private DataServiceClient client;

        [TestFixtureSetUp()]
        public void setup()
        {
            client = new DataServiceClient();
            client.Open();
        }
        [TestFixtureTearDown()]
        public void teardown()
        {
            client.Close();
        }
        [Test()]
        public void getJobsTest()
        {
            var j = client.getJobs(1);
            Assert.IsNotNull(j);
        }

        [Test()]
        public void getStoreInfoTest()
        {
            Store s = client.getStoreInfo(1);
            Assert.IsNotNull(s);
        }

        [Test()]
        public void getQuestionsTest()
        {
            int[] posIds = {0,1,2};
            var q = client.getQuestions(posIds);
            Assert.IsNotNull(q);
            int[] posIdsNull = null;
            var qNull = client.getQuestions(posIdsNull);
            Assert.IsNull(qNull);
        }

        [Test()]
        public void getStoreLocationTest()
        {
            string s = client.getStoreLocation(1);
            Assert.AreEqual(s, "Wilsonville, OR");
        }

        [Test()]
        public void getApplicantIdTest()
        {
            int x = client.getApplicantId("123456789");
            Assert.IsInstanceOf<int>(x);
            int y = client.getApplicantId("");
            Assert.AreEqual(-1, y);
            int z = client.getApplicantId(null);
            Assert.AreEqual(-1, z);
        }

        [Test()]
        public void authenticateUserTest()
        {
            int result = client.authenticateUser("123456789", "Apple");
            Assert.AreNotEqual(-1, result);
            result = client.authenticateUser("123456789", "apple");
            Assert.AreEqual(-1, result);
        }

        [Test()]
        public void authenticateManagerTest()
        {
            string result = client.authenticateManager("chris", "Apple");
            Assert.AreNotEqual("None", result);
            result = client.authenticateManager("", "");
            Assert.AreEqual("None", result);
            result = client.authenticateManager(null, null);
            Assert.AreEqual("None", result);
        }

        [Test()]
        public void getJobsAppliedForTest()
        {
            var result = client.getJobsAppliedFor(1);
            Assert.IsNotEmpty(result);
            result = client.getJobsAppliedFor(0);
            Assert.IsEmpty(result);
        }

        [Test()]
        public void getPersonalInfoTest()
        {
            var result = client.getPersonalInfo(1);
            Assert.IsInstanceOf<PersonalInfo>(result);
            Assert.AreEqual(result.applicantId,1);
            Assert.IsNotNull(result.firstName);
            result = client.getPersonalInfo(0);
            Assert.IsInstanceOf<PersonalInfo>(result);
            Assert.IsNull(result.firstName);
        }

        [Test()]
        public void getAvailabilityTest()
        {
            var result = client.getAvailability(1);
            Assert.IsInstanceOf<Availability>(result);
            Assert.AreEqual(1, result.applicantId);
            result = client.getAvailability(99999);
            Assert.IsInstanceOf<Availability>(result);
            Assert.AreEqual(0, result.applicantId);
        }

        [Test()]
        public void getEducationTest()
        {
            var result = client.getEducation(1);
            Assert.IsNotNull(result);
            result = client.getEducation(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getReferencesTest()
        {
            var result = client.getReferences(1);
            Assert.IsNotNull(result);
            result = client.getReferences(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getJobHistoriesTest()
        {
            var result = client.getReferences(1);
            Assert.IsNotNull(result);
            result = client.getReferences(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getESignatureTest()
        {
            var result = client.getESignature(1);
            Assert.IsNotNull(result);
            result = client.getESignature(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getApplicationTest()
        {
            var result = client.getApplication(1);
            Assert.IsNotNull(result);
            result = client.getApplication(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getApplicationsWithStoreIDTest()
        {
            var result = client.getApplicationsWithStoreID(1);
            Assert.IsNotNull(result);
            result = client.getApplicationsWithStoreID(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getApplicationsWithJobOpeningIDTest()
        {
            var result = client.getApplicationsWithJobOpeningID(1);
            Assert.IsNotNull(result);
            result = client.getApplicationsWithJobOpeningID(999999);
            Assert.IsNotNull(result);
        }

        [Test()]
        public void getApplicationsWithNameTest()
        {
            var result = client.getApplicationsWithName("","");
            Assert.IsNotNull(result);
            result = client.getApplicationsWithName(null, null);
            Assert.IsNotNull(result);
            result = client.getApplicationsWithName("chris", "apple");
            Assert.IsNotNull(result);
        }

        [Test()]
        public void updatePersonalInfoTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updateJobIdsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updateAvailabilityTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updateEducationsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updateJobHistoryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updateReferencesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updateElectronicSigTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void updatePasswordTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void deleteJobIdsTest()
        {
            Assert.Fail();
        }
    }
}
