using AESApplications.Models;
using AESApplications.DataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AESApplications.Controllers
{
    public class AvailabilityController : Controller
    {
        //
        // GET: /Availability/
        public async Task<ActionResult> Index()
        {
            var model = new AvailabilityModel();
            if (((string)Session["Status"]).CompareTo("LoggedIn") == 0 && Convert.ToInt32(Session["ApplicantID"]) > 0)
            {
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var availability = await client.getAvailabilityAsync(Convert.ToInt32(Session["ApplicantId"]));
                    if (availability.applicantId <= 0)
                    {
                        model.applicantId = Convert.ToInt32(Session["ApplicantID"]);
                    }
                    else
                    {
                        model.daysYN = availability.daysYN;
                        model.eveningsYN = availability.eveningsYN;
                        model.fridayFrom = availability.fridayFrom == null ? 0 : availability.fridayFrom.Value.Hours + 1;
                        model.fridayTo = availability.fridayTo == null ? 0 : availability.fridayTo.Value.Hours + 1;
                        model.fullTimeYN = availability.fullTimeYN;
                        model.mondayFrom = availability.mondayFrom == null ? 0 : availability.mondayFrom.Value.Hours + 1;
                        model.mondayTo = availability.mondayTo == null ? 0 : availability.mondayTo.Value.Hours + 1;
                        model.salaryExpected = availability.salaryExpected;
                        model.saturdayFrom = availability.saturdayFrom == null ? 0 : availability.saturdayFrom.Value.Hours + 1;
                        model.saturdayTo = availability.saturdayTo == null ? 0 : availability.saturdayTo.Value.Hours + 1;
                        model.sundayFrom = availability.sundayFrom == null ? 0 : availability.sundayFrom.Value.Hours + 1;
                        model.sundayTo = availability.sundayTo == null ? 0 : availability.sundayTo.Value.Hours + 1;
                        model.thursdayFrom = availability.thursdayFrom == null ? 0 : availability.thursdayFrom.Value.Hours + 1;
                        model.thursdayTo = availability.thursdayTo == null ? 0 : availability.thursdayTo.Value.Hours + 1;
                        model.tuesdayFrom = availability.tuesdayFrom == null ? 0 : availability.tuesdayFrom.Value.Hours + 1;
                        model.tuesdayTo = availability.tuesdayTo == null ? 0 : availability.tuesdayTo.Value.Hours + 1;
                        model.wednesdayFrom = availability.wednesdayFrom == null ? 0 : availability.wednesdayFrom.Value.Hours + 1;
                        model.wednesdayTo = availability.wednesdayTo == null ? 0 : availability.wednesdayTo.Value.Hours + 1;
                        model.weekendsYN = availability.weekendsYN;
                        model.applicantId = availability.applicantId;
                    }
                    client.Close();
                }
            }
            else
            {
                //error need to login or applicantId not properly set
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(AvailabilityModel model)
        {
            bool saveSuccessful = false;
            if (ModelState.IsValid)
            {
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var infoToStore = new Availability();
                    infoToStore.applicantId = Convert.ToInt32(this.Session["ApplicantId"]);
                    infoToStore.daysYN = model.daysYN;
                    infoToStore.eveningsYN = model.eveningsYN;
                    infoToStore.weekendsYN = model.weekendsYN;
                    infoToStore.fullTimeYN = model.fullTimeYN;
                    infoToStore.salaryExpected = model.salaryExpected;
                    infoToStore.mondayFrom = model.mondayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.mondayFrom - 1, 0, 0)) : null;
                    infoToStore.mondayTo = model.mondayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.mondayTo - 1, 0, 0)) : null;
                    infoToStore.tuesdayFrom = model.tuesdayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.tuesdayFrom - 1, 0, 0)) : null;
                    infoToStore.tuesdayTo = model.tuesdayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.tuesdayTo - 1, 0, 0)) : null;
                    infoToStore.wednesdayFrom = model.wednesdayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.wednesdayFrom - 1, 0, 0)) : null;
                    infoToStore.wednesdayTo = model.wednesdayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.wednesdayTo - 1, 0, 0)) : null;
                    infoToStore.thursdayFrom = model.thursdayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.thursdayFrom - 1, 0, 0)) : null;
                    infoToStore.thursdayTo = model.thursdayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.thursdayTo - 1, 0, 0)) : null;
                    infoToStore.fridayFrom = model.fridayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.fridayFrom - 1, 0, 0)) : null;
                    infoToStore.fridayTo = model.fridayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.fridayTo - 1, 0, 0)) : null;
                    infoToStore.saturdayFrom = model.saturdayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.saturdayFrom - 1, 0, 0)) : null;
                    infoToStore.saturdayTo = model.saturdayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.saturdayTo - 1, 0, 0)) : null;
                    infoToStore.sundayFrom = model.sundayFrom > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.sundayFrom - 1, 0, 0)) : null;
                    infoToStore.sundayTo = model.sundayTo > 0 ? new Nullable<TimeSpan>(new TimeSpan(model.sundayTo - 1, 0, 0)) : null;
                    infoToStore.applicantId = model.applicantId;
                    saveSuccessful = await client.updateAvailabilityAsync(infoToStore);
                    client.Close();
                }
                if (saveSuccessful)
                {
                    this.Session["Availability"] = "Done";
                    return RedirectToAction("Index", "JobHistory");
                }
                else
                {
                    //some error has occured saving --currently do nothing
                }
            }
            return View(model);
        }
    }
}