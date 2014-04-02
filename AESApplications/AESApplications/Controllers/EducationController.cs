using AESApplications.Models;
using AESApplications.DataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace AESApplications.Controllers
{
    public class EducationController : Controller
    {
        //
        // GET: /Education/
        public async Task<ActionResult> Index()
        {
            var model = new List<EducationModel>();
            EducationModel modelElement;
            for (int i = 0; i < 3; i++)
            {
                modelElement = new EducationModel();
                model.Add(modelElement);
            }
            if (((string)Session["Status"]).CompareTo("LoggedIn") == 0 && Convert.ToInt32(Session["ApplicantID"]) > 0)
            {
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var educations = await client.getEducationAsync(Convert.ToInt32(Session["ApplicantId"]));
                    if (educations.Length < 3)
                    {
                        foreach (var element in model)
                        {
                            element.applicantId = Convert.ToInt32(Session["ApplicantID"]);
                        }
                    }
                    else
                    {
                        model.Clear();
                        foreach (var education in educations)
                        {
                            modelElement = new EducationModel();
                            modelElement.city = education.city == null ? education.city : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(education.city.ToLower());
                            modelElement.degree = education.degreeMajor == null ? education.degreeMajor : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(education.degreeMajor.ToLower());
                            modelElement.graduated = education.graduatedYN == null ? null : education.graduatedYN.ToLower();
                            modelElement.name = education.name == null ? education.name : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(education.name.ToLower());
                            modelElement.state = education.stateAbrev == null ? null : education.stateAbrev.ToUpper();
                            modelElement.street = education.street == null ? education.street : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(education.street.ToLower());
                            modelElement.yearFrom = education.yearFrom;
                            modelElement.yearTo = education.yearTo;
                            modelElement.zip = education.zip;
                            modelElement.applicantId = education.applicantId;
                            modelElement.educationId = education.educationId;
                            model.Add(modelElement);
                        }
                    }
                    client.Close();
                }
            }
            else
            {
                //error must login or applicantId not set properly on login
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(List<EducationModel> model)
        {
            if (ModelState.IsValid)
            {
                bool educationStored = false;
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var education = new List<Education>();
                    foreach (var ed in model)
                    {
                        var temp = new Education();
                        temp.applicantId = Convert.ToInt32(this.Session["ApplicantId"]);
                        temp.degreeMajor = ed.degree;
                        temp.graduatedYN = ed.graduated;
                        temp.street = ed.street;
                        temp.city = ed.city;
                        temp.stateAbrev = ed.state;
                        temp.zip = ed.zip;
                        temp.name = ed.name;
                        temp.yearFrom = ed.yearFrom;
                        temp.yearTo = ed.yearTo;
                        temp.applicantId = ed.applicantId;
                        temp.educationId = ed.educationId;
                        education.Add(temp);
                    }
                    educationStored = await client.updateEducationsAsync(education.ToArray());
                    client.Close();
                }
                if (educationStored)
                {
                    this.Session["Education"] = "Done";
                    return RedirectToAction("Index", "References");
                }
                else
                {
                    //error occured storing education 
                }
            }
            return View(model);
        }
	}
}