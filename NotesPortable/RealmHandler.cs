using Java.Lang;
using NotesPortable;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotesPortable
{
    public class RealmHandler<T> where T : RealmObject
    {
        Realm mRealm;
        IRealmUpdated IUpdate;

        public RealmHandler(IRealmUpdated IUpdate)
        {
            this.IUpdate = IUpdate;
            mRealm = Realm.GetInstance();
            mRealm.RealmChanged += delegate { IUpdate.Update(); };

        }
        public List<T> GetAll()
        {
            return mRealm.All<T>().ToList();
        }
        public void AddRealmObject(T t)
        {

            new Thread(() =>
            {
                Realm.GetInstance().Write(() =>
              {
                  Realm.GetInstance().Add<T>(t);

              });
            }).Start();
        }
        public void UpdateRealmObject(Action action)
        {
           mRealm.Write(action);

            IUpdate.Update();

    }

        public int GetCount()
        {
            return mRealm.All<T>().Count();
        }



    }
}
