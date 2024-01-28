using LondonAPI.Resources;
using System.Text.Json.Serialization;

namespace LondonAPI.GenericsT
{
    public class PagedCollection<T> : Collection<T>
    {
        [JsonIgnore]
        public int? Offset { get; set; }
        [JsonIgnore]
        public int? Limit { get; set; }
        [JsonIgnore]
        public int? Size { get; set; }
        [JsonIgnore]
        public Link? First { get; set; }
        [JsonIgnore]
        public Link? Previous { get; set; }
        [JsonIgnore]
        public Link? Next { get; set; }
        [JsonIgnore]
        public Link? Last { get; set; }
    }
}
