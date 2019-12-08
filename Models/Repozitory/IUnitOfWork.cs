using Models;

namespace ElectronDecanat.Repozitory
{
    public interface IUnitOfWork
    {
        IRepository<Faculty> Faculties { get; }
        IRepository<Speciality> Specialitys { get; }
        IRepository<Discipline> Disciplines { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<Group> Groups { get; }
        IRepository<Subgroup> Subgroups { get; }
        IRepository<Student> Students { get; }
        IRepository<Lab> Labs { get; }
        IRepository<LabProgress> LabProgress { get; }
        IRepository<Work> Works { get; }
        IRepository<Study> Studying { get; }
        IRepository<DisciplineStudy> DisciplineStudying { get; }
    }
}