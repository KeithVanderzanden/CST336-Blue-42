using AESApplications.DataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AESApplications.Controllers
{
    public class ESignatureController : Controller
    {
        //
        // GET: /ESignature/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ApplicationFinished()
        {
            bool signatureStored = false;
            using (var client = new DataServiceClient())
            {
                client.Open();
                var sig = new ElectronicSig();
                sig.date = DateTime.Now;
                sig.applicantId = Convert.ToInt32(this.Session["applicantId"]);
                int[] _jobIds = (int[])this.Session["jobIds"];
                signatureStored = await client.updateElectronicSigAsync(sig, _jobIds);
                client.Close();
            }
            if (signatureStored)
            {
                this.Session["ESignature"] = "Done";
                return RedirectToAction("Index", "PhoneScreen");
            }
            else
            {
                //error in storing signature
            }
            return RedirectToAction("Index", "ESignature");
        }
	}
}