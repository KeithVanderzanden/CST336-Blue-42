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

        public async Task<ActionResult> Authenticate()
        {
            Session["Status"] = "LoggedIn";
            return RedirectToAction("Index","Dashboard");
        }
    }
}