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

        [Display(Name = "Phone")]
        [MaxLength(16)]
        [RegularExpression(@"^(\d{1})?-?(\d{3})?-?\d{3}-?\d{4}$", ErrorMessage = "Invalid phone number Ex: 123-456-7890")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
        
        [Display(Name = "Company")]
        public string company { get; set; }
        
        [Display(Name = "Title")]
        public string title { get; set; }
    }
}