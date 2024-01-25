using System.ComponentModel;
using System.Text.Json.Serialization;


namespace LondonAPI.Resources
{
    public class Link
    {
        public const string GETMETHOD = "GET";
        public Link()
        {
            Relations = null;
            Method = string.Empty;
            RouteName = string.Empty;
            RouteValues = null;

        }
        public static Link To(string routeName, object? routeValues = null)
        {
            return new Link()
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GETMETHOD,
                Relations = null,
                Href = null
            };
        }

        public static Link ToCollection(string routeName, object? routeValues = null)
        {
            return new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GETMETHOD,
                Relations = new[] {"collection"},
            };
        }
        //[JsonProperty(Order = -4)]
        //[JsonPropertyOrder(-6)]
        [JsonPropertyOrder(-4)]
        public string? Href { get; set; }

        //[JsonProperty(Order = -3, PropertyName = "rel",
        //    NullValueHandling = NullValueHandling.Ignore)]
        [JsonIgnore]
        [JsonPropertyName("rel")]
        //[JsonPropertyOrder(-3)]

        //[JsonPropertyOrder(-3)]
        public string[]? Relations { get; set; }

        //[DefaultValue(GETMETHOD), JsonProperty(Order = -2,
        //    DefaultValueHandling = DefaultValueHandling.Ignore,
        //    NullValueHandling = NullValueHandling.Ignore)]
        [JsonIgnore]
        [DefaultValue(GETMETHOD)]
        //[JsonPropertyOrder(-2)]
        public string Method { get; set; }
        ////Stores the route name before being rewritten by the LinkRewrittingFilter
        [JsonIgnore]
        public string RouteName { get; set; }
        ////Stores the route parameter before being rewritten by the LinkRewrittingFilter
        [JsonIgnore]   
        public object? RouteValues { get; set; }
    }
}
