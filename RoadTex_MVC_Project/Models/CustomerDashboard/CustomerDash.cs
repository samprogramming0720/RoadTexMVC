using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadTex_MVC_Project.Models
{
    public class CustomerDash
    {
        public string custName;
        public string contactName;
        public List<string> d;
    }

    public class AddCustomerModel
    {
        public int customer_id { get; set; }
        [Required(ErrorMessage = "Account Name Required")]
        [Display(Name = "Account Name")]
        public string customer_name { get; set; }
        [Required(ErrorMessage = "Contact Name Required")]
        [Display(Name = "Contact Name")]
        public string contact_name { get; set; }
        [Required]
        [Display(Name = "Status")]
        public bool customer_status { get; set; }
        [Required(ErrorMessage = "Enter User Id of Sales Rep")]
        [Display(Name = "Sales Rep")]
        public int sales_rep { get; set; }
        [Required(ErrorMessage = "Enter User Id of Preparer")]
        [Display(Name = "Preparer")]
        public int preparer { get; set; }
        [Display(Name = "Priority")]
        public Nullable<int> customer_priority { get; set; }
        [Display(Name = "Follow Up Type")]
        public Nullable<int> follow_up_type { get; set; }
        [Display(Name = "Follow Up Date")]
        public Nullable<System.DateTime> follow_up_date { get; set; }
        [Display(Name = "State Id")]
        public int state_id { get; set; }


    }

    public class ShowCustomers
    {
        public string Search { get; set; }
        public List<ShowCustomers> CustomerList { get; set; }
        public string Account { get; set; }
        public string Contact { get; set; }
        public string SalesRep { get; set; }
        public string Preparer { get; set; }
        public string Status { get; set; }
        public string FollowUp { get; set; }
        public string FollowUpDate { get; set; }
        public int Priority { get; set; }

    }
}