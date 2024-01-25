namespace LondonAPI.Models
{
    public class HotelInfo : Resource
    {
        public required string Title { get; set; }
        public required string Tagline { get; set; }
        public required string Email { get; set; }

        public required string Website { get; set; }
        public required Address Location { get; set; }
    }
}
