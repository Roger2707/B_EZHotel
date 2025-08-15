using EZHotel.DTOs.Users;
using EZHotel.Helpers;
using EZHotel.Infrastructures;
using EZHotel.Models.Users;
using EZHotel.Services.IServices;
namespace EZHotel.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Retrieve

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _uow.User.GetAllAsync<object>(includes: c => c.Role);
            if (users == null || !users.Any()) return new List<UserDTO>();

            var userDTOs = users.Select(u =>
            {
                if (u is Customer customer)
                    return (UserDTO)new CustomerDTO
                    {
                        Id = customer.Id,
                        UserName = customer.UserName,
                        HashPassword = customer.HashPassword,
                        Email = customer.Email,
                        FullName = customer.FullName,
                        PhoneNumber = customer.PhoneNumber,
                        Avatar = customer.Avatar,
                        PublicId = customer.PublicId,
                        Address = customer.Address,
                        Birthday = customer.Birthday,
                        IsActive = customer.IsActive,
                        CreatedAt = customer.CreatedAt,
                        RoleName = customer.Role?.Name,
                        LoyaltyPoints = customer.LoyaltyPoints,
                        Nationality = customer.Nationality,
                        PassportNumber = customer.PassportNumber,
                        PreferredRoomType = customer.PreferredRoomType,
                        PaymentMethod = customer.PaymentMethod,
                        SpecialRequest = customer.SpecialRequest
                    };

                if(u is Staff staff)
                    return (UserDTO)new StaffDTO
                    {
                        Id = staff.Id,
                        UserName = staff.UserName,
                        HashPassword = staff.HashPassword,
                        Email = staff.Email,
                        FullName = staff.FullName,
                        PhoneNumber = staff.PhoneNumber,
                        Avatar = staff.Avatar,
                        PublicId = staff.PublicId,
                        Address = staff.Address,
                        Birthday = staff.Birthday,
                        IsActive = staff.IsActive,
                        CreatedAt = staff.CreatedAt,
                        RoleName = staff.Role?.Name,
                        EmployeeCode = staff.EmployeeCode,
                        Position = staff.Position,
                        HireDate = staff.HireDate,
                        Salary = staff.Salary,
                        BankAccount = staff.BankAccount,
                        EmergencyContact = staff.EmergencyContact,
                        Certifications = staff.Certifications
                    };

                if (u is Manager manager)
                    return (UserDTO)new ManagerDTO
                    {
                        Id = manager.Id,
                        UserName = manager.UserName,
                        HashPassword = manager.HashPassword,
                        Email = manager.Email,
                        FullName = manager.FullName,
                        PhoneNumber = manager.PhoneNumber,
                        Avatar = manager.Avatar,
                        PublicId = manager.PublicId,
                        Address = manager.Address,
                        Birthday = manager.Birthday,
                        IsActive = manager.IsActive,
                        CreatedAt = manager.CreatedAt,
                        RoleName = manager.Role?.Name,
                        EmployeeCode = manager.EmployeeCode,
                        Position = manager.Position,
                        HireDate = manager.HireDate,
                        Salary = manager.Salary,
                        BankAccount = manager.BankAccount,
                        EmergencyContact = manager.EmergencyContact,
                        Certifications = manager.Certifications,
                        ApprovalLimit = manager.ApprovalLimit,
                        PerformanceBonusRate = manager.PerformanceBonusRate,
                        AuthorityLevel = manager.AuthorityLevel
                    };

                return null;
            });

            return userDTOs.ToList();
        }

        #endregion

        #region Register / Login

        public Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
                throw new ArgumentException("Username and password cannot be empty.");

            return _uow.User.AnyAsync(c => c.UserName == loginDTO.UserName && BF.VerifyPassword(loginDTO.Password, c.HashPassword));
        }

        public async Task<bool> RegisterCustomerAsync(RegisterCustomerDTO registerCustomerDTO)
        {
            if (await _uow.User.AnyAsync(c => c.UserName == registerCustomerDTO.UserName))
            {
                throw new InvalidOperationException("Username is existed.");
            }

            var customer = new Customer
            {
                UserName = registerCustomerDTO.UserName,
                HashPassword = BF.HashPassword(registerCustomerDTO.Password),
                Email = registerCustomerDTO.Email,
                FullName = registerCustomerDTO.FullName,
                PhoneNumber = registerCustomerDTO.PhoneNumber,
                Nationality = registerCustomerDTO.Nationality,
                PassportNumber = registerCustomerDTO.PassportNumber,
                PreferredRoomType = registerCustomerDTO.PreferredRoomType,
                PaymentMethod = registerCustomerDTO.PaymentMethod,
                SpecialRequest = registerCustomerDTO.SpecialRequest,
                RoleId = Guid.Parse("ac841e72-8e9a-40a1-8d73-55e77286fffb")
            };

            await _uow.User.AddAsync(customer);
            await _uow.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
