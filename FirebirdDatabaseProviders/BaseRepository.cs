using System;
using System.Collections.Generic;
using System.Linq;
using FirebirdDatabaseProviders;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseIdModel
    {
        public List<T> GetAll(Func<ITable<T>, IEnumerable<T>> filter = null)
        {
            using (var db = new FirebirdDb())
            {
                var table = db.GetTable<T>();
                return filter != null
                    ? filter.Invoke(table).ToList()
                    : table.ToList();
            }
        }

        public virtual T Get(int id, Func<ITable<T>, IEnumerable<T>> predicate = null)
        {
            using (var db = new FirebirdDb())
            {
                var table = db.GetTable<T>();
                return predicate != null
                    ? predicate.Invoke(table).FirstOrDefault(faculty => faculty.Id == id)
                    : db.GetTable<T>().FirstOrDefault(faculty => faculty.Id == id);
            }
        }

        public void Create(T item)
        {
            using (var db = new FirebirdDb())
            {
                db.Insert(item);
            }
        }

        public void Update(T item)
        {
            using (var db = new FirebirdDb())
            {
                db.Update(item);
            }
        }

        public void Delete(T item)
        {
            using (var db = new FirebirdDb())
            {
                db.Delete(item);
            }
        }
    }
}