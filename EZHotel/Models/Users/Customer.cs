using EZHotel.DTOs.Rooms;
using EZHotel.DTOs.Users;

namespace EZHotel.Models.Users
{
    public class Customer : User
    {
        public int LoyaltyPoints { get; set; } = 0;
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public RoomType? PreferredRoomType { get; set; } = RoomType.Normal;
        public PaymentMethodType? PaymentMethod { get; set; } = PaymentMethodType.CreditCard;
        public string SpecialRequest { get; set; }
    }
}
