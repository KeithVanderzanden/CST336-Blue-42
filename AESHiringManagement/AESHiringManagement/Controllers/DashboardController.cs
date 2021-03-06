﻿using AESHiringManagement.Models;
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

        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View(new AESHiringManagement.Models.Application());
        }

        [HttpPost]
        public async Task<ActionResult> LoadApplication(AESHiringManagement.Models.Application model)
        {
            int id = Dashboard.getId((Request["selectApp"]).ToString());
            using (var client = new AESDataService.DataServiceClient())
            {
                model.application = await client.getApplicationAsync(id);
                client.Close();
                Dashboard.checkoutApplication(id);
            }
            if (model.notes == null)
            {
                model.notes = new AESDataService.Note();
            }
            return View("Index", model);
        }

        [OutputCache(Duration = 0)]
        [HttpGet]
        public ActionResult RefreshPending()
        {
            Dashboard.updatePendingApplications("new");
            return PartialView("_Pending");
        }

	}
}