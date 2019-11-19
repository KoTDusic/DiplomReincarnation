using Models;

namespace ElectronDecanat.Repozitory
{
    public interface IUnitOfWork
    {
        IRepository<Faculty> Faculties { get; }
    }
}