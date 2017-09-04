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

namespace XamarinHangman
{
    class User
    {
        public string Name { get; set; }
        public int HighestScore { get; set; }
        public string HighestScoreWord { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int WinRate { get {  try { return GamesWon / GamesPlayed * 100; } catch { return 0; }; }
        }
    }
}