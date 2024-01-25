using LondonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LondonAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController(IOptions<HotelInfo> options)
        : ControllerBase
    {
        private readonly HotelInfo _options = options.Value;

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<HotelInfo> GetInfo()
        {
            _options.Href = Url.Link(nameof(GetInfo), null);
            return _options;
        }

    }
}
