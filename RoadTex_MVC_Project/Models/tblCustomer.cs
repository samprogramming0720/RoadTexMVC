//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoadTex_MVC_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomer
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string contact_name { get; set; }
        public bool customer_status { get; set; }
        public int sales_rep { get; set; }
        public int preparer { get; set; }
        public Nullable<int> customer_priority { get; set; }
        public Nullable<int> follow_up_type { get; set; }
        public Nullable<System.DateTime> follow_up_date { get; set; }
        public int state_id { get; set; }
    
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual tblState tblState { get; set; }
    }
}
