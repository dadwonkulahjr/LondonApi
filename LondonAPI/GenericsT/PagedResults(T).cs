namespace LondonAPI.GenericsT
{
    public class PagedResults<T>
    {
        public IEnumerable<T>? Items { get; set; }

        public int TotalSize { get; set; }
    }
}
