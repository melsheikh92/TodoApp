using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Realms;
using System.Linq;
using System.Collections.Generic;
using App1.Screens;

namespace App1
{
    [Activity(Label = "#TODO", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Realm realm;
        FloatingActionButton fab;
        ListView listviewNotes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            realm = Realm.GetInstance();
            listviewNotes = FindViewById<ListView>(Resource.Id.listviewNotes);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += Fab_Click;
            realm.RealmChanged += MainActivity_RealmChanged;
            showAll();

        }

        private void MainActivity_RealmChanged(object sender, System.EventArgs e)
        {
            showAll();
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {


            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            EditText input = new EditText(this);
            input.InputType = Android.Text.InputTypes.TextVariationLongMessage;
            input.Hint = "#TODO";
            input.SetHeight(200);
            alert.SetView(input);
            alert.SetTitle("New #TODO");
            alert.SetNegativeButton("Cancel", delegate
            {


            });
            alert.SetPositiveButton("Add", delegate
            {
                new System.Threading.Thread(() =>
                {
                    Realm.GetInstance().Write(() =>
                {

                    Realm.GetInstance().Add<Note>(new Note { Id = Realm.GetInstance().All<Note>().Count() + 1, Text = input.Text, IsDone = false });

                });
                }).Start();
            });
            alert.Show();

        }
        void showAll()
        {
            List<Note> notes = realm.All<Note>().ToList();

            NotesCustomAdapter adapter = new NotesCustomAdapter(this, notes);
            listviewNotes.Adapter = adapter;

        }
    }
}