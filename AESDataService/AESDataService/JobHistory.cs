//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AESDataService
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobHistory
    {
        public int applicantId { get; set; }
        public int jobHistoryId { get; set; }
        public string employer { get; set; }
        public Nullable<System.DateTime> fromDate { get; set; }
        public Nullable<System.DateTime> toDate { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string stateAbrev { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string supervisor { get; set; }
        public string position { get; set; }
        public string startSalary { get; set; }
        public string endSalary { get; set; }
        public string reasonLeave { get; set; }
        public string duties { get; set; }
    
        public virtual PersonalInfo PersonalInfo { get; set; }
    }
}
