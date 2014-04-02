using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AESApplications.Models
{
    public class EducationModel
    {
        public int applicantId { get; set; }
        public int educationId { get; set; }

        [Display(Name = "School Name")]
        [StringLength(50)]
        public string name { get; set; }

        [RegularExpression(@"\d{1,6}( *\s[a-zA-Z.]{2,30})*", ErrorMessage = "Address must begin with a number followed by street")]
        [Display(Name = "Street Address")]
        public string street { get; set; }

        [StringLength(50)]
        [Display(Name = "City")]
        public string city { get; set; }

        [StringLength(2)]
        [Display(Name = "State")]
        public string state { get; set; }
        
        [RegularExpression(@"\d{5}", ErrorMessage = "Invalid zip code. Ex: 45334")]
        [Display(Name = "Zipcode")]
        [DataType(DataType.PostalCode)]
        public string zip { get; set; }
        
        [Display(Name = "Year started")]
        [StringLength(4)]
        [RegularExpression(@"\d{4}", ErrorMessage = "Invalid year. Ex: 1992")]
        public string yearFrom { get; set; }

        [Display(Name = "Last Year Attended")]
        [StringLength(4)]
        [RegularExpression(@"\d{4}", ErrorMessage = "Invalid year. Ex: 1992")]
        public string yearTo { get; set; }
        
        [Display(Name = "Graduated(y/n)")]
        [StringLength(1)]
        [RegularExpression(@"[yYnN]", ErrorMessage = "Must be y or n")]
        public string graduated { get; set; }
        
        [Display(Name = "Degree/Major")]
        public string degree { get; set; }
    }
}