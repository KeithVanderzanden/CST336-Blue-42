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
            Assert.Fail();
        }

        [Test()]
        public void getJobsAppliedForTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getPersonalInfoTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getAvailabilityTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getEducationTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getReferencesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getJobHistoriesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getESignatureTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getApplicationTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getApplicationsWithStoreIDTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getApplicationsWithJobOpeningIDTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getApplicationsWithNameTest()
        {
            Assert.Fail();
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
