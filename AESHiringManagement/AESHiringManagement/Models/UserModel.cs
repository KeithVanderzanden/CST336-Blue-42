using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AESHiringManagement.Models
{
    public class UserModel
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string userName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}