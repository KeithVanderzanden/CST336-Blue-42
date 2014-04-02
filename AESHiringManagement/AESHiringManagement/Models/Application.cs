using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AESHiringManagement.Models
{
    public class Application
    {
        public int nextApp { get; set; }
        public AESDataService.ApplicantApp application { get; set;}
        public AESDataService.Note notes { get; set; }
    }
}