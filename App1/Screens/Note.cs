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
using Realms;

namespace App1.Screens
{
    class Note : RealmObject
    {

        [PrimaryKey]
        public int Id
        { get; set; }
        public string Text
        { get; set; }
        public bool IsDone
        { get; set; }

    }
}