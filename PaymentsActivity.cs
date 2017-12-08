
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
    [Activity(Label = "PaymentActivity")]
    public class PaymentActivity : Activity

    {


        EditText edtPayCardNum, edtPayExp, edtPayCVV, edtUserId;
        HttpClient client;
        Button btnMakePay;
        static string realcustid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Payment);


            edtPayCardNum = FindViewById<EditText>(Resource.Id.txtCardNum);
            edtPayExp = FindViewById<EditText>(Resource.Id.txtCardExp);
            edtPayCVV = FindViewById<EditText>(Resource.Id.txtCVV);
            edtUserId = FindViewById<EditText>(Resource.Id.txtUserId);

            btnMakePay = FindViewById<Button>(Resource.Id.btnMakePayment);




            btnMakePay.Click += BtnMakePay_Click;


            ISharedPreferences ccca = Application.Context.GetSharedPreferences("User", FileCreationMode.Private);
            ISharedPreferencesEditor edit = ccca.Edit();
            string cusId = ccca.GetString("CustId", "yep");
            edit.Apply();

            edtUserId.Text = cusId;
        }


        //MAKE PAYMENT BUTTON
        async void BtnMakePay_Click(object sender, EventArgs e)
        {

            try
            {
                client = new HttpClient();
                var myClient = new Payment
                {
                    PayCustId = Convert.ToInt32(edtUserId.Text),
                    PayCardNum = edtPayCardNum.Text,
                    PayExp = edtPayExp.Text,
                    PayCVV = edtPayCVV.Text,
                   
                };

                edtUserId.Text = ""; 
                edtPayCardNum.Text = "";
                edtPayExp.Text = "";
                edtPayCVV.Text = "";

                string url = "http://10.0.2.2:8080/api/Payment";
                var uri = new System.Uri(string.Format(url));
                //var json = JsonConvert.SerializeObject(myClient);
                var content = new StringContent(JsonConvert.SerializeObject(myClient), Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Payment pay = JsonConvert.DeserializeObject<Payment>(data);
                    Toast.MakeText(this, "Payment Successfully", ToastLength.Long).Show();
                    Intent ti = new Intent(this, typeof(trackOrderActivity));
                    StartActivity(ti);
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
