
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UberEatsV1
{
    [Activity(Label = "trackOrderActivity", MainLauncher = true)]
    public class trackOrderActivity : Activity
    {

        int count = 7;
        int timefrom = 7;
        TextView timerViewer;
        private System.Threading.Timer timer;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TrackOrder);
            // Create your application he

          

            timerViewer = FindViewById<TextView>(Resource.Id.textView2);
            timer = new Timer(x => UpdateView(), timefrom, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
            Toast.MakeText(this, "Your order will be delivered in " + count + "Minutes", ToastLength.Short).Show();
        }

        private void UpdateView()
        {
            


            if(count > 0)
            {
                
                this.RunOnUiThread(() => timerViewer.Text = string.Format("{0} Minute(s) Left!", count--));
            }
            else
            {
                this.RunOnUiThread(() => timerViewer.Text = "Your order is here, Thank you for using UberEats ");
            }
        }
    }
}
