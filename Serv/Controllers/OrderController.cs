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
    public class OrderController : ApiController
    {

        DataAccess akss = new DataAccess();
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddOrder")]
        public string PostOrder(Order odr)
        {
            if (odr != null)
            {
                return akss.AddOrder(odr);
            }
            return "Unable to add";
         
            
           
        }


    }
}
