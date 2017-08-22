using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Graphics;
using Android.Content;
using Java.IO;
using Android.Views;

namespace XamarinHangman
{
    [Activity(Label = "XamarinHangman", MainLauncher = true, Icon = "@drawable/icon",Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class MainActivity : Activity
    {
        private TextView mainTitle;
        private ImageButton btnPlay;
        private ImageButton btnScores;
        private ImageButton btnSettings;
        private ImageButton btnPlayers;
        //public User CurrentPlayer;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.MainMenu);
            InitializeAllTheThings();
        }

        private void InitializeAllTheThings()
        {

            mainTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            Typeface spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            mainTitle.Typeface = spywareFont;
            btnPlay = FindViewById<ImageButton>(Resource.Id.imageButtonPlay);
            btnPlay.Click += BtnPlay_Click;
            btnScores = FindViewById<ImageButton>(Resource.Id.imageButtonScores);
            btnSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            btnPlayers = FindViewById<ImageButton>(Resource.Id.imageButtonPlayers);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

