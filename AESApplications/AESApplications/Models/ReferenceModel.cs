using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AESApplications.Models
{
    public class ReferenceModel
    {
        public int applicantId { get; set; }
        public int referenceId { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Company")]
        public string company { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
    }
}