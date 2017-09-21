using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinHangman
{
    public class Users
    {
        [NotNull, PrimaryKey, AutoIncrement, Unique]
        public int UserID { get; set; }
        [NotNull]
        public string Name { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int HighestScore { get; set; }//not in DB, may crash
        public string HighestScoreWord { get; set; }//not in DB
        public int WinRate { get { try { return GamesWon / GamesPlayed * 100; } catch { return 0; }; } }//not in DB
        //public int IsCurrentUser { get { return IsCurrentUser; } set { if (IsCurrentUser == 1) { IsCurrentUser = 0; } else { IsCurrentUser = 1; } } }
    }
    public class Scores
    {
        [NotNull,PrimaryKey,AutoIncrement,Unique]
        public int ScoreID { get; set; }
        [NotNull]
        public int UsersIDFK { get; set; }
        [NotNull]
        public int Score { get; set; }
        [NotNull]
        public int Word { get; set; }
    }
}