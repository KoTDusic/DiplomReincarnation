using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class WorkRepository: BaseRepository<Work>
    {
        public override Work Get(int id, Func<ITable<Work>, IEnumerable<Work>> predicate = null)
        {
            return base.Get(id, table => table
                .LoadWith(work => work.Discipline)
                .LoadWith(work => work.Teacher)
                .LoadWith(work => work.Subgroup.Group.Speciality.Faculty));
        }
    }
}