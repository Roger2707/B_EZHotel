using EZHotel.DTOs.Rooms;

namespace EZHotel.DTOs.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string PublicId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; }
    }

    public class StaffDTO : UserDTO
    {
        public string EmployeeCode { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string BankAccount { get; set; }
        public string EmergencyContact { get; set; }
        public string Certifications { get; set; }
    }

    public class ManagerDTO : StaffDTO
    {
        public decimal ApprovalLimit { get; set; }
        public decimal PerformanceBonusRate { get; set; }
        public int AuthorityLevel { get; set; }
    }

    public class CustomerDTO : UserDTO
    {
        public int LoyaltyPoints { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public RoomType? PreferredRoomType { get; set; }
        public PaymentMethodType? PaymentMethod { get; set; }
        public string SpecialRequest { get; set; }
    }
}
