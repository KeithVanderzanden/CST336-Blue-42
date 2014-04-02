using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AESApplications.Models
{
    public class PersonalInfoModel
    {
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string email { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string name_first { get; set; }

        [MaxLength(1)]
        [Display(Name = "Middle Initial")]
        public string name_middle { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string name_last { get; set; }
        
        [MaxLength(50)]
        [Display(Name = "Alternate Name (Optional)")]
        public string name_alt { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^\d{1,6}( *\s[0-9a-zA-Z.]{2,30})*", ErrorMessage = "Address must begin with a number followed by street")]
        [Display(Name = "Street Address")]
        public string street { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression(@"^[A-Za-z][A-Za-z]$", ErrorMessage = "State abreviation must be two letters.")]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}", ErrorMessage = "Invalid zip code. Ex: 45334")]
        [Display(Name = "Zipcode")]
        [DataType(DataType.PostalCode)]
        public string zip { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^(\d{1})?-?(\d{3})?-?\d{3}-?\d{4}$", ErrorMessage = "Invalid phone number Ex: 123-456-7890")]
        [DataType(DataType.PhoneNumber)]
        public string phone_num { get; set; }

        [Required]
        [Display(Name = "SSN#")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Invalid SSN. Ex: 123456789")]
        public string ssn { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MaxLength(50)]
        public string password { get; set; }

        [Required]
        [Display(Name = "Re-enter password")]
        [Compare("password")]
        [MaxLength(50)]
        public string passVerification { get; set; }
    }
}