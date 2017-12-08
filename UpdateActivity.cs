
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UberEatsV1.Models;

namespace UberEatsV1
{
    [Activity(Label = "UpdateActivity")]
    public class UpdateActivity : Activity
    {


        EditText txtId, txtFirst, txtLast, txtEmail, txtCell;
        Button btnUpdate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Update);

            var loggedOn = Application.Context.GetSharedPreferences("CustomerList", FileCreationMode.Private);


            txtFirst = FindViewById<EditText>(Resource.Id.txtUpName);
            txtLast = FindViewById<EditText>(Resource.Id.txtUpSurname);
            txtEmail = FindViewById<EditText>(Resource.Id.txtUpEmail);
            txtCell = FindViewById<EditText>(Resource.Id.txtUpCell);



            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            btnUpdate.Click += BtnUpdate_ClickAsync;


            txtFirst.SetText(loggedOn.GetString("@CustName", null), TextView.BufferType.Editable);
            txtLast.SetText(loggedOn.GetString("@CustSurname", null), TextView.BufferType.Editable);
            txtEmail.SetText(loggedOn.GetString("CustEmail", null), TextView.BufferType.Editable);
            txtCell.SetText(loggedOn.GetString("CustCell", null), TextView.BufferType.Editable);

        }

        private async void BtnUpdate_ClickAsync(object sender, EventArgs e)
        {
            //try
            //{
            //    ServiceUb serve = new ServiceUb();
            //    serve.Update(new Customer(txtFirst.Text, txtLast.Text, txtEmail.Text, txtCell.Text));

            //    Toast.MakeText(this, "Your information is updated", ToastLength.Short).Show();
            //    Intent inter = new Intent(this, typeof(MainActivity));
            //    StartActivity(inter);
            //}
            //catch (Exception error)
            //{
            //    Toast.MakeText(this, error.ToString(), ToastLength.Short).Show();
            //    Intent inters = new Intent(this, typeof(LoginActivity));
            //    StartActivity(inters);

            //}

        }

    }
}
