using System;
using System.IO;
using System.Collections.Generic;
using XamarinHangman;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace ToDoList
{
    [Activity(Label = "ToDoList", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : Activity
    {
        //we need this so we can copy the DB across to the phone or emulator......
        static string dbName = "ToDoList.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        //tblToDoList myDoList = new tblToDoList();
        //ListView lstToDoList;
        List<tblToDoList> myList;

        //private string tag = "dbstuff";
        DatabaseManager myDbManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //lstToDoList = FindViewById<ListView>(Resource.Id.listView1);
            CopyTheDB();

            myDbManager = new DatabaseManager();
            myList = myDbManager.ViewAll();
            //lstToDoList.Adapter = new DataAdapter(this, myList);
            //lstToDoList.ItemClick += OnLstToDoListClick;


        }


        private void CopyTheDB()
        {
            // Check if your DB has already been extracted. If the file does not exist move it
            if (!File.Exists(dbPath))
            {

                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }

        }



        //Adds Add to the Menu in the top right of your screen.
        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    menu.Add("Add");
        //    return base.OnPrepareOptionsMenu(menu);
        //}

        //void OnLstToDoListClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    var ToDoItem = myList[e.Position];
        //    var edititem = new Intent(this, typeof(EditItem));

        //    edititem.PutExtra("Title", ToDoItem.Title);
        //    edititem.PutExtra("Details", ToDoItem.Details);
        //    edititem.PutExtra("ListID", ToDoItem.ListId);

        //    StartActivity(edititem);
        //}


        //When you choose Add from the Menu run the Add Activity
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    var itemTitle = item.TitleFormatted.ToString();

        //    switch (itemTitle)
        //    {
        //        case "Add":
        //            StartActivity(typeof(AddItem));
        //            break;
        //    }
        //    return base.OnOptionsItemSelected(item);
        //}

        //Basically reload stuff when the app resumes operation after being pauused
        protected override void OnResume()
        {
            base.OnResume();
            myDbManager = new DatabaseManager();
            myList = myDbManager.ViewAll();
            //lstToDoList.Adapter = new DataAdapter(this, myList);
        }

    }
}


