using EZHotel.Repositories.IRepositories;

namespace EZHotel.Infrastructures
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();

        public IRoomRepository Room { get; }
        public IUserRepository User { get; }
    }
}
