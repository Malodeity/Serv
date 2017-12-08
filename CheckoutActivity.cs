
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using UberEatsV1.Models;

namespace UberEatsV1
{
    [Activity(Label = "CheckoutActivity", MainLauncher = false)]
    public class CheckoutActivity : Activity, ILocationListener
    {
        
        Button btnPlaceOrder;
        TextView txtAddress;
        HttpClient client;
        static string ProdN, ProdAmnt, ProdQ;


        static string url = "http://10.0.2.2:8080/api/AddOrder";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Checkout);

            // Create your application here

            _addressText = FindViewById<TextView>(Resource.Id.address_text);
            _locationText = FindViewById<TextView>(Resource.Id.location_text);
            FindViewById<TextView>(Resource.Id.get_address_button).Click += AddressButton_OnClick;

            InitializeLocationManager();


            btnPlaceOrder = FindViewById<Button>(Resource.Id.btnPlaceOrder);
            txtAddress = FindViewById<TextView>(Resource.Id.txtOrderAddress);
            btnPlaceOrder.Click += BtnPlaceOrder_Click;

            ISharedPreferences ccca = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = ccca.Edit();
            string prodname = ccca.GetString("ProdName", "");
            string amnt = ccca.GetString("Total", "");
            string quan = ccca.GetString("OrderQuantity", "");
            edit.Apply();


            ProdN = prodname;
            ProdAmnt = amnt;
            ProdQ = quan;

        }

        async void BtnPlaceOrder_Click(object sender, EventArgs e)
        {


            try
            {
                client = new HttpClient();
                var orderdeatils = new Order
                {
                    //PayCustId = Convert.ToInt32(edtUserId.Text),
                    OrderProdName = ProdN,
                    OrderQuantity = ProdQ,
                    OrderTotalAmnt = ProdAmnt,
                    OrderAddress = txtAddress.Text,



                };


               
                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(orderdeatils);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
               
           
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Order ord = JsonConvert.DeserializeObject<Order>(data);
                    Toast.MakeText(this, "Your Order Will Be Delivered To " + txtAddress.Text, ToastLength.Long).Show();
                    Intent ti = new Intent(this, typeof(PaymentActivity));
                    StartActivity(ti);
                    this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);

                }

                txtAddress.Text = "";



                ISharedPreferences ccca = Application.Context.GetSharedPreferences("User", FileCreationMode.Private);
                ISharedPreferencesEditor edit = ccca.Edit();
                string email = Intent.GetStringExtra("email");
                string passsword = Intent.GetStringExtra("password");

                DataAccess dt = new DataAccess();
                Customer cust = dt.GetCust(email, passsword);
                edit.PutString("CustId", cust.CustId.ToString());
                edit.Apply();

            }


            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();


            }


        }

        static readonly string TAG = "X:" + typeof(CheckoutActivity).Name;
        TextView _addressText;
        Location _currentLocation;
        LocationManager _locationManager;

        string _locationProvider;
        TextView _locationText;



        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                _locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                _locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
                Address address = await ReverseGeocodeCurrentLocation();
                DisplayAddress(address);
            }

        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }


        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (_currentLocation == null)
            {
                _addressText.Text = "Can't determine the current address. Try again in a few minutes.";


                return;
            }

            Address address = await ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
            txtAddress.Text = _addressText.Text;
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }

        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                _addressText.Text = deviceAddress.ToString();
            }
            else
            {
                _addressText.Text = "Unable to determine the address. Try again in a few minutes.";
            }
        }


     

    }
}
