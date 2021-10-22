using System.Threading.Tasks;

namespace Blog.Domain.Contracts.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RollbackTransactionAsync();
        int Commit();
        Task<int> CommitAsync();
    }
}