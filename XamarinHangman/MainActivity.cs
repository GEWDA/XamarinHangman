using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Graphics;
using Android.Content;
using Android.Util;
using Java.IO;
using Android.Views;

namespace XamarinHangman
{
    public class MyBinder : Java.Lang.Object, IServiceConnection
    {
        MainActivity mainActivity;
        public bool IsConnected { get; set; }
        public Binder b { get; set; }
        public MyBinder(MainActivity activity)
        {
            IsConnected = false;
            b = null;
            mainActivity = activity;
        }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            b = service as Binder;
            IsConnected = this.b != null;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            IsConnected = false;
            b = null;
        }
    }

    [Activity(Label = "XamarinHangman", MainLauncher = true, Icon = "@drawable/icon",Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class MainActivity : Activity
    {
        private TextView mainTitle;
        private ImageButton btnPlay;
        private ImageButton btnScores;
        private ImageButton btnSettings;
        private ImageButton btnPlayers;
        private MyBinder binder;
        Intent PlayMusic;
        Typeface spywareFont;
        //public User CurrentPlayer;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.MainMenu);
            InitializeAllTheThings();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(!(binder is null))
            {
                UnbindService(binder);//ISN'T ACTUALLY RUNNING. LINE IS REACHED, BUT NOTHING IS UNBOUND
                
            }
        }

        private void InitializeAllTheThings()
        {
            mainTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            mainTitle.Typeface = spywareFont;
            btnPlay = FindViewById<ImageButton>(Resource.Id.imageButtonPlay);
            btnPlay.Click += BtnPlay_Click;
            btnScores = FindViewById<ImageButton>(Resource.Id.imageButtonScores);
            btnScores.Click += BtnScores_Click;
            btnSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            btnSettings.Click += BtnSettings_Click;
            btnPlayers = FindViewById<ImageButton>(Resource.Id.imageButtonPlayers);
            btnPlayers.Click += BtnPlayers_Click;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Intent StartGame = new Intent(this, typeof(GameActivity));
            StartActivity(StartGame);
            PlayMusic = new Intent(this, typeof(AudioService));
            //StartService(PlayMusic);
            binder = new MyBinder(this);
            BindService(PlayMusic,binder,Bind.AutoCreate);
        }
        private void BtnScores_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void BtnPlayers_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

