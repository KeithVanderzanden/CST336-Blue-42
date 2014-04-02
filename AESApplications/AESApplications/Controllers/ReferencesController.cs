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
    public class ReferencesController : Controller
    {
        //
        // GET: /References/
        public async Task<ActionResult> Index()
        {
            var model = new List<ReferenceModel>();
            ReferenceModel modelElement;
            for (int i = 0; i < 3; i++)
            {
                modelElement = new ReferenceModel();
                model.Add(modelElement);
            }
            if (((string)Session["Status"]).CompareTo("LoggedIn") == 0 && Convert.ToInt32(Session["ApplicantID"]) > 0)
            {
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    //the problem here is what if the reference never got saved in the first place?
                    var references = await client.getReferencesAsync(Convert.ToInt32(Session["ApplicantId"]));
                    if (references.Length < 3)
                    {
                        foreach (var element in model)
                        {
                            element.applicantId = Convert.ToInt32(Session["ApplicantId"]);
                        }
                    }
                    else
                    {
                        model.Clear();
                        foreach (var reference in references)
                        {
                            modelElement = new ReferenceModel();
                            modelElement.company = reference.company == null ? reference.company : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reference.company.ToLower());
                            modelElement.name = reference.name == null ? reference.name : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reference.name.ToLower());
                            modelElement.phone = reference.phone;
                            modelElement.title = reference.title == null ? reference.title : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reference.title.ToLower());
                            modelElement.applicantId = reference.applicantId;
                            modelElement.referenceId = reference.referenceId;
                            model.Add(modelElement);
                        }
                    }
                    client.Close();
                }
            }
            else 
            {
                //error not logged in or login failed to load ApplicantId 
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(List<ReferenceModel> model)
        {
            if (ModelState.IsValid)
            {
                bool referencesStored = false;
                using (var client = new DataServiceClient())
                {
                    client.Open();
                    var references = new List<Reference>();
                    foreach (var refer in model)
                    {
                        var temp = new Reference();
                        temp.applicantId = Convert.ToInt32(this.Session["ApplicantId"]);
                        temp.company = refer.company;
                        temp.name = refer.name;
                        temp.phone = refer.phone;
                        temp.title = refer.title;
                        temp.applicantId = refer.applicantId;
                        temp.referenceId = refer.referenceId;
                        references.Add(temp);
                    }
                    referencesStored = await client.updateReferencesAsync(references.ToArray());
                    client.Close();
                }
                if (referencesStored) 
                {
                    this.Session["Reference"] = "Done";
                    return RedirectToAction("Index", "ESignature");
                }
                else
                {
                    //error has occured storing references.
                }
            }
            return View(model);
        }
	}
}