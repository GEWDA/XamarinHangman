﻿using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using SQLite;
using Android.Graphics;
using Android.Content;
using Android.Util;
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
        private Spinner usersSpinner;
        private ArrayAdapter<string> userArrayAdapter;
        private string[] nameArray= new string[0];
        string DBPath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "hangmanScores.sqlite");
        private SQLiteAsyncConnection AConn;
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
                PlayMusic = null;//in case you close and re-open app
            }
        }

        private void InitializeAllTheThings()
        {
            LoadDB();
            AConn = new SQLiteAsyncConnection(DBPath);
            AConn.CreateTableAsync<Scores>().ContinueWith(t => { Log.Info("myDebug", "Scores table created"); });
            AConn.CreateTableAsync<Users>().ContinueWith(t => { Log.Info("myDebug", "Users table created"); });

            binder = new MyBinder(this);
            mainTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            mainTitle.Typeface = spywareFont;

            usersSpinner = FindViewById<Spinner>(Resource.Id.invisibleSpinner);
            UpdateSpinner();

            btnPlay = FindViewById<ImageButton>(Resource.Id.imageButtonPlay);
            btnPlay.Click += BtnPlay_Click;
            btnScores = FindViewById<ImageButton>(Resource.Id.imageButtonScores);
            btnScores.Click += BtnScores_Click;
            btnSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            btnSettings.Click += BtnSettings_Click;
            btnPlayers = FindViewById<ImageButton>(Resource.Id.imageButtonPlayers);
            btnPlayers.Click += BtnPlayers_Click;
        }

        private void LoadDB()
        {
            using (BinaryReader br = new BinaryReader(Assets.Open(@"database/hangmanScores.sqlite")))
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(DBPath, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int len = 0;
                    while ((len=br.Read(buffer,0,buffer.Length))>0)
                    {
                        bw.Write(buffer, 0, len);
                    }
                }
            }
        }

        private void UpdateSpinner()
        {
            userArrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, nameArray);
            userArrayAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            usersSpinner.Adapter = userArrayAdapter;
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
            btnPlayers.Enabled = false;//no spamming if it lags
            GetUsersNames(AConn.Table<Users>().ToListAsync());
        }

        private void GetUsersNames(System.Threading.Tasks.Task<System.Collections.Generic.List<Users>> a)
        {
            if (nameArray.Length != a.Result.Count)//if updating a user, will need to explicitly refresh
            {
                nameArray = new string[a.Result.Count];
                for (int i = 0; i < a.Result.Count; i++)
                {
                    nameArray[i] = a.Result[i].Name;
                }
                UpdateSpinner();
                usersSpinner.PerformClick();
                btnPlayers.Enabled = true;
            }
        }
    }
}

