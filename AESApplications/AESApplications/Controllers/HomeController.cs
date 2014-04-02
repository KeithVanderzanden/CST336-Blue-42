using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AESApplications.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            resetSession();
            return View();
        }

        [HttpPost]
        public ActionResult Welcome(String storeId)
        {
            this.Session["StoreId"] = storeId;
            return View();
        }

        public ActionResult Welcome()
        {
            resetSession();
            return View();
        }

        private void resetSession()
        {
            Session.Add("Status", "NoUser");  //LoggedIn or NoUser
            Session.Add("ApplicantId", -1);  //Any int > 0 is valid applicantId
            Session.Add("JobHistory", "Unfinished"); //Done or Unfinished
            Session.Add("LocalJobs", "Unfinished"); //Done or Unfinished
            Session.Add("Education", "Unfinished"); //Done or Unfinished
            Session.Add("Reference", "Unfinished"); //Done or Unfinished
            Session.Add("PersonalInfo", "Unfinished"); //Done or Unfinished
            Session.Add("Availability", "Unfinished"); //Done or Unfinished
            Session.Add("ESignature", "Unfinished"); //Done or Unfinished
            Session.Add("LoginType", "New"); //New or Return
            Session.Add("DisableMenu", "No"); //Yes or No - disables menu bar if Yes.
        }
    }
}