using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;
using Android.Graphics;
using NotesPortable.Models;

namespace App1.Screens
{
    class NotesCustomAdapter : ArrayAdapter
    {

        private Activity mcontext;
        List<Note> notes;
        Realm realm;
        public NotesCustomAdapter(Activity mcontext, List<Note> notes ) : base(mcontext, Resource.Layout.listitemNote)
        {
            this.mcontext = mcontext;
            this.notes = notes;
            realm = Realm.GetInstance();

        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = mcontext.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            View view = inflater.Inflate(Resource.Layout.listitemNote, parent, false);
            TextView tvNote = view.FindViewById<TextView>(Resource.Id.tvNote);
            CheckBox checkbox = view.FindViewById<CheckBox>(Resource.Id.checIsDone);
            tvNote.Text = notes[position].Text;
            checkbox.Checked = notes[position].IsDone;

            if (checkbox.Checked)
            {
                tvNote.PaintFlags = tvNote.PaintFlags | PaintFlags.StrikeThruText;
            }
            tvNote.Text = notes[position].Text;
            checkbox.Checked = notes[position].IsDone;
            checkbox.CheckedChange += delegate
            {
                
                realm.Write(() => notes[position].IsDone = checkbox.Checked);


            };
            tvNote.Click += delegate
            {

                AlertDialog.Builder alert = new AlertDialog.Builder(mcontext);
                EditText input = new EditText(mcontext);
                input.InputType = Android.Text.InputTypes.TextVariationLongMessage;
                input.Text = notes[position].Text;
                input.Hint = "#TODO";
                input.SetHeight(200);
                alert.SetView(input);
                alert.SetTitle("Edit #TODO");
                alert.SetNegativeButton("Cancel", delegate
                {


                });
                alert.SetPositiveButton("Edit", delegate
                {

                    realm.Write(() => notes[position].Text = input.Text);



                });
                alert.Show();
            };
            return view;
        }



        public override int Count
        {
            get
            {
                return notes.Count;
            }
        }

    }
}