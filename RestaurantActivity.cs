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
    [Activity(Label = "RestaurantActivity", MainLauncher = false)]
    public class RestaurantActivity : Activity
    {
        static string uri = @"http://10.0.2.2:8080/api/GetRestaurant";
        public static Context conxt;
        private static List<Restaurant> res = new List<Restaurant>();
        static ListView listRestaurants;

        //static Button btnViewRes;


       // private SearchView search;
        private static ArrayList RRestaurant;
        private ArrayAdapter<Restaurant> adatp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Restaurant);
            // Create your application here



            listRestaurants = FindViewById<ListView>(Resource.Id.listView1);
            GetRes staurant = new GetRes();
            staurant.Execute();

            SearchView search = FindViewById<SearchView>(Resource.Id.searchView1);
            search.SetQueryHint("Search Location");

            listRestaurants.Adapter = new ProImageAdapter(this, res);
            listRestaurants.ItemClick += ListProp_ItemClick;
           // search.QueryTextChange += Search_QueryTextChange;

            search.QueryTextSubmit += (sender, e) =>
            {
               Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;

            };


        }


        //protected void Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        //{
        //    adatp.Filter.InvokeFilter(e.NewText);

        //}

        private void ListProp_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ProductsActivity));
            StartActivity(intent);
            this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
        }


        public class GetRes : AsyncTask
        {
            
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                HttpClient client = new HttpClient();

                Uri url = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                var restu = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<Restaurant>>(restu);

                foreach (var g in result)
                {
                    res.Add(g);
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
                listRestaurants.Adapter = new ProImageAdapter(conxt, res);
            }
        }
        public class ProImageAdapter : BaseAdapter<Restaurant>
        {
            private List<Restaurant> prope = new List<Restaurant>();
            static Context context;
            public ProImageAdapter(Context con, List<Restaurant> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;
                this.NotifyDataSetChanged();
            }

            public override Restaurant this[int position]
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
                    propertie = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListDesign, parent, false);
                }
                TextView txtName = propertie.FindViewById<TextView>(Resource.Id.txtResName);
                TextView txtAddress = propertie.FindViewById<TextView>(Resource.Id.txtResAddress);
                TextView txtCity = propertie.FindViewById<TextView>(Resource.Id.txtResCity);
                ImageView imm = propertie.FindViewById<ImageView>(Resource.Id.imageView1);

                if (prope[position].ResImage != null)
                {
                    imm.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].ResImage, 0, prope[position].ResImage.Length));
                }

                txtName.Text = prope[position].ResName;
                txtAddress.Text = prope[position].ResAddress;
                txtCity.Text = prope[position].ResCity;
                imm.Tag = prope[position].ResImage;


                return propertie;
            }
        }


    }
}

