using EZHotel.DTOs.Users;

namespace EZHotel.Services.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<bool> RegisterCustomerAsync(RegisterCustomerDTO registerCustomerDTO);
        public Task<bool> LoginAsync(LoginDTO loginDTO);
    }
}
