using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using UberEatsV1.Models;
namespace UberEatsV1.Models
{
    public class CoreUb
    {
       
        //update Customers
        public static async Task<Customer> Update(Customer custs)
        {
            string url = @"http://10.0.2.2:8080/api/UpdateCust?CustId=" + custs.CustId + ";";

            dynamic results = await DataAccess.GetCustomer(url).ConfigureAwait(false);

            if (results["Customers"] != null)
            {
               
                Customer cust = new Customer();
                cust.CustId = (Int32)results["CustId"];
                cust.CustName = (string)results["CustName"];
                cust.CustSurname = (string)results["CustSurname"];
                cust.CustEmail = (string)results["CustEmail"];
                cust.CustCell = (string)results["CustCell"];
                cust.CustPassword = (string)results["CustPassword"];
                return cust;
            }
            return null;
        }



        ////Login
        //public static async Task<Customer> GetCustomerss(string email, string password)
        //{
        //    string url = @"http://10.0.2.2:8080/api/CustomersLogin?CustEmail=" + email + "&CustPassword=" + password;

        //    dynamic results = await DataAccess.getCustomerData(url).ConfigureAwait(false);

        //    if (results["Customers"] != null)
        //    {
        //        Customer cust = new Customer();
        //        cust.CustId = (Int32)results["CustId"];
        //        cust.CustName = (string)results["CustName"];
        //        cust.CustSurname = (string)results["CustSurname"];
        //        cust.CustEmail = (string)results["CustEmail"];
        //        cust.CustCell = (string)results["CustCell"];
        //        cust.CustPassword = (string)results["CustPassword"];
        //        return cust;
        //    }
        //    return null;
        //}


        public CoreUb()
        {
        }



    }
}
