using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class DisciplineRepository: BaseRepository<Discipline>
    {
        public override Discipline Get(int id, Func<ITable<Discipline>, IEnumerable<Discipline>> predicate = null)
        {
            return base.Get(id, table => table.LoadWith(discipline => discipline.Speciality.Faculty));
        }
    }
}