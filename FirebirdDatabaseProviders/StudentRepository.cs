using System;
using System.Collections.Generic;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class StudentRepository : BaseRepository<Student>
    {
        public override Student Get(int id, Func<ITable<Student>, IEnumerable<Student>> predicate = null)
        {
            return base.Get(id, table => table.LoadWith(student =>  student.Subgroup.Group.Speciality.Faculty));
        }
    }
}