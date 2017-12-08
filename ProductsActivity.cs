using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Android.Graphics;
using Android.Views;
using Android.Text;
using System;
using System.Collections;
using System.Linq;
using UberEatsV1.Models;

namespace UberEatsV1
{
    [Activity(Label = "ProductsActivity", MainLauncher = false)]
    public class ProductsActivity : Activity

    {
        static string uri = @"http://10.0.2.2:8080/api/GetProduct";
        public static Context conxt;
        private static List<Product> prod = new List<Product>();
        static ListView listProducts;
        TextView txtProdName;
        TextView txtProdDesc;
        TextView txtProdPrice;
        ImageView imm;
        static decimal interim = 0;
        static int quantity = 0;

        private static ArrayList RRestaurant;
        private ArrayAdapter<Product> adatp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Product);

           
            txtProdName = FindViewById<TextView>(Resource.Id.txtProdName);
            txtProdDesc = FindViewById<TextView>(Resource.Id.txtProdDesc);
            txtProdPrice = FindViewById<TextView>(Resource.Id.txtProdPrice);
            imm = FindViewById<ImageView>(Resource.Id.imageView1);

            listProducts = FindViewById<ListView>(Resource.Id.listView1);
            GetProd staurant = new GetProd();
            staurant.Execute();

            listProducts.Adapter = new ProImageAdapter(this, prod);
            listProducts.ItemClick += ListProp_ItemClick;

           
        }

        private void Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //adtp.Filter.InvokeFilter(e.NewText);

            //listProp.TextFilter(e.NewText);
        }

        private void ListProp_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, adatp.GetItem(e.Position).ToString(), ToastLength.Long).Show();
        }


        public class GetProd : AsyncTask
        {
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                HttpClient client = new HttpClient();

                Uri url = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                var restu = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<Product>>(restu);

                foreach (var g in result)
                {
                    prod.Add(g);
                }
                return true;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
                listProducts.Adapter = new ProImageAdapter(conxt, prod);
            }
        }
        public class ProImageAdapter : BaseAdapter<Product>
        {
            private List<Product> prope = new List<Product>();
            static Context context;
            //EventHandler buttonClickHandler;
            public ProImageAdapter(Context con, List<Product> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;
                this.NotifyDataSetChanged();
            }
            public override Product this[int position]
            {
                get
                {
                    return prope[position];
                }
            }

            public override int Count
            {
                get
                {
                    return prope.Count;
                }
            }
            public Context Mcontext
            {
                get;
                private set;
            }
            public override long GetItemId(int position)
            {
                return position;
            }

            public Bitmap getBitmap(byte[] getByte)
            {
                if (getByte.Length != 0)
                {
                    return BitmapFactory.DecodeByteArray(getByte, 0, getByte.Length);
                }
                else
                {
                    return null;
                }
            }


            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View propertie = convertView;
                if (propertie == null)
                {
                    propertie = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ProductList, parent, false);
                }
                TextView txtProdName = propertie.FindViewById<TextView>(Resource.Id.txtProdName);
                TextView txtProdDesc = propertie.FindViewById<TextView>(Resource.Id.txtProdDesc);
                TextView txtProdPrice = propertie.FindViewById<TextView>(Resource.Id.txtProdPrice);
                ImageView imm = propertie.FindViewById<ImageView>(Resource.Id.imageView1);

                if (prope[position].ProdImage != null)
                {
                    imm.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].ProdImage, 0, prope[position].ProdImage.Length));
                }



                txtProdName.Text = prope[position].ProdName;
                txtProdDesc.Text = prope[position].ProdDesc;
                txtProdPrice.Text = "R" + Convert.ToString(prope[position].ProdPrice);
                imm.Tag = prope[position].ProdImage;

                var Button = propertie.FindViewById<Button>(Resource.Id.btnAddCart);
                Button.Click += buttonClickHandler;

                void buttonClickHandler(object sender, EventArgs e)
                {

                    Intent ti = new Intent();
                    decimal total = 0;
                    total = prope[position].ProdPrice;
                        interim += total;
                    quantity++;

                        ISharedPreferences ccca = Application.Context.GetSharedPreferences("CartInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor edit = ccca.Edit();
                        edit.PutString("ProdName", txtProdName.Text.Trim());
                        edit.PutString("ProdDesc", txtProdDesc.Text.Trim());
                        edit.PutString("ProdPrice", txtProdPrice.Text.Trim());
                        edit.PutString("Total",interim.ToString());
                        edit.PutString("Quantity", quantity.ToString());
                        edit.Apply();

                }




                return propertie;
            }


        }

        // Create your application here

        //MENU


        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.TheeMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.viewCart:
                    var intent = new Intent(this, typeof(CartActivity));
                    StartActivity(intent);
                    return true;
                case Resource.Id.Viewres:
                    var intents = new Intent(this, typeof(RestaurantActivity));
                    StartActivity(intents);
                    return true;
                default:
                    return false;
            }
        }





        }
    }
