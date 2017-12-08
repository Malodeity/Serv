using System;
namespace Serv.Models
{
    public class Product
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public decimal ProdPrice { get; set; }
        public byte[] ProdImage { get; set; }


        public Product(int prodid, string prodname, string proddesc, decimal prodprice, byte[] prodimage)
        {
            ProdId = prodid;
            ProdName = prodname;
            ProdDesc = proddesc;
            ProdPrice = prodprice;
            ProdImage = prodimage;
        }


        public Product()
        {
            
        }


        public Product(string prodname, string proddesc, decimal prodprice, byte[] prodimage)
        {
            ProdName = prodname;
            ProdDesc = proddesc;
            ProdPrice = prodprice;
            ProdImage = prodimage;
        }



    }
}
