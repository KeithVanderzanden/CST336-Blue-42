using AESHiringManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AESHiringManagement.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            HttpContext.Response.AddHeader("refresh", "10; url=" + Url.Action());
            return View(new AESHiringManagement.Models.Application());
        }

        [HttpPost]
        public async Task<ActionResult> LoadApplication(AESHiringManagement.Models.Application model)
        {
            int id = Dashboard.getId((Request["selectApp"]).ToString());
            Dashboard.setStatus(id, 1);
            using (var client = new AESDataService.DataServiceClient())
            {
                model.application = await client.getApplicationAsync(id);
                client.Close();
            }
            if (model.notes == null)
            {
                model.notes = new AESDataService.Note();
            }
            return View("Index", model);
        }

        public ActionResult WaitList()
        {
            return PartialView("WaitList");
        }
	}
}