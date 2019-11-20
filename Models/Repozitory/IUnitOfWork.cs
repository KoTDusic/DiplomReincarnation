using Models;

namespace ElectronDecanat.Repozitory
{
    public interface IUnitOfWork
    {
        IRepository<Faculty> Faculties { get; }
        IRepository<Speciality> Specialitys { get; }
        IRepository<Discipline> Disciplines { get; }
    }
}