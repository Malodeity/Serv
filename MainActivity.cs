using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace UberEatsV1
{
    [Activity(Label = "UberEats", MainLauncher = false, Icon = "@drawable/unuber")]
    public class MainActivity : Activity
    {
        Button btnToRegister;
        Button btnToLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            btnToRegister = FindViewById<Button>(UberEatsV1.Resource.Id.btnHRegister);
            btnToLogin = FindViewById<Button>(UberEatsV1.Resource.Id.btnHSignIn);

            // Get our button from the layout resource,
            // and attach an event to it
            btnToRegister.Click += btnToRegister_Click;
            btnToLogin.Click += btnToLogin_Click;
        }

        void btnToRegister_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
            this.OverridePendingTransition(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_top);
        }

        void btnToLogin_Click(object sender, EventArgs e)
        {
           
                        
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
        }

           
        }
    }


