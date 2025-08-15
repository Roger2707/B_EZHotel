using EZHotel.Data;
using EZHotel.Models.Users;
using EZHotel.Repositories.IRepositories;

namespace EZHotel.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }
    }
}
