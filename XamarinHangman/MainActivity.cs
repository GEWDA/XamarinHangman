using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Graphics;
using Android.Content;
using Java.IO;

namespace XamarinHangman
{
    [Activity(Label = "XamarinHangman", MainLauncher = true, Icon = "@drawable/icon",Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class MainActivity : Activity
    {
        private TextView mainTitle;
        private Button btnPlay;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.MainMenu);
            InitializeAllTheThings();
        }

        private void InitializeAllTheThings()
        {
            //InputStream input =  Assets.Open("fonts/spyware_nbp.tff");
            
            mainTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            Typeface spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            mainTitle.Typeface = spywareFont;
        }
    }
}

