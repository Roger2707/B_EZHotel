using EZHotel.DTOs.Rooms;

namespace EZHotel.Services.IServices
{
    public interface IRoomService
    {
        public Task<IEnumerable<RoomDTO>> GetAllAsync();
        public Task<RoomDTO?> GetByIdAsync(Guid roomId);
        public Task<Guid> CreateAsync(RoomUpsertDTO roomUpsertDTO);
        public Task<bool> UpdateAsync(Guid roomId, RoomUpsertDTO roomUpsertDTO);
        public Task<bool> DeleteAsync(Guid roomId);
    }
}
