using EZHotel.DTOs.Rooms;
using System.ComponentModel.DataAnnotations;

namespace EZHotel.DTOs.Users
{
    public class RegisterCustomerDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public RoomType PreferredRoomType { get; set; } = RoomType.Normal;
        public PaymentMethodType PaymentMethod { get; set; } = PaymentMethodType.CreditCard;
        public string SpecialRequest { get; set; } = string.Empty;
    }
}
