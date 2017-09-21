using Android.App;
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

    //public class CustListener : Android.Widget.AdapterView.IOnItemSelectedListener
    //{
    //    public void OnItemSelected(AdapterView parent, View item, int position, long l)//might need AdapterView<Type>
    //    {

    //    }
    //    public void OnNothingSelected(AdapterView parent)//might need AdapterView<Type>
    //    {

    //    }

    //}

    [Activity(Label = "Hangman", MainLauncher = true, Icon = "@drawable/bomb9",Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class MainActivity : Activity
    {
        public class MySpinner:Spinner
        {
            AdapterView.IOnItemSelectedListener listener;
            public MySpinner(Context context, IAttributeSet attrs):base(context,attrs){ }
            public override void SetSelection(int position)
            {
                base.SetSelection(position);
                if (listener != null)
                { listener.OnItemSelected(this, this.SelectedView, position, 0); }
            }
            public void SetOnItemSelectedEvenWhenUnchangedListener(IOnItemSelectedListener theListener)//WHY IS THIS NOT IN BY DEFAULT???
            { this.listener = theListener; }
        }

        private LinearLayout mainMenu;
        private LinearLayout nukeMenu;
        private LinearLayout addMenu;
        private TextView mainTitle;
        private TextView confirmation;
        private TextView confirmationSub;
        private Button nukeCancel;
        private ImageButton btnPlay;
        private ImageButton btnScores;
        private ImageButton btnSettings;
        private ImageButton btnPlayers;
        private Button btnNuke;
        private Spinner usersSpinner;
        private ArrayAdapter<string> userArrayAdapter;
        private string[] nameArray= new string[0];
        string DBPath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "hangmanScores.sqlite");
        private SQLiteAsyncConnection AConn;
        Intent PlayMusic;
        Typeface spywareFont;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.MainMenu);
            LoadDB();
            AConn = new SQLiteAsyncConnection(DBPath);
            AConn.CreateTableAsync<Scores>().ContinueWith(t => { Log.Info("myDebug", "Scores table created"); });
            AConn.CreateTableAsync<Users>().ContinueWith(t => { Log.Info("myDebug", "Users table created"); });
            InitializeAllTheThings();
        }

        public override void OnBackPressed()
        {
            if (!mainMenu.Activated)
            {
                mainMenu.BringToFront();
                nukeMenu.Activated = false;
                addMenu.Activated = false;
                mainMenu.Activated = true;
            }
            else { base.OnBackPressed(); }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(!(PlayMusic is null))
            {
                StopService(PlayMusic);
                PlayMusic = null;//in case you close and re-open app
            }
        }

        private void InitializeAllTheThings()
        {
            mainTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            mainTitle.Typeface = spywareFont;
            mainMenu = FindViewById<LinearLayout>(Resource.Id.MainMenuLayout);
            nukeMenu = FindViewById<LinearLayout>(Resource.Id.NukeMenuLayout);
            addMenu = FindViewById<LinearLayout>(Resource.Id.AddUserLayout);
            addMenu.Activated = false;
            nukeMenu.Activated = false;
            mainMenu.Activated = true;
            mainMenu.BringToFront();


            usersSpinner = FindViewById<Spinner>(Resource.Id.invisibleSpinner);
            //usersSpinner = (MySpinner)aSpinner;
            //UpdateSpinner();
            usersSpinner.ItemSelected += UsersSpinner_ItemSelected;

            btnPlay = FindViewById<ImageButton>(Resource.Id.imageButtonPlay);
            btnPlay.Click += BtnPlay_Click;
            btnScores = FindViewById<ImageButton>(Resource.Id.imageButtonScores);
            btnScores.Click += BtnScores_Click;
            btnSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            btnSettings.Click += BtnSettings_Click;
            btnPlayers = FindViewById<ImageButton>(Resource.Id.imageButtonPlayers);
            btnPlayers.Click += BtnPlayers_Click;

            nukeCancel = FindViewById<Button>(Resource.Id.buttonAbort);
            nukeCancel.Typeface = spywareFont;
            nukeCancel.Click += NukeDB;
            confirmation = FindViewById<TextView>(Resource.Id.txtConfirmation);
            confirmation.Typeface = spywareFont;
            confirmationSub = FindViewById<TextView>(Resource.Id.txtConfirmationSubtext);
            confirmationSub.Typeface = spywareFont;
            btnNuke = FindViewById<Button>(Resource.Id.buttonNuke);
            btnNuke.Typeface = spywareFont;
            btnNuke.Click += NukeDB;
        }

        private void UpdateCurrentUser(object sender, View.FocusChangeEventArgs e)
        {
            Log.Info("myDebug", "UpdateCurrentUser triggered!");
            //if (true)
            //{
            //    addMenu.Activated = true;
            //    nukeMenu.Activated = false;
            //    mainMenu.Activated = false;
            //    addMenu.BringToFront();
            //}
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
            userArrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, nameArray);//fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  fix me  
            userArrayAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //set background colour, and text colour
            usersSpinner.Adapter = userArrayAdapter;
            //usersSpinner.Click += UpdateCurrentUser;//Java.Lang.RuntimeException: Don't call setOnClickListener for an AdapterView. You probably want setOnItemClickListener instead
            //usersSpinner.ItemClick += UpdateCurrentUser;//Java.Lang.RuntimeException: setOnItemClickListener cannot be used with a spinner.
        }

        private void UsersSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Log.Info("myDebug", "IT WORKS");
            Toast.MakeText(this,"This feature is currently unavailable",ToastLength.Long).Show();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (!(usersSpinner.SelectedItem is null) && usersSpinner.SelectedItem.ToString()!="New User...")
            {
                Log.Info("myDebug", "Works");
            }
            Intent StartGame = new Intent(this, typeof(GameActivity));
            StartActivity(StartGame);
            PlayMusic = new Intent(this, typeof(AudioService));
            StartService(PlayMusic);
        }
        private void BtnScores_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "The highscores feature is currently unavailable,\nlaunching game instead...", ToastLength.Long).Show();
            btnPlay.PerformClick();//FIX ME  FIX ME  FIX ME  FIX ME  FIX ME  FIX ME  FIX ME
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            addMenu.Activated = false;
            nukeMenu.Activated = true;
            mainMenu.Activated = false;
            nukeMenu.BringToFront();
        }
        private void BtnPlayers_Click(object sender, EventArgs e)
        {
            GetUsersNames(AConn.Table<Users>().ToListAsync());
        }




        //~DB CALLS/RESULTS
#region ~DB CALLS/RESULTS
        private void GetUsersNames(System.Threading.Tasks.Task<System.Collections.Generic.List<Users>> a)
        {
            Log.Info("MyDebug", "Found " + nameArray.Length.ToString() + " users");
            if (nameArray.Length != a.Result.Count)
            {
                nameArray = new string[a.Result.Count];
                for (int i = 0; i < a.Result.Count; i++)
                {
                    nameArray[i] = a.Result[i].Name;
                }
                Log.Info("MyDebug", "Now has found " + nameArray.Length.ToString() + " users");
                UpdateSpinner();
            }
            usersSpinner.PerformClick();
        }
        private void NukeDB(object sender, EventArgs e)
        {
            if(sender==btnNuke)
            {
                AConn.ExecuteAsync("DELETE * FROM Scores");
                AConn.ExecuteAsync("DELETE * FROM Users WHERE UserID != 1");//leaves the special create new user record
                Log.Info("myDebug", "DB Nuked");
            }
            else { Log.Info("myDebug", "DB NOT nuked"); }
            mainMenu.BringToFront();
            nukeMenu.Activated = false;
            mainMenu.Activated = true;

        }

#endregion
    }
}

