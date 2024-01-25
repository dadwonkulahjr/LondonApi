namespace LondonAPI.Models
{
    public class Room : Resource
    {
        public required string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
