using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using RssFeedApi.Annotations;
using RssFeedApi.Models.Entities;


namespace RssFeedApi.DbController
{
    public class DbController : INotifyPropertyChanged
    {
       // private RssFeedWorkerV1_dbEntities db = new RssFeedWorkerV1_dbEntities();


        public RssFeedWorkerV1_dbEntities _db = new RssFeedWorkerV1_dbEntities();
        public RssFeedWorkerV1_dbEntities db
        {
            get
            {
                return _db;
            }
            set
            {
                if (_db == null)
                {
                    _db = value;
                    OnPropertyChanged("db");
                }
              
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}