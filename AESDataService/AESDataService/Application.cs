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
    
    public partial class Application
    {
        public int applicantId { get; set; }
        public int availablePosId { get; set; }
        public int storeId { get; set; }
        public string status { get; set; }
        public bool locked { get; set; }
        public string notes { get; set; }
    
        public virtual AvailablePosition AvailablePosition { get; set; }
        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual Store Store { get; set; }
    }
}
