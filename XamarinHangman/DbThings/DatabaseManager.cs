using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Environment = Android.OS.Environment;

namespace ToDoList
{
    public class DatabaseManager
    {

        //https://components.xamarin.com/gettingstarted/sqlite-net 
        //https://github.com/praeclarum/sqlite-net
        //https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/

        //https://github.com/praeclarum/sqlite-net/blob/master/src/SQLite.cs


        //YOUR CLASS NAME MUST BE YOUR TABLE NAME

        SQLiteConnection db = new SQLiteConnection(System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "ToDoList.sqlite"));


        public DatabaseManager()
        {

        }

        public List<tblToDoList> ViewAll()
        {
            try
            {
                SQLiteConnection db = new SQLiteConnection(System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "ToDoList.sqlite"));

                return db.Query<tblToDoList>("select * from tblToDoList");

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR Did the DB move across??:" + e.Message);
                return null;
            }
        }

        public void AddItem(string title, string details)
        {
            try
            {
                using (

                    SQLiteConnection db = new SQLiteConnection(
                            Path.Combine(Environment.ExternalStorageDirectory.ToString(),
                                "ToDoList.sqlite")))
                {
                    var AddThis = new tblToDoList() { Title = title, Details = details };
                    db.Insert(AddThis);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Add Error:" + e.Message);
            }
        }

        public void EditItem(string title, string details, int listid)
        {
            try
            {
                //http://stackoverflow.com/questions/14007891/how-are-sqlite-records-updated

                SQLiteConnection db = new SQLiteConnection(System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "ToDoList.sqlite"));
                var EditThis = new tblToDoList() { Title = title, Details = details, ListId = listid };

                db.Update(EditThis);

                //or this

                db.Execute("UPDATE tblToDoList Set Title = ?, Details =, WHERE ID = ?", title, details, listid);

            }
            catch (Exception e)
            {
                Console.WriteLine("Update Error:" + e.Message);
            }
        }

        public void DeleteItem(int listid)
        {
            //https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/
            try
            {
                SQLiteConnection db = new SQLiteConnection(System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "ToDoList.sqlite"));
                db.Delete<tblToDoList>(listid);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error:" + ex.Message);
            }
        }

    }
}

