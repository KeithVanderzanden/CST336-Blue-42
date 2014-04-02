using AESApplications.DataServiceReference;
using AESApplications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AESApplications.Controllers
{
    public class LocalJobsController : Controller
    {
        //
        // GET: /LocalJobs/
        public async Task<ActionResult> Index()
        {
            var jobs = new List<JobModel>();
            using (var client = new DataServiceClient())
            {
                client.Open();
                var loadedJobs = await client.getJobsAsync(Convert.ToInt32(this.Session["storeId"]));
                var jobsSelected = new List<int>();
                if (((string)this.Session["Status"]).CompareTo("LoggedIn") == 0)
                {
                    jobsSelected.AddRange(await client.getJobsAppliedForAsync(Convert.ToInt32(this.Session["ApplicantId"])));
                }
                foreach (var job in loadedJobs)
                {
                    var tempJob = new JobModel();
                    tempJob.selected = jobsSelected.Contains(job.availablePositionId) ? 1 : 0;
                    tempJob.jobId = job.availablePositionId;
                    tempJob.description = job.description;
                    tempJob.requirements = job.requirements;
                    tempJob.education = job.education;
                    tempJob.title = job.title;
                    tempJob.pay = job.pay;
                    tempJob.location = job.location;
                    tempJob.mondayFrom = job.mondayFrom != null ? DateTime.Today.Add((TimeSpan)job.mondayFrom).ToString("hh:mm tt"): "-" ;
                    tempJob.mondayTo = job.mondayTo != null ? DateTime.Today.Add((TimeSpan)job.mondayTo).ToString("hh:mm tt") : "-";
                    tempJob.tuesdayFrom = job.tuesdayFrom != null ? DateTime.Today.Add((TimeSpan)job.tuesdayFrom).ToString("hh:mm tt") : "-";
                    tempJob.tuesdayTo = job.tuesdayTo != null ? DateTime.Today.Add((TimeSpan)job.tuesdayTo).ToString("hh:mm tt") : "-";
                    tempJob.wednesdayFrom = job.wednesdayFrom != null ? DateTime.Today.Add((TimeSpan)job.wednesdayFrom).ToString("hh:mm tt") : "-";
                    tempJob.wednesdayTo = job.wednesdayTo != null ? DateTime.Today.Add((TimeSpan)job.wednesdayTo).ToString("hh:mm tt") : "-";
                    tempJob.thursdayFrom = job.thursdayFrom != null ? DateTime.Today.Add((TimeSpan)job.thursdayFrom).ToString("hh:mm tt") : "-";
                    tempJob.thursdayTo = job.thursdayTo != null ? DateTime.Today.Add((TimeSpan)job.thursdayTo).ToString("hh:mm tt") : "-";
                    tempJob.fridayFrom = job.fridayFrom != null ? DateTime.Today.Add((TimeSpan)job.fridayFrom).ToString("hh:mm tt") : "-";
                    tempJob.fridayTo = job.fridayTo != null ? DateTime.Today.Add((TimeSpan)job.fridayTo).ToString("hh:mm tt") : "-";
                    tempJob.saturdayFrom = job.saturdayFrom != null ? DateTime.Today.Add((TimeSpan)job.saturdayFrom).ToString("hh:mm tt") : "-";
                    tempJob.saturdayTo = job.saturdayTo != null ? DateTime.Today.Add((TimeSpan)job.saturdayTo).ToString("hh:mm tt") : "-";
                    tempJob.sundayFrom = job.sundayFrom != null ? DateTime.Today.Add((TimeSpan)job.sundayFrom).ToString("hh:mm tt") : "-";
                    tempJob.sundayTo = job.sundayTo != null ? DateTime.Today.Add((TimeSpan)job.sundayTo).ToString("hh:mm tt") : "-";
                    jobs.Add(tempJob);
                }
                client.Close();
            }
          
            return View(jobs);
        }
    }

}