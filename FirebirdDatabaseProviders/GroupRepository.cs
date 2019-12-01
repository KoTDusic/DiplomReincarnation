using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class GroupRepository : BaseRepository<Group>
    {
        public override Group Get(int id, Func<ITable<Group>, IEnumerable<Group>> predicate = null)
        {
            return base.Get(id, table => table.LoadWith(group => group.Speciality.Faculty));
        }
    }
}