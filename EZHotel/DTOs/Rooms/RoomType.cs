using System.ComponentModel.DataAnnotations;

namespace EZHotel.DTOs.Rooms
{
    public enum RoomType
    {
        [Display(Name = "Normal")]
        Normal = 0,

        [Display(Name = "VIP")]
        Vip = 1,

        [Display(Name = "Luxury")]
        Luxury = 2
    }
}
