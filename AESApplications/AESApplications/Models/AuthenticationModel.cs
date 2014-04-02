using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AESApplications.Models
{
    public class AuthenticationModel
    {
        [Required]
        [Display(Name = "User name: (SSN#)")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Invalid SSN. Ex: 123456789")]
        public string ssn { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MaxLength(50)]
        public string password { get; set; }
    }
}