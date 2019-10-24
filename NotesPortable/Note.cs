using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;

namespace NotesPortable.Models
{
public    class Note : RealmObject
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