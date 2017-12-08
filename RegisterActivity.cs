
using System;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using UberEatsV1.Models;


namespace UberEatsV1
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        static string url = "http://10.0.2.2:8080/api/Register";


        EditText CustName, CustSurname, CustEmail, CustCell, CustPassword;

        HttpClient client;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);


            CustName = FindViewById<EditText>(Resource.Id.txtCustName);
            CustSurname = FindViewById<EditText>(Resource.Id.txtCustSurname);
            CustEmail = FindViewById<EditText>(Resource.Id.txtCustEmail);
            CustCell = FindViewById<EditText>(Resource.Id.txtCustCell);
            CustPassword = FindViewById<EditText>(Resource.Id.txtCustPassword);
            Button SignUp = FindViewById<Button>(Resource.Id.btnSignUp);



            SignUp.Click += SignUp_Click;


            TextView text = FindViewById<TextView>(Resource.Id.textView1);

            text.Click += delegate
            {
                Intent ti = new Intent(this, typeof(LoginActivity));
                StartActivity(ti);
            };
        }


        //REGISTER BUTTON WITH AN INTENTx 
        private async void SignUp_Click(object sender, EventArgs e)
        {

            try
            {
                client = new HttpClient();
                var myClient = new Customer
                {
                    CustName = CustName.Text,
                    CustSurname = CustSurname.Text,
                    CustEmail = CustEmail.Text,
                    CustCell = CustCell.Text,

                    CustPassword = CustPassword.Text
                };


                CustName.Text = "";
                CustCell.Text = "";
                CustSurname.Text = "";
                CustEmail.Text = "";
                CustCell.Text = "";
                CustPassword.Text = "";

                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(myClient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Customer custs = JsonConvert.DeserializeObject<Customer>(data);
                    Toast.MakeText(this, "You are now registered", ToastLength.Long).Show();
                    Intent ip = new Intent(this, typeof(LoginActivity));
                    StartActivity(ip);
                    this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);



                }
            }


            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();


            }
           
        }
    }
}