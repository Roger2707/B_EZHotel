using EZHotel.DTOs.Rooms;
using EZHotel.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EZHotel.Helpers
{
    public static class BF
    {
        public static List<Role> roles = new List<Role>();
        public static string GetDisplayNameEnum(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }

        public static double GetRateRoom(RoomType roomType)
        {
            return roomType switch
            {
                RoomType.Normal => 1,
                RoomType.Vip => 1.5,
                RoomType.Luxury => 1.8,
                _ => 0
            };
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
