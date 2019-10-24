using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;

using App1.Screens;
using GalaSoft.MvvmLight.Views;
using NotesPortable.Models;
using Android.Views;
using System;

namespace App1
{
    [Activity(Label = "#TODO", MainLauncher = true)]
    public class MainActivity : ActivityBase
    {


        MainViewModel mainViewModel;

        FloatingActionButton fab;
        ListView listviewNotes;
        EditText input; //for the alert
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            listviewNotes = FindViewById<ListView>(Resource.Id.listviewNotes);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += Fab_Click;
            input = new EditText(this);
            mainViewModel = new MainViewModel();
            listviewNotes.Adapter = MissingExtensions.GetAdapter(mainViewModel.ObservavleNotes, GetTaskAdapter);
            
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();


            listviewNotes.Adapter = null;
        }
      
        
        private View GetTaskAdapter(int position,
                                           Note note,
                                           View convertView)
        {
            convertView = LayoutInflater.Inflate(Resource.Layout.listitemNote, null);
            TextView tvNote = convertView.FindViewById<TextView>(Resource.Id.tvNote);
            CheckBox checkbox = convertView.FindViewById<CheckBox>(Resource.Id.checIsDone);
            tvNote.Text = note.Text;
            checkbox.Checked = note.IsDone;
            checkbox.CheckedChange += mainViewModel.ItemChangeCheck(note, checkbox.Checked);
            tvNote.Click += updateNote(note);

            return convertView;
        }

        private EventHandler updateNote(Note note)
        {
            return delegate
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                input.InputType = Android.Text.InputTypes.TextVariationLongMessage;
                input.Text = note.Text;
                input.Hint = "#TODO";
                input.SetHeight(200);
                alert.SetView(input);
                alert.SetTitle("New #TODO");
                alert.SetNegativeButton("Cancel", delegate { });
                alert.SetPositiveButton("Edit", mainViewModel.EditHandler(note, input));
                alert.Show();
                
            };


        }

        private void Fab_Click(object sender, System.EventArgs e)
        {

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            input.InputType = Android.Text.InputTypes.TextVariationLongMessage;
            input.Hint = "#TODO";
            input.SetHeight(200);
            input.Text = "";
            alert.SetView(input);
            alert.SetTitle("New #TODO");
            alert.SetNegativeButton("Cancel", delegate { });
            alert.SetPositiveButton("Add", mainViewModel.NewTodoHandler(input.Text));


            alert.Show();


        }


    }


}