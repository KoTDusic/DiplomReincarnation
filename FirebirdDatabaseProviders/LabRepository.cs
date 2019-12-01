using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class LabRepository : BaseRepository<Lab>
    {
        public override Lab Get(int id, Func<ITable<Lab>, IEnumerable<Lab>> predicate = null)
        {
            return base.Get(id, table => table.LoadWith(lab => lab.Discipline.Speciality.Faculty));
        }
    }
}