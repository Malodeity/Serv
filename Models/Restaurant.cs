using System;
namespace UberEatsV1.Models
{
    public class Restaurant
    {
        public int ResId { get; set; }
        public string ResName { get; set; }
        public string ResAddress { get; set; }
        public string ResCity { get; set; }
        public byte[] ResImage { get; set; }



        public Restaurant(int resid, string resname, string resaddress, string rescity, byte[] resimage)
        {
            ResId = resid;
            ResName = resname;
            ResAddress = resaddress;
            ResCity = rescity;
            ResImage = resimage;

        }

        public Restaurant()
        {

        }


        public Restaurant(string resname, string resaddress, string rescity, byte[] resimage)
        {
            ResName = resname;
            ResAddress = resaddress;
            ResCity = rescity;
            ResImage = resimage;

        }
    }
}