using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Newtonsoft.Json;

namespace UberEatsV1.Models
{
    public class DataAccess
    {
        public static async Task<dynamic> getCustomerData(string query)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(query);

            dynamic cust = null;

            if (response != null)

            {
                string json = response.Content.ReadAsStringAsync().Result;
                cust = JsonConvert.DeserializeObject(json);


            }
            return cust;
        }


        HttpClient client = new HttpClient();

        public Customer GetCust(string email, string password)
        {
            string url = "http://10.0.2.2:8080/api/CustomersLogin?CustEmail=" + email + "&CustPassword=" + password + "";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Customer data = new Customer();
            data = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);

            return data;
        }


        public static async Task<dynamic> getCust(string query)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(query);

            dynamic cust = null;

            if (response != null)

            {
                string json = response.Content.ReadAsStringAsync().Result;
                cust = JsonConvert.DeserializeObject(json);
            }
            return cust;
        }



        public static async Task<dynamic> GetCustomer(string quer)
        {
            HttpClient client = new HttpClient();
            Customer customer = new Customer();

            var data = JsonConvert.SerializeObject(customer);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(quer, content);

            dynamic cust = null;

            if (response != null)
            {

                string json = response.Content.ReadAsStringAsync().Result;
                cust = JsonConvert.DeserializeObject(json);
                //ISharedPreferencesEditor editor = pref.Edit();
                //editor.PutString("Customers", data);
                //editor.Commit();
            }
            return cust;
        }
    }
}
