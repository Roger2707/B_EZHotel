using EZHotel.Data;
using EZHotel.Models;
using EZHotel.Repositories.IRepositories;

namespace EZHotel.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext db) : base(db)
        {
        }
    }
}
