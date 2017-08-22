using System.Collections.Generic;
using XamarinHangman;
using Android.App;
using Android.Views;
using Android.Widget;

namespace ToDoList {
    public class DataAdapter : BaseAdapter<tblToDoList> {
        private readonly Activity context;

        private readonly List<tblToDoList> items;

        public DataAdapter(Activity context, List<tblToDoList> items) {
            this.context = context;
            this.items = items;
            }

        public override tblToDoList this[int position]
            {
            get { return items[position]; }
            }

        public override int Count
            {
            get { return items.Count; }
            }

        public override long GetItemId(int position) {
            return position;
            }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            var item = items[position];
            var view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);

            view.FindViewById<TextView>(Resource.Id.lbltitle).Text = item.Title;
            view.FindViewById<TextView>(Resource.Id.lbldescription).Text = item.Details;
            return view;
            }
        }
    }