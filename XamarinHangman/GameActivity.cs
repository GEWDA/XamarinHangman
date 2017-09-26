using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;
using System.Threading;
using System.IO;
using System.Reflection;
using SQLite;
namespace XamarinHangman
{
    [Activity(Label = "GameActivity",Icon ="@drawable/bomb9", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class GameActivity : Activity
    {
        string theWord = "annoying" ;//default value
        string[] Allwords;
        char[] wordCheck;
        char[] wordDisplay;
        bool IsGameWon;
        int incorrectGuesses = 0;
        int score = 0;
        //private SQLiteAsyncConnection AConn;
        string DBPath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "hangmanScores.sqlite");
        ImageView Bomb;
        TextView WordView;
        Button[] AllLetters = new Button[26];
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //AConn = new SQLiteAsyncConnection(DBPath);
            //AConn.CreateTableAsync<Scores>().ContinueWith(t => { Log.Info("myDebug", "Scores table created in game"); });
            //AConn.CreateTableAsync<Users>().ContinueWith(t => { Log.Info("myDebug", "Users table created in game"); });
            SetContentView(Resource.Layout.GameScreen);
            InitializeTheGame();
        }
        protected override void OnDestroy()
        {
            object[] record = { score, theWord };
            //UpdateScores(record);
            //UPDATE SCORES HERE   UPDATE SCORES HERE   UPDATE SCORES HERE   UPDATE SCORES HERE
            base.OnDestroy();
        }



        private void InitializeTheGame()
        {
            Random r = new Random();
            Assembly assembly = typeof(GameActivity).GetTypeInfo().Assembly;
            Stream s = assembly.GetManifestResourceStream("XamarinHangman.Resources.raw.WordList.txt");
            System.IO.StreamReader reader = new System.IO.StreamReader(s);
            Allwords=reader.ReadToEnd().Split(Convert.ToChar("\n"));
            theWord = Allwords[r.Next(0,Allwords.Length)].ToLower().Replace("-",String.Empty).Replace("\r",String.Empty);//selects random word from the list, removes capital letters and dashes
            //theWord.TrimEnd("\r".ToCharArray());//doesn't actually trim

            Typeface spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            Bomb = FindViewById<ImageView>(Resource.Id.imageViewBomb);
            WordView = FindViewById<TextView>(Resource.Id.textViewWord);
            WordView.Typeface = spywareFont;
            #region word initialization
            wordCheck = theWord.ToCharArray();
            wordDisplay = new char[wordCheck.Length*2-1];//multiply by 2 in order to add spaces in between
            for(int i = 0;i<wordDisplay.Length;i+=2)
            {
                wordDisplay[i] = Convert.ToChar("_");
                if(i<wordDisplay.Length-1)
                {
                    wordDisplay[i + 1] = Convert.ToChar(" ");
                }
            }
            UpdateText();
#endregion
            #region alphabet buttons initialization
            int exceptions = 0;
            for (int i = 0; i < 28; i++)//28, due to exactly two deliberate exceptions
            {
                if(FindViewById(Resource.Id.button1+i).GetType().Equals(FindViewById(Resource.Id.button1).GetType()))
                {
                    AllLetters[i-exceptions] = FindViewById<Button>(Resource.Id.button1+i); //all buttons were created at the same time, so their ids are adjacent ints
                    AllLetters[i-exceptions].Typeface=spywareFont;                          //with the exception of the last two keyboard row containers
                    AllLetters[i-exceptions].Text = AllLetters[i-exceptions].Tag.ToString().ToUpper();
                    AllLetters[i-exceptions].Click += AlphabetButton_Click;
                }
                else { exceptions++; }
            }
            if (exceptions > 2) { Log.Warn("myDebug", "Warning: More than two exceptions have occurred while initializing keyboard"); }
#endregion
        }

        private void AlphabetButton_Click(object sender, EventArgs e)
        {
            Button theSender = (Button)sender;
            theSender.Enabled = false;
            theSender.Text = "";
            if (!theWord.Contains(theSender.Tag.ToString()))
            {
                ChangeImage();
            }
            else
            {
                for(int i=0;i<wordCheck.Length;i++)
                {
                    if(theSender.Tag.ToString()==Convert.ToString(wordCheck[i]))
                    {
                        wordDisplay[i * 2] = Convert.ToChar(theSender.Tag.ToString());
                        UpdateText();
                    }
                }
            }
        }

        private void UpdateText()
        {
            WordView.Text = new string(wordDisplay);
            if(!WordView.Text.Contains("_"))
            {
                GameWon();
            }
        }



        private void ChangeImage()
        {
            incorrectGuesses++;
            Bomb.SetImageResource(Resource.Drawable.bomb1 + incorrectGuesses);//bomb resources ids are adjacent
            if (incorrectGuesses>8)
            {
                incorrectGuesses = 8;
                GameLost();
            }
        }

        private void GameWon()
        {
            IsGameWon = true;
            Toast.MakeText(this, "You Win", ToastLength.Long).Show();
            DisableButtons();
        }

        private void GameLost()
        {
            Toast.MakeText(this, "Game Over", ToastLength.Short).Show();
            Toast.MakeText(this, "Word:\t" + theWord, ToastLength.Long).Show();
            DisableButtons();
        }
        private void DisableButtons()
        {
            for(int i=0; i<AllLetters.Length;i++)
            {
                if (AllLetters[i].Enabled)//for each letter untried after winning
                {
                    if(IsGameWon)
                    {
                        score++;
                    }
                    else
                    {
                        score--;
                    }
                }
                AllLetters[i].Enabled = false;
            }
            Toast.MakeText(this, "Score:\t" + score.ToString(), ToastLength.Long).Show();
            //UpdateScores(score,theWord,isGameWon)//this is where i WOULD update the scores and the number of games the user has played

            Bomb.Click += Bomb_Click;
            Toast again = Toast.MakeText(this, "Click the picture to play again",ToastLength.Long);
            again.SetGravity(GravityFlags.Center, 0, 0);
            again.Show();
        }

        private void Bomb_Click(object sender, EventArgs e)
        {
            Recreate();
        }



        //DB Calls
        private void UpdateScores(object[] record)
        {
            //var e=AConn.Table<Users>().Where(x => x.IsCurrentUser == 1).FirstAsync();
            //AConn.ExecuteAsync("INSERT INTO Scores (score, word, UsersIDFK) VALUES ("+record[0].ToString()+", "+record[1].ToString()+","+ AConn.Table<Users>().Where(x=>x.IsCurrentUser==1).FirstAsync() + ")");
        }
    }
}