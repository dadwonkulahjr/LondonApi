namespace LondonAPI.Models
{
    public class RoomEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Rate { get; set; }
    }
}
