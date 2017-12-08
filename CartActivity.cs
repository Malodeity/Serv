
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
using Newtonsoft.Json;
using UberEatsV1.Models;
using System.Net.Http;

namespace UberEatsV1
{
    [Activity(Label = "CartActivity")]
    public class CartActivity : Activity

    {
        TextView txtProdName;
        TextView txtProdDesc;
        TextView txtProdPrice;
        TextView txtTotal;
        TextView txtQuantity;
        private List<string> MyItems;
        private ListView VView;
        Button btnCheckout;
        HttpClient client;
       


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.CartList);
            txtProdName = FindViewById<TextView>(Resource.Id.txtProdName);
            txtProdDesc = FindViewById<TextView>(Resource.Id.txtProdDesc);
            txtProdPrice = FindViewById<TextView>(Resource.Id.txtProdPrice);
            VView = FindViewById<ListView>(Resource.Id.listAhh1);
            txtTotal = FindViewById<TextView>(Resource.Id.txttotal);
            txtQuantity = FindViewById<TextView>(Resource.Id.txtQuantity);
            btnCheckout = FindViewById<Button>(Resource.Id.btnCheck);

            btnCheckout.Click += BtnCheckout_Click;


            ISharedPreferences ccca = Application.Context.GetSharedPreferences("CartInfo", FileCreationMode.Private);

            string ProductName = ccca.GetString("ProdName", string.Empty);
            string ProductDesc = ccca.GetString("ProdDesc", string.Empty);
            string ProductPrice = ccca.GetString("ProdPrice", string.Empty);
            string total = ccca.GetString("Total", string.Empty);
            string quantity = ccca.GetString("Quantity", string.Empty);


            txtProdName.Text =  ProductName;
            txtProdDesc.Text = "Desc: " + ProductDesc;
            txtProdPrice.Text = Convert.ToString(ProductPrice);
            txtTotal.Text = total;
            txtQuantity.Text = quantity;

            MyItems = new List<string>();
            MyItems.Add(txtProdName.Text);
            MyItems.Add(txtProdDesc.Text);
            MyItems.Add(txtProdPrice.Text);
            MyItems.Add(txtTotal.Text);
            MyItems.Add(txtQuantity.Text);

            ArrayAdapter<string> adapt = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, MyItems);


            VView.Adapter = adapt;


            ISharedPreferencesEditor edit = ccca.Edit();
            edit.Clear();
            edit.Apply();

        }


        void BtnCheckout_Click(object sender, EventArgs e)
        {

            client = new HttpClient();
            Toast.MakeText(this, "Complete Order Details", ToastLength.Long).Show();
            Intent ti = new Intent(this, typeof(CheckoutActivity));
            ISharedPreferences ccca = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = ccca.Edit();
           
            edit.PutString("ProdName", txtProdName.Text);
            edit.PutString("Total", txtTotal.Text); 
            edit.PutString("OrderQuantity", txtQuantity.Text); 
            edit.Apply();
            StartActivity(ti);
            this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
           

        }
    }

    }

