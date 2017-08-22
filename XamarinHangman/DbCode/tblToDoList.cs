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

namespace ToDoList {

    public class tblToDoList {

        [PrimaryKey, AutoIncrement]  //These are attributes that define the property below it
        public int ListId { get; set; }

        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }

        public tblToDoList() {
            }
        }
    }