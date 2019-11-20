using System.Collections.Generic;
using System.Linq;
using FirebirdDatabaseProviders;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseIdModel
    {
        public IEnumerable<T> GetAll()
        {
            using (var db = new FirebirdDb())
            {
                return db.GetTable<T>().ToList();
            }
        }

        public T Get(int id)
        {
            using (var db = new FirebirdDb())
            {
                return db.GetTable<T>().FirstOrDefault(faculty => faculty.Id == id);
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