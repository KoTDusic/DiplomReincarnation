using System;
using System.Collections.Generic;
using System.Linq;
using FirebirdDatabaseProviders;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class LabProgressRepository : BaseRepository<LabProgress>
    {
        public override List<LabProgress> GetAll(Func<ITable<LabProgress>, IEnumerable<LabProgress>> filter = null)
        {
            if (filter != null)
            {
                throw new NotImplementedException();
            }

            using (var db = new FirebirdDb())
            {
                var table = db.GetTable<Student>()
                    .InnerJoin(db.GetTable<Lab>(),
                        (student, lab) => student.Subgroup.Group.SpecialityId == lab.Discipline.SpecialityId,
                        (student, lab) => new LabProgress
                        {
                            Student = student,
                            Lab = lab,
                            Discipline = lab.Discipline,
                            LabId = lab.Id,
                            StudentId = student.Id,
                            DisciplineId = lab.DisciplineId
                        }).LeftJoin(db.GetTable<Work>(),
                        (progress, work) => work.SubGroupId == progress.Student.SubGroupId,
                        (progress, work) => new LabProgress
                        {
                            Student = progress.Student,
                            Lab = progress.Lab,
                            Discipline = progress.Discipline,
                            Teacher = work.Teacher,
                            LabId = progress.LabId,
                            StudentId = progress.StudentId,
                            DisciplineId = progress.DisciplineId,
                            TeacherId = work.TeacherId
                        }).LeftJoin(db.GetTable<LabProgress>(), (localProgress, labProgress) =>
                            localProgress.DisciplineId == labProgress.DisciplineId &&
                            localProgress.StudentId == labProgress.StudentId &&
                            localProgress.TeacherId == labProgress.TeacherId &&
                            localProgress.LabId == labProgress.LabId,
                        (localProgress, labProgress) => new LabProgress
                        {
                            Id = labProgress.Id,
                            Student = localProgress.Student,
                            Lab = localProgress.Lab,
                            Discipline = localProgress.Discipline,
                            Teacher = localProgress.Teacher,
                            LabId = localProgress.LabId,
                            StudentId = localProgress.StudentId,
                            DisciplineId = localProgress.DisciplineId,
                            TeacherId = localProgress.TeacherId,
                            LabStatus = labProgress.LabStatus ?? LabStatus.NotComplete.ToString()
                        })
                    .ToList();
                LastQuery = db.LastQuery;
                return table;
            }
        }

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