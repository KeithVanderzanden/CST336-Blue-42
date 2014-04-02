using AESApplications.Models;
using AESApplications.DataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace AESApplications.Controllers
{
    public class JobHistoryController : Controller
    {
        //
        // GET: /JobHistory/
        public async Task<ActionResult> Index()
        {
            var model = new List<JobHistoryModel>();
            JobHistoryModel modelElement;
            for (int i = 0; i < 3; i++)
            {
                modelElement = new JobHistoryModel();
                model.Add(modelElement);
            }
            if (((string)Session["Status"]).CompareTo("LoggedIn") == 0 && Convert.ToInt32(Session["ApplicantId"]) > 0)
            {
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var allHistories = await client.getJobHistoriesAsync(Convert.ToInt32(Session["ApplicantId"]));
                    if (allHistories.Length < 3)
                    {
                        foreach (var element in model)
                        {
                            element.applicantId = Convert.ToInt32(Session["ApplicantId"]);
                        }
                    }
                    else
                    {
                        model.Clear();
                        foreach (var history in allHistories)
                        {
                            modelElement = new JobHistoryModel();
                            modelElement.city = history.city == null ? history.city : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(history.city.ToLower());
                            modelElement.employer = history.employer == null ? history.employer : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(history.employer.ToLower());
                            modelElement.from_month = history.fromDate == null ? "" : history.fromDate.Value.Month.ToString();
                            modelElement.from_year = history.fromDate == null ? "" : history.fromDate.Value.Year.ToString();
                            modelElement.phone = history.phone;
                            modelElement.position = history.position == null ? history.position : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(history.position.ToLower());
                            modelElement.reason_for_leaving = history.reasonLeave;
                            modelElement.responsibilities = history.duties;
                            modelElement.salary_end = history.endSalary;
                            modelElement.salary_start = history.startSalary;
                            modelElement.state = history.stateAbrev == null ? null : history.stateAbrev.ToUpper();
                            modelElement.street = history.street == null ? history.street : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(history.street.ToLower());
                            modelElement.supervisor = history.supervisor == null ? history.supervisor : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(history.supervisor.ToLower());
                            modelElement.to_month = history.toDate == null ? "" : history.toDate.Value.Month.ToString();
                            modelElement.to_year = history.toDate == null ? "" : history.toDate.Value.Year.ToString();
                            modelElement.zip = history.zip;
                            modelElement.applicantId = history.applicantId;
                            modelElement.historyId = history.jobHistoryId;
                            model.Add(modelElement);
                        }
                    }
                    client.Close();
                }
            }
            else
            {
                //error not logged in or applicantId not set correctly
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(List<JobHistoryModel> model)
        {
            if (ModelState.IsValid)
            {
                bool historyStored = false;
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var history = new List<JobHistory>();
                    foreach (var jobHistory in model)
                    {
                        int tempYear;
                        int tempMonth;
                        var jobHist = new JobHistory();
                        jobHist.street = jobHistory.street;
                        jobHist.city = jobHistory.city;
                        jobHist.stateAbrev = jobHistory.state;
                        jobHist.zip = jobHistory.zip;
                        jobHist.applicantId = Convert.ToInt32(this.Session["ApplicantId"]);
                        jobHist.duties = jobHistory.responsibilities;
                        jobHist.employer = jobHistory.employer;
                        jobHist.endSalary = jobHistory.salary_end;
                        jobHist.startSalary = jobHistory.salary_start;
                        if (jobHistory.from_year != null && jobHistory.from_month != null)
                        {
                            tempYear = Convert.ToInt32(jobHistory.from_year);  
                            tempMonth = Convert.ToInt32(jobHistory.from_month); 
                            jobHist.fromDate = new DateTime(tempYear, tempMonth, 1);
                            tempYear = Convert.ToInt32(jobHistory.to_year);  
                            tempMonth = Convert.ToInt32(jobHistory.to_month); 
                            jobHist.toDate = new DateTime(tempYear, tempMonth, 1);
                        }
                        else
                        {
                            jobHist.toDate = null;
                            jobHist.fromDate = null;
                        }
                        jobHist.phone = jobHistory.phone;
                        jobHist.position = jobHistory.position;
                        jobHist.reasonLeave = jobHistory.reason_for_leaving;
                        jobHist.supervisor = jobHistory.supervisor;
                        jobHist.applicantId = jobHistory.applicantId;
                        jobHist.jobHistoryId = jobHistory.historyId;
                        history.Add(jobHist);
                    }
                    historyStored = await client.updateJobHistoryAsync(history.ToArray());
                    client.Close();
                }
                if (historyStored/**true**/)
                {
                    this.Session["JobHistory"] = "Done";
                    return RedirectToAction("Index", "Education");
                }
                else
                {
                    //error in storing history... add error view or determine action??
                }
            }
            return View(model);
        }
	}
}