using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.IO;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Serv.Models;
using System.Web.Http;

namespace Serv.Controllers
{
    public class ProductController : ApiController
    {

        DataAccess akss = new DataAccess();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetProduct")]
        public IEnumerable<Product> GetAllRestaurants()
        {
            return akss.GetProduct();
        }




    }
}
