using System;
using System.Collections.Generic;
using System.Linq;
using FirebirdDatabaseProviders;
using LinqToDB;
using Models;

namespace ElectronDecanat.Repozitory
{
    public class StudyingRepository : BaseRepository<Study>
    {
        public override List<Study> GetAll(Func<ITable<Study>, IEnumerable<Study>> filter = null)
        {
            if (filter != null)
            {
                throw new NotImplementedException();
            }

            using (var db = new FirebirdDb())
            {
                var table = db.GetTable<Student>()
                    .LoadWith(student => student.Subgroup.Group.Speciality)
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
                        (progress, work) => work.SubGroupId == progress.Student.SubGroupId &&
                                            progress.DisciplineId == work.DisciplineId,
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
                        }).GroupBy(progress => progress.StudentId, progress => progress)
                    .Select(gr => new Study
                    {
                        StudentId = gr.Key,
                        StudentName = gr.First().StudentName,
                        Coors = gr.First().Student.Course,
                        SpecialityId = gr.First().Student.Subgroup.Group.SpecialityId,
                        GroupNumber = gr.First().Student.GroupNumber,
                        SubgroupNumber = gr.First().Student.SubgroupNumber,
                        SubgroupId = gr.First().Student.SubGroupId,
                        Completed = gr.Count(labProgress => labProgress.LabStatus == LabStatus.Complete.ToString()),
                        AllLabs = gr.Count(labProgress => labProgress.StudentId == gr.Key)
                    }).ToList();
                LastQuery = db.LastQuery;
                return table;
            }
        }
    }
}

//
//.LeftJoin(db.GetTable<LabProgress>()
//.LoadWith(student => student.Discipline.Speciality),
//(student, progress) => student.Id == progress.StudentId,
//(student, progress) => new Study
//{
//StudentId = student.Id,
//StudentName = student.StudentName,
//Coors = student.Student.Course,
//SpecialityId = progress.Discipline.SpecialityId,
//GroupNumber = student.Student.GroupNumber,
//SubgroupNumber = student.Student.SubgroupNumber,
//SubgroupId = student.Student.SubGroupId,
//Completed = db.GetTable<LabProgress>()
//.Where(labProgress => labProgress.StudentId== student.Id)
//.Count(labProgress => labProgress.LabStatus== LabStatus.Complete.ToString()),
//NotCompleted = db.GetTable<LabProgress>()
//.Where(labProgress => labProgress.StudentId== student.Id)
//.Count(labProgress => labProgress.LabStatus!= LabStatus.Complete.ToString()),
//AllLabs = db
//    .GetTable<LabProgress>()
//.Count(labProgress => labProgress.StudentId== student.Id)
//})