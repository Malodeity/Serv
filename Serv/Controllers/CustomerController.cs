using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Serv.Models;

namespace Serv.Controllers
{
    public class CustomerController : ApiController
    {

        DataAccess akss = new DataAccess();



        //RETRIEVE ALL THE CUSTOMERS <--DATABASE
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
        return akss.GetAllCust();
        }



        //REGISTER A CUSTOMER

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Register")]
        public string PostCust(Customer cust)
        {
            if (cust != null)
            {
                return akss.AddCustomer(cust);
            }
            return "Unable to add";
        }

        //CUSTOMER LOGGING IN

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CustomersLogin")]
        public Customer GetCust(string email, string password)
        {
            return akss.CustomerLogin(email, password);
        }

        //UPDATE CUSTOMER

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/UpdateCust")]
        public Customer UpdateCust(Customer cust, int id)
        {
            return akss.CustomerUpdate(cust, id);
        }


    }
}
