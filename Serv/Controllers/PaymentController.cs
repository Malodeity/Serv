using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Serv.Models;

namespace Serv.Controllers
{
    public class PaymentController : ApiController
    {
        DataAccess akss = new DataAccess();
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Payment")]
        public string PostPayment(Payment pay)
        {

            return akss.AddPayment(pay);
        }
    }
}
