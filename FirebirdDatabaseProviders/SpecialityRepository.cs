using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class SpecialityRepository : BaseRepository<Speciality>
    {
        public override Speciality Get(int id, Func<ITable<Speciality>, IEnumerable<Speciality>> predicate = null)
        {
            return base.Get(id, table => table.LoadWith(speciality => speciality.Faculty));
        }
    }
}