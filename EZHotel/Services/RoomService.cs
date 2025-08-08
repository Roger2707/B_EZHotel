using EZHotel.DTOs.Rooms;
using EZHotel.Infrastructures;
using EZHotel.Models;
using EZHotel.Services.IServices;

namespace EZHotel.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _uow;

        public RoomService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllAsync()
        {
            var rooms = await _uow.Room.GetAllAsync();
            return rooms.Select(room => new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Price = room.Price,
                Capacity = room.Capacity,
                ImageUrl = room.ImageUrl,
                PublicId = room.PublicId,
                IsAvailable = room.IsAvailable,
                UpdatedAt = room.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        public async Task<RoomDTO> GetByIdAsync(Guid roomId)
        {
            var room = await _uow.Room.FindFirstAsync(x => x.Id == roomId);
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Price = room.Price,
                Capacity = room.Capacity,
                ImageUrl = room.ImageUrl,
                PublicId = room.PublicId,
                IsAvailable = room.IsAvailable,
                UpdatedAt = room.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }

        public async Task<bool> CreateAsync(RoomUpsertDTO roomUpsertDTO)
        {
            await _uow.Room.AddAsync(new Room
            {
                Name = roomUpsertDTO.Name,
                Description = roomUpsertDTO.Description,
                Price = roomUpsertDTO.Price,
                Capacity = roomUpsertDTO.Capacity,
                ImageUrl = roomUpsertDTO.ImageUrl,
                PublicId = roomUpsertDTO.PublicId,
                IsAvailable = true,
                UpdatedAt = DateTime.UtcNow
            });

            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Guid roomId, RoomUpsertDTO roomUpsertDTO)
        {
            var existedRoom = await _uow.Room.FindFirstAsync(x => x.Id == roomId);
            if (existedRoom == null)
            {
                throw new ArgumentException("Room not found");
            }

            existedRoom.Name = roomUpsertDTO.Name;
            existedRoom.Description = roomUpsertDTO.Description;
            existedRoom.Price = roomUpsertDTO.Price;
            existedRoom.Capacity = roomUpsertDTO.Capacity;
            existedRoom.ImageUrl = roomUpsertDTO.ImageUrl;
            existedRoom.PublicId = roomUpsertDTO.PublicId;
            existedRoom.IsAvailable = roomUpsertDTO.IsAvailable;
            existedRoom.UpdatedAt = DateTime.UtcNow;

            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid roomId)
        {
            var existedRoom = await _uow.Room.FindFirstAsync(x => x.Id == roomId);
            if (existedRoom == null)
            {
                throw new ArgumentException("Room not found");
            }
            _uow.Room.DeleteAsync(existedRoom);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
