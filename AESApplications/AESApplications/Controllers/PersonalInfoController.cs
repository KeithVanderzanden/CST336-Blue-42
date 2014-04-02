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
    public class PersonalInfoController : Controller
    {
        //
        // GET: /PersonalInfo/
        public async Task<ActionResult> Index()
        {
            var model = new PersonalInfoModel();
            if (((string)Session["Status"]).CompareTo("LoggedIn") == 0 && Convert.ToInt32(Session["ApplicantID"]) > 0)
            {
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var personalInfo = await client.getPersonalInfoAsync(Convert.ToInt32(Session["ApplicantId"]));
                    model.city = personalInfo.city;
                    model.name_alt = personalInfo.alias;
                    model.name_first = personalInfo.firstName;
                    model.name_last = personalInfo.lastName;
                    model.name_middle = personalInfo.middleName;
                    model.email = personalInfo.email;
                    model.phone_num = personalInfo.Phone;
                    model.ssn = personalInfo.socialNum;
                    model.state = personalInfo.state;
                    model.street = personalInfo.street;
                    model.zip = personalInfo.zip;
                    client.Close();
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(PersonalInfoModel model)
        {
            int tempId = -1;
            var auth = new ApplicantAuth();
            var personalInfo = new PersonalInfo();
            
            if (ModelState.IsValid)
            {
                personalInfo.firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.name_first.ToLower());
                personalInfo.middleName = model.name_middle == null ? model.name_middle : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.name_middle);
                personalInfo.lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.name_last.ToLower());
                personalInfo.alias = model.name_alt == null? model.name_alt : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.name_alt.ToLower());
                personalInfo.street = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.street.ToLower());
                personalInfo.city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.city.ToLower());
                personalInfo.state = model.state.ToUpper();
                personalInfo.email = model.email;
                personalInfo.zip = model.zip;
                personalInfo.Phone = model.phone_num;
                personalInfo.socialNum = model.ssn;
                personalInfo.applicantId = Convert.ToInt32(Session["ApplicantId"]);
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    tempId = await client.updatePersonalInfoAsync(personalInfo);
                    if (tempId > 0)
                    {
                        if (((string)Session["Status"]).CompareTo("LoggedIn") != 0)
                        {
                            if (!await client.updateJobIdsAsync(tempId, (int[])this.Session["jobIds"]))
                            {
                                //error storing jobIds (when logged in this is stored after questionnair)    
                            }
                            else
                            {
                                this.Session["LocalJobs"] = "Done";
                            }
                        }
                        this.Session["ApplicantId"] = tempId;
                        this.Session["PersonalInfo"] = "Done";
                        auth.applicantId = tempId;
                        auth.password = model.password;
                        if (!await client.updatePasswordAsync(auth))
                        {
                            //error saving password 
                        }
                        Session["Status"] = "LoggedIn";
                        return RedirectToAction("Index", "Availability");
                    }
                    else
                    {
                        //error occured, update failed
                    }
                    client.Close();
                }
            }
            return View(model);
        }
    }
}