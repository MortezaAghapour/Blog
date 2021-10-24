using System.Threading;
using System.Threading.Tasks;

namespace Blog.Domain.Contracts.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task BeginTransactionAsync(CancellationToken cancellationToken=default);
        void CommitTransaction();
        Task CommitTransactionAsync(CancellationToken cancellationToken=default);
        void RollbackTransaction();
        Task RollbackTransactionAsync(CancellationToken cancellationToken=default);
        int SaveChange();
        Task<int> SaveChangeAsync(CancellationToken cancellationToken=default);
    }
}