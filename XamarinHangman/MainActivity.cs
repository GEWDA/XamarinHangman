using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Graphics;
using Android.Content;

namespace XamarinHangman
{
    [Activity(Label = "XamarinHangman", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.MainMenu);
            InitializeAllTheThings();
        }

        private void InitializeAllTheThings()
        {
            Android.Content.Res.AssetManager[] manager = new Android.Content.Res.AssetManager[1];   //what on earth is going on here?            
                Typeface tf = Typeface.CreateFromAsset(manager[0], "spyware_nbp.tff");              //an array shouldn't be necessary, but otherwise this doesn't work
            

        }
    }
}

