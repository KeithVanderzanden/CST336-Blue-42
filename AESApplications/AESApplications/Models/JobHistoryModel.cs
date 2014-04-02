using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AESApplications.Models
{
    public class JobHistoryModel
    {
        public int applicantId { get; set; }
        public int historyId { get; set; }

        [Display(Name = "Employer")]
        [MaxLength(50)]
        public string employer { get; set; }

        [MaxLength(50)]
        [Display(Name = "Street address")]
        public string street { get; set; }

        [Display(Name = "City")]
        [MaxLength(50)]
        public string city { get; set; }

        [Display(Name = "State")]
        [RegularExpression(@"^[A-Za-z][A-Za-z]$", ErrorMessage = "State abreviation must be two letters.")]
        [StringLength(2)]
        public string state { get; set; }

        [StringLength(5)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid zip code. Ex: 45334")]
        [Display(Name = "Zip code")]
        public string zip { get; set; }

        [Display(Name = "Position")]
        [MaxLength(50)]
        public string position { get; set; }

        [Display(Name = "From MM/YYYY")]
        [StringLength(2)]
        [RegularExpression(@"^(0?[1-9]|1[012])$", ErrorMessage = "Invalid month, must be 1-12")]
        public string from_month { get; set; }

        [StringLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid year")]
        public string from_year { get; set; }

        [Display(Name = "To MM/YYYY")]
        [StringLength(2)]
        [RegularExpression(@"^(0?[1-9]|1[012])$", ErrorMessage = "Invalid month, must be 1-12")]
        public string to_month { get; set; }

        [StringLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid year")]
        public string to_year { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(16)]
        [RegularExpression(@"^(\d{1})?-?(\d{3})?-?\d{3}-?\d{4}$", ErrorMessage = "Invalid phone number Ex: 123-456-7890")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [Display(Name = "Supervisor")]
        [MaxLength(50)]
        public string supervisor { get; set; }

        [Display(Name = "Starting salary")]
        [MaxLength(50)]
        public string salary_start { get; set; }

        [Display(Name = "Ending salary")]
        [MaxLength(50)]
        public string salary_end { get; set; }

        [Display(Name = "Reason for leaving")]
        [MaxLength(50)]
        public string reason_for_leaving { get; set; }

        [Display(Name = "Describe your responsibilities")]
        [MaxLength(2000)]
        public string responsibilities { get; set; }

    }
}