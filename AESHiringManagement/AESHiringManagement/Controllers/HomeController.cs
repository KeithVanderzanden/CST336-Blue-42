using AESHiringManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AESHiringManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dashboard = Dashboard.Instance;
            Session["Status"] = "NoUser";
            return View("Login",new UserModel());
        }

        public ActionResult Authenticate(UserModel model)
        {
            using (var client = new AESDataService.DataServiceClient())
            {
                //make some call to get permission level
                //method doesnt exist yet....
                Session["Permission"] =  client.authenticateManager(model.userName, model.password);
            }
            if (((string)Session["Permission"]).CompareTo("None") == 0)
            {
                ModelState.Clear();
                ModelState.AddModelError("userName", "User name or password is incorrect.");
                return View("Login");
            }
            else
            {
                Session["Status"] = "LoggedIn";
            }
            return RedirectToAction("Index","Dashboard");
        }
    }
}