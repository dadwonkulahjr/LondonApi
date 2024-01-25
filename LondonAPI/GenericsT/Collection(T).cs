using LondonAPI.Models;

namespace LondonAPI.GenericsT
{
    public class Collection<T> : Resource
    {
        public T[]? Value { get; set; }
    }
}
