using EZHotel.DTOs.Rooms;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EZHotel.Helpers
{
    public static class BF
    {
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
    }
}
