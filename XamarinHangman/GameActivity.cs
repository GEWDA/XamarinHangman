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

namespace XamarinHangman
{
    [Activity(Label = "GameActivity",Icon ="@drawable/bomb9", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class GameActivity : Activity
    {
        string theWord = "annoying";
        char[] wordCheck;
        char[] wordDisplay;
        bool IsGameWon;
        int incorrectGuesses = 0;
        int score = 0;
        ImageView Bomb;
        TextView WordView;
        Button[] AllLetters = new Button[26];
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameScreen);
            InitializeTheGame();
        }

        private void InitializeTheGame()
        {
            Typeface spywareFont = Typeface.CreateFromAsset(Assets, "fonts/spyware.ttf");
            Bomb = FindViewById<ImageView>(Resource.Id.imageViewBomb);
            WordView = FindViewById<TextView>(Resource.Id.textViewWord);
            WordView.Typeface = spywareFont;
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

            int exceptions = 0;
            for (int i = 0; i < 28; i++)
            {
                if(FindViewById(Resource.Id.button1+i).GetType().Equals(FindViewById(Resource.Id.button1).GetType()))
                {
                    AllLetters[i-exceptions] = FindViewById<Button>(Resource.Id.button1+i);//all buttons were created at the same time, so their ids are adjacent ints
                    AllLetters[i-exceptions].Typeface=spywareFont;
                    AllLetters[i-exceptions].Text = AllLetters[i-exceptions].Tag.ToString().ToUpper();
                    AllLetters[i-exceptions].Click += AlphabetButton_Click;
                }
                else { exceptions++; }
            }
            if (exceptions > 2) { Log.Warn("myDebug", "Warning: More than two exceptions have occurred while initializing keyboard"); }
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
            Bomb.SetImageResource(Resource.Drawable.bomb1 + incorrectGuesses);
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
            Toast.MakeText(this, "Game Over", ToastLength.Long).Show();
            DisableButtons();
        }
        private void DisableButtons()
        {
            for(int i=0; i<AllLetters.Length;i++)
            {
                if (AllLetters[i].Enabled)// || IsGameWon && Convert.ToString(wordCheck).Contains(AllLetters[i].Tag.ToString()))//for each letter untried after winning //OR required in the word?
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
        }
    }
}