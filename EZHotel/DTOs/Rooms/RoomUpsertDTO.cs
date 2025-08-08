using System.ComponentModel.DataAnnotations;

namespace EZHotel.DTOs.Rooms
{
    public class RoomUpsertDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 5)]
        public int Capacity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public string? PublicId { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }
}
