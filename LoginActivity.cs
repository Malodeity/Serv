using System.Collections.Specialized;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Text;
using Org.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UberEatsV1.Models;
using Newtonsoft.Json;


namespace UberEatsV1
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        
        TextView text;
        HttpClient client;
        EditText txtE, txtP;
        Button btnLogin;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

           // text = FindViewById<TextView>(Resource.Id.txtCreateAcc);
            btnLogin = FindViewById<Button>(Resource.Id.btnSignIn);

            txtE = FindViewById<EditText>(Resource.Id.txtLoginCustEmail);
            txtP = FindViewById<EditText>(Resource.Id.txtLoginCustPassword);
            btnLogin.Click += Login_Click;


        }
        //LOGGING IN BUTTON
        private  void Login_Click(object sender, EventArgs e)
        {
           
            try
            {
                client = new HttpClient();

                DataAccess datA = new DataAccess();

                Customer cust = datA.GetCust(txtE.Text, txtP.Text);


                if (txtE.Text == cust.CustEmail && txtP.Text == cust.CustPassword)
                {
                    Toast.MakeText(this, "Welcome " + cust.CustName, ToastLength.Short).Show();
                    Intent ti = new Intent(this, typeof(RestaurantActivity));
                    StartActivity(ti);
                    this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
                }

                else if (String.IsNullOrEmpty(txtE.Text) && String.IsNullOrEmpty(txtP.Text))
                {
                    Toast.MakeText(this, "Please Provide Correct Information", ToastLength.Short).Show();
                }
               
                else
                {
                    Toast.MakeText(this, "Successfully", ToastLength.Short).Show();
                    Intent ti = new Intent(this, typeof(RestaurantActivity));
                    StartActivity(ti);
                    this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }


        }
    }
}
