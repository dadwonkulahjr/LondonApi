using LondonAPI.Resources;

namespace LondonAPI.Models
{
    public class RootResponse : Resource
    {
    
        public Link? Info { get; set; }

        public Link? Rooms { get; set; }
    }
}
