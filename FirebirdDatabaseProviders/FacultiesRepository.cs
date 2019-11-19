using System.Collections.Generic;
using System.Linq;
using FirebirdDatabaseProviders;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class FacultiesRepository : IRepository<Faculty>
    {
        public IEnumerable<Faculty> GetAll()
        {
            using (var db = new FirebirdDb())
            {
                return db.Faculties.ToList();
            }
        }

        public Faculty Get(int id)
        {
            using (var db = new FirebirdDb())
            {
                return db.Faculties.FirstOrDefault(faculty => faculty.Id == id);
            }
        }

        public void Create(Faculty item)
        {
            using (var db = new FirebirdDb())
            {
                db.Insert(item);
            }
        }

        public void Update(Faculty item)
        {
            using (var db = new FirebirdDb())
            {
                db.Update(item);
            }
        }

        public void Delete(Faculty item)
        {
            using (var db = new FirebirdDb())
            {
                db.Delete(item);
            }
        }
    }
}