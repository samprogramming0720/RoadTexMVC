using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;

namespace RoadTex_MVC_Project.Controllers
{
    public class CustomerDashBoardController : Controller
    {
        RoadTex_MVC_Model_Local DB = new RoadTex_MVC_Model_Local();
        
        // GET: CustomerDashBoard
        public ActionResult Index(ShowCustomers cs, string search)
        {
                  List<ShowCustomers> CustList = new List<ShowCustomers>();
                if (search == null)
                {
                  
                    var customers = DB.sp_GetCustomers();
                    foreach (var cust in customers)
                    {
                        cs = new ShowCustomers();
                        cs.Account = cust.customer_name;
                        cs.Contact = cust.contact_name;
                        cs.SalesRep = cust.sales_rep;
                        cs.Preparer = cust.preparer;
                        if (cust.follow_up_type == 1)
                        {
                            cs.FollowUp = "Phone Call";
                        }
                        else if (cust.follow_up_type == 2)
                        {
                            cs.FollowUp = "Email";

                        }
                        else
                        {
                            cs.FollowUp = "Mail";
                        }

                        cs.FollowUpDate = cust.followUp_date;
                        cs.Priority = (int)cust.customer_priority;

                        if (cust.customer_status)
                        {
                            cs.Status = "Active";
                        }
                        else
                        {
                            cs.Status = "Inactive";
                        }

                        CustList.Add(cs);

                    }
                    
                    cs = new ShowCustomers();
                    cs.CustomerList = CustList;



                    return View(cs);
                }
                else
                {
                    var searchResult = DB.sp_Search(search);
                    foreach (var cust in searchResult)
                    {
                        cs = new ShowCustomers();
                        cs.Account = cust.customer_name;
                        cs.Contact = cust.contact_name;
                        cs.SalesRep = cust.sales_rep;
                        cs.Preparer = cust.preparer;
                        if (cust.follow_up_type == 1)
                        {
                            cs.FollowUp = "Phone Call";
                        }
                        else if (cust.follow_up_type == 2)
                        {
                            cs.FollowUp = "Email";

                        }
                        else
                        {
                            cs.FollowUp = "Mail";
                        }

                        cs.FollowUpDate = cust.followUp_date;
                        cs.Priority = (int)cust.customer_priority;

                        if (cust.customer_status)
                        {
                            cs.Status = "Active";
                        }
                        else
                        {
                            cs.Status = "Inactive";
                        }

                        CustList.Add(cs);

                    }
                    // ViewBag.data = CustomerList;
                    cs = new ShowCustomers();
                    cs.CustomerList = CustList;
                    return View(cs);
                }

          
        }

        //Get: form to add customer
        public ActionResult AddCustomer()
        {
           

            return View();
        }


        [HttpPost]
        public ActionResult AddCustomer(tblCustomer c)
        {

            string cName = c.customer_name;
            string conName = c.contact_name;
            int sRep = c.sales_rep;
            int p = c.preparer;
            int ft = (int)c.follow_up_type;
            DateTime fdate = (DateTime)c.follow_up_date;
            int sId = c.state_id;
            bool cStatus = c.customer_status;
            int cp = (int)c.customer_priority;


            //Entering the data into database using store procedure.
            int result = DB.sp_cm_create_customers(cName, conName, cStatus, sRep, p, cp, ft, fdate, sId);

            if(result != 0)
            {
                ViewBag.message = "Customer Added Successfully.";
            }
            else
            {
                ViewBag.message = "Failed to Add Customer !";
            }


             
            return RedirectToAction("AddCustomer");
        }

      
    }
}