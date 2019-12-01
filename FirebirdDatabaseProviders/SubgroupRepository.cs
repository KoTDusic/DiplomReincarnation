using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class SubgroupRepository : BaseRepository<Subgroup>
    {
        public override Subgroup Get(int id, Func<ITable<Subgroup>, IEnumerable<Subgroup>> predicate = null)
        {
            return base.Get(id, table => table.LoadWith(subgroup =>  subgroup.Group.Speciality.Faculty));
        }
    }
}