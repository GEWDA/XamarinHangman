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

namespace XamarinHangman
{
    [Activity(Label = "GameActivity",Icon ="@drawable/bomb9", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class GameActivity : Activity
    {
        string theWord = "annoying";
        int incorrectGuesses = 0;
        ImageView Bomb;
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
        }

        private void AlphabetButton_Click(object sender, EventArgs e)
        {
            Button theSender = (Button)sender;
            if (!theWord.Contains(theSender.Tag.ToString()))
            {
                ChangeImage();
            }

        }

        private void ChangeImage()
        {
            incorrectGuesses++;
            Bomb.SetImageResource(Resource.Drawable.bomb1 + incorrectGuesses);
            if (incorrectGuesses>8)
            {
                incorrectGuesses = 8;
                RunOnUiThread(GameOver);
            }
        }

        private void GameOver()
        {
            Toast.MakeText(this, "Game Over", ToastLength.Long);
        }
    }
}