
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

namespace ToDoList
{
    [Activity(Label = "ToDoList")]
    public class AddItem : Activity
    {
        Button btnAdd;
        EditText txtItemDescription;
        EditText txtItemTitle;
        private tblToDoList myDoList;

        DatabaseManager DB = new DatabaseManager();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.AddItem);

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            txtItemTitle = FindViewById<EditText>(Resource.Id.txtItemTitle);
            txtItemDescription = FindViewById<EditText>(Resource.Id.txtItemDescription);
            //try
            //{
            //    txtItemDescription.Text = myDoList.Details; //FindViewById<EditText> (Resource.Id.txtItemDescription);
            //}
            //catch (Exception)
            //{
            //    txtItemDescription.Text = "No data added";
            //}
            btnAdd.Click += OnBtnAddClick;
        }

        private void OnBtnAddClick(object sender, EventArgs e)
        {
            if (txtItemTitle.Text != "" && txtItemDescription.Text != "")
            {
                DB.AddItem(txtItemTitle.Text, txtItemDescription.Text);
                Toast.MakeText(this, "Note Added", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
        }

    }
}

