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
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            var model = new AuthenticationModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(AuthenticationModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new DataServiceClient())
                {
                    int authenticated = await client.authenticateUserAsync(model.ssn, model.password);
                    if (authenticated > 0)
                    {
                        Session["ApplicantId"] = authenticated;
                        Session["Status"] = "LoggedIn";
                        Session["PersonalInfo"] = "Done";
                        return RedirectToAction("Index", "PersonalInfo");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "User name or password does not match.");
                    }
                }
            }
            return View(model);
        }
    }
}