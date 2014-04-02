using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AESApplications.Models
{
    public class JobModel
    {
        public int jobId { get; set; }
        public int selected { get; set; }
        public string location { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string requirements { get; set; }
        public string education { get; set; }
        public string pay { get; set; }
        public string mondayFrom { get; set; }
        public string mondayTo { get; set; }
        public string tuesdayFrom { get; set; }
        public string tuesdayTo { get; set; }
        public string wednesdayFrom { get; set; }
        public string wednesdayTo { get; set; }
        public string thursdayFrom { get; set; }
        public string thursdayTo { get; set; }
        public string fridayFrom { get; set; }
        public string fridayTo { get; set; }
        public string saturdayFrom { get; set; }
        public string saturdayTo { get; set; }
        public string sundayFrom { get; set; }
        public string sundayTo { get; set; }
    }
}