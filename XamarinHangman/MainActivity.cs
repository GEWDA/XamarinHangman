using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLite;
using Android.Graphics;
using Android.Content;
using Android.Util;
using Java.IO;
using Android.Views;

namespace XamarinHangman
{
    /// <summary>
    /// A binder to bind the service to the activity
    /// </summary>
    public class MyBinder : Java.Lang.Object, IServiceConnection
    {
        MainActivity mainActivity;
        public bool IsConnected { get; set; }
        public Binder B { get; set; }
        public MyBinder(MainActivity activity)
        {
            IsConnected = false;
            B = null;
            mainActivity = activity;
        }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            B = service as Binder;
            IsConnected = this.B != null;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            IsConnected = false;
            B = null;
        }
    }

    [Activity(Label = "XamarinHangman", MainLauncher = true, Icon = "@drawable/bomb9",Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
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
            if(!(PlayMusic is null))
            {
                UnbindService(binder);
                StopService(PlayMusic);
                //PlayMusic = null;//in case you close and re-open app
            }
        }

        private void InitializeAllTheThings()
        {
            binder = new MyBinder(this);
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
            string DBPath = @"XamarinHangman/Assets/database/hangmanScores.sqlite";
            var AConn = new SQLiteAsyncConnection(DBPath);
            AConn.CreateTableAsync<Scores>().ContinueWith(t => { Log.Info("myDebug", "Scores table created"); });
            AConn.CreateTableAsync<Users>().ContinueWith(t => { Log.Info("myDebug", "Users table created"); });
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Intent StartGame = new Intent(this, typeof(GameActivity));
            StartActivity(StartGame);
            PlayMusic = new Intent(this, typeof(AudioService));
            //StartService(PlayMusic);
            if(!binder.IsConnected)
            {
                BindService(PlayMusic,binder,Bind.AutoCreate);
            }
        }
        private void BtnScores_Click(object sender, EventArgs e)
        {
            btnPlay.PerformClick();//FIX ME  FIX ME  FIX ME  FIX ME  FIX ME  FIX ME  FIX ME
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();//will contain delete query
        }
        private void BtnPlayers_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

