using System;
namespace Serv.Models
{
    public class Customer
    {


        public int CustId { get; set; }
        public string CustName { get; set; }
        public string CustSurname { get; set; }
        public string CustEmail { get; set; }
        public string CustCell { get; set; }
        public string CustPassword { get; set; }



        public Customer(int id, string custName, string custSurname, string custEmail, string custCell, string custPassword)
        {
            CustId = id;
            CustName = custName;
            CustSurname = custSurname;
            CustEmail = custEmail;
            CustCell = custCell;
            CustPassword = custPassword;

        }
        public Customer()
        {


        }

        public Customer(string custName, string custSurname, string custEmail, string custCell, string custPassword)
        {
            CustName = custName;
            CustSurname = custSurname;
            CustEmail = custEmail;
            CustCell = custCell;
            CustPassword = custPassword;

        }


        public Customer(string cutEmail, string custPassword)
        {
            CustEmail = cutEmail;
            CustPassword = custPassword;
        }
    }
}
