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
    public static class DBQueries
    {
        public static string GetCurrentUser()
        { return ""; }
        public static string SetCurrentUser()//technically an update query
        { return ""; }
        public static string CreateUser()
        { return ""; }
        public static string UpdateUserHighScore()
        { return ""; }
        public static string CreateScore()
        { return ""; }
        public static string GetHighScores()
        { return ""; }
        public static string NukeUsersAndScores()
        { return ""; }
    }
}