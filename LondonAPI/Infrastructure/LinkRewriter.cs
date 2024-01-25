using LondonAPI.Resources;
using Microsoft.AspNetCore.Mvc;

namespace LondonAPI.Infrastructure
{
    public class LinkRewriter(IUrlHelper urlHelper)
    {
        private readonly IUrlHelper _urlHelper = urlHelper;

        public Link Rewrite(Link original)
        {
            if (original == null) return null;
            return new Link
            {
                Href = _urlHelper.Link(original.RouteName, original.RouteValues),
                Method = original.Method,
                Relations = original.Relations,

            };


        }
    }
}
