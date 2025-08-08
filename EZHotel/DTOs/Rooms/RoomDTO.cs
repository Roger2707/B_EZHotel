namespace EZHotel.DTOs.Rooms
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public bool IsAvailable { get; set; }
        public string UpdatedAt { get; set; }
    }
}
