using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AESHiringManagement.Controllers
{
    public class JobOpeningsController : Controller
    {
        public ActionResult Open()
        {
            return View();
        }

        public ActionResult Close()
        {
            return View();
        }
	}
}