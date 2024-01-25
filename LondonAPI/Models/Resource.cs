using LondonAPI.Resources;
using System.Text.Json.Serialization;

namespace LondonAPI.Models
{
    public abstract class Resource : Link
    {
        //[JsonPropertyOrder(-2)]
        [JsonIgnore]
        public Link? Self { get; set; }
    }
}
