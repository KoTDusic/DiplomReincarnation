using System;
using System.Collections.Generic;
using LinqToDB;

namespace ElectronDecanat.Repozitory
{
    public interface IRepository<T>
    {
        List<T> GetAll(Func<ITable<T>, IEnumerable<T>> filter = null);
        T Get(int id, Func<ITable<T>,IEnumerable<T>> predicate = null);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
