using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AESApplications.Models
{
    public class AvailabilityModel
    {
        public int applicantId { get; set; }

        [Display(Name = "Salary expected")]
        public string salaryExpected { get; set; }

        [Display(Name = "Can you work full time?")]
        public string fullTimeYN { get; set; }

        [Display(Name = "Can you work days?")]
        public string daysYN { get; set; }

        [Display(Name = "Can you work evenings?")]
        public string eveningsYN { get; set; }

        [Display(Name = "Can you work weekends?")]
        public string weekendsYN { get; set; }

        public int mondayFrom { get; set; }
        
        public int mondayTo { get; set; }

        public int tuesdayFrom { get; set; }

        public int tuesdayTo { get; set; }

        public int wednesdayFrom { get; set; }

        public int wednesdayTo { get; set; }

        public int thursdayFrom { get; set; }

        public int thursdayTo { get; set; }

        public int fridayFrom { get; set; }

        public int fridayTo { get; set; }

        public int saturdayFrom { get; set; }

        public int saturdayTo { get; set; }

        public int sundayFrom { get; set; }

        public int sundayTo { get; set; }
    }

}