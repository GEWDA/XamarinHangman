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
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
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
            for (int i = 0; i < 28; i++)
            {
                try
                {
                    AllLetters[i] = FindViewById<Button>(Resource.Id.button1+i);//all buttons were created at the same time, so their ids are adjacent ints
                    AllLetters[i].Typeface=spywareFont;
                }
                catch
                {
                    Log.Warn("myDebug", "ID " + (Resource.Id.button1 + i).ToString() + " is not a Android.Widget.Button, it is a "+FindViewById(Resource.Id.button1 + i).GetType());
                }
            }
        }
    }
}