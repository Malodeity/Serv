using System;
namespace UberEatsV1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderProdName { get; set; }
        public string OrderQuantity { get; set; }
        public string OrderAddress { get; set; }
        public string OrderTotalAmnt { get; set; }


        public Order(int orderid, string orderprodname, string orderquanity, string orderaddress, string ordertotal)
        {
            OrderId = orderid;
            OrderProdName = orderprodname;
            OrderQuantity = orderquanity;
            OrderAddress = orderaddress;
            OrderTotalAmnt = ordertotal;

        }

        public Order()
        {

        }


        public Order(string orderprodname, string orderquanity, string orderaddress, string ordertotal)
        {

            OrderProdName = orderprodname;
            OrderQuantity = orderquanity;
            OrderAddress = orderaddress;
            OrderTotalAmnt = ordertotal;

        }

        public Order(string orderprodname, string orderquanity)
        {

            OrderProdName = orderprodname;
            OrderQuantity = orderquanity;

        }
    }
}
