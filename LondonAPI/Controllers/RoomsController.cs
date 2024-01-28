using AutoMapper;
using LondonAPI.ApiServices;
using LondonAPI.DataSource;
using LondonAPI.GenericsT;
using LondonAPI.Models;
using LondonAPI.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LondonAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RoomsController(IApiRoomService apiRoomService, IMapper mapper,
        IOpeningService openingService,
        IOptions<PagingOptions> defaultPagingOptionsWrapper) : ControllerBase
    {
        private readonly IApiRoomService _apiRoomService = apiRoomService;
        private readonly IMapper _mapper = mapper;
        private readonly IOpeningService _openingService = openingService;
       private readonly PagingOptions _defaultPagingOptions = defaultPagingOptionsWrapper.Value;

        [HttpGet(Name = nameof(GetAllRooms))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Collection<Room>>> GetAllRooms()
        {
            var rooms = await _apiRoomService.GetRoomsAsync();

            var collection = new Collection<Room>
            {
                Self = Link.ToCollection(nameof(GetAllRooms)),
                Value = rooms.ToArray()
            };

            return collection;
        }

        [HttpGet(template:"{roomId}", Name = nameof(GetRoomById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
        {
            var entity = await _apiRoomService.GetRoomById(roomId);
            
            if(entity != null)
            {
               return _mapper.Map<Room>(entity);
                //resource.Href = Url.Link(nameof(GetRoomById), new { roomId });
                //return resource;
            }

            return NotFound();
        }

        [HttpGet("openings", Name = nameof(GetAllRoomOpenings))]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings(
           [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var openings = await _openingService.GetOpeningsAsync(pagingOptions);

            var collection = new PagedCollection<Opening>()
            {
                Self = Link.ToCollection(nameof(GetAllRoomOpenings)),
                Value = openings.Items.ToArray(),
                Size = openings.TotalSize,
                Offset = pagingOptions.Offset.Value,
                Limit = pagingOptions.Limit.Value
            };

            return collection;
        }
    }
}
