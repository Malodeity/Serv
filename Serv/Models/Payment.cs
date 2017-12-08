using System;
namespace Serv.Models
{
    public class Payment
    {
       
        public int PayId { get; set; }
        public int PayCustId { get; set; }
        public string PayCardNum { get; set; }
        public string PayExp { get; set; }
        public string PayCVV { get; set; }



        public Payment(int payid, int paycustid, string paycard, string payexp, string paycvv)
        {
            PayId = payid;
            PayCustId = paycustid;
            PayCardNum = paycard;
            PayExp = payexp;
            PayCVV = paycvv;
        }


        public Payment()
        {


        }

        public Payment(int paycustid, string paycard, string payexp, string paycvv)
        {
            PayCustId = paycustid;
            PayCardNum = paycard;
            PayExp = payexp;
            PayCVV = paycvv;
        }


    }
}
