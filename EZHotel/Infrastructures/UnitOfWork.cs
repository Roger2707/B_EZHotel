using EZHotel.Data;
using EZHotel.Repositories;
using EZHotel.Repositories.IRepositories;

namespace EZHotel.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db, IConfiguration config)
        {
            _db = db;
            Room = new RoomRepository(_db);
            User = new UserRepository(_db);
        }

        public IRoomRepository Room { get; private set; }

        public IUserRepository User { get; private set; }

        public async Task BeginTransactionAsync()
        {
            await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_db.Database.CurrentTransaction != null)
            {
                await _db.Database.CommitTransactionAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_db.Database.CurrentTransaction != null)
            {
                await _db.Database.RollbackTransactionAsync();
            }

        }
        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
