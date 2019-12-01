using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class LabProgressRepository : BaseRepository<LabProgress>
    {
        public override LabProgress Get(int id, Func<ITable<LabProgress>, IEnumerable<LabProgress>> predicate = null)
        {
            return base.Get(id, table => table
                .LoadWith(labProgress => labProgress.Discipline.Speciality.Faculty)
                .LoadWith(labProgress => labProgress.Teacher)
                .LoadWith(labProgress => labProgress.Student)
                .LoadWith(labProgress => labProgress.Lab));
        }
    }
}