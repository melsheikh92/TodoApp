using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;
using NotesPortable.Models;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.ObjectModel;
using NotesPortable;

namespace App1.Screens
{
    public class MainViewModel : IRealmUpdated
    {
        List<Note> notes = new List<Note>();
        public ObservableCollection<Note> ObservavleNotes { get; set; }
        RealmHandler<Note> realmHandler;
        public MainViewModel()
        {
            realmHandler = new RealmHandler<Note>(this);
            ObservavleNotes = new ObservableCollection<Note>(notes);
            notes = realmHandler.GetAll();
            foreach (Note note in notes)
            {
                ObservavleNotes.Add(note);
            }
        }


        internal EventHandler<CompoundButton.CheckedChangeEventArgs> ItemChangeCheck(Note note, bool @checked)
        {
            return delegate { realmHandler.UpdateRealmObject(() => note.IsDone = !@checked); };
        }

        internal EventHandler<DialogClickEventArgs> NewTodoHandler(string text)
        {
            return delegate { realmHandler.AddRealmObject(new Note { Text = text, Id = realmHandler.GetCount() + 1, IsDone = false }); };
        }

        internal EventHandler<DialogClickEventArgs> EditHandler(Note note, EditText et)
        {
            return delegate { realmHandler.UpdateRealmObject(() => note.Text = et.Text); };
        }


        public void Update()
        {
            notes = realmHandler.GetAll();
            foreach (Note note in notes)
            {
                ObservavleNotes.Add(note);
            }

        }
    }

    public static class MissingExtensions
    {
        public static ObservableAdapter<Note> GetAdapter<Note>(
        this ObservableCollection<Note> collection,
        Func<int, Note, View, View> getTemplateDelegate)
        {
            return new ObservableAdapter<Note>
            {
                DataSource = collection,
                GetTemplateDelegate = getTemplateDelegate
            };
        }
    }

}