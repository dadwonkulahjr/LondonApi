
using AutoMapper.QueryableExtensions;
using LondonAPI.DataSource;
using LondonAPI.Models;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace LondonAPI.ApiServices
{
    public class RoomServiceImplementation(HotelApiDbContext hotelApiDbContext,
        IConfigurationProvider configurationProvider) : IApiRoomService
    {
        private readonly HotelApiDbContext _hotelApiDbContext = hotelApiDbContext;
        private readonly IConfigurationProvider _mappingConfiguration = configurationProvider;

        public async Task<Room?> GetRoomById(Guid roomId)
        {
            var entity = await _hotelApiDbContext.Rooms.SingleOrDefaultAsync(x => x.Id == roomId);

            if (entity != null)
            {
                var mapper = _mappingConfiguration.CreateMapper();
                return mapper.Map<Room>(entity);
            }

            return null;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var query = _hotelApiDbContext.Rooms
                                    .ProjectTo<Room>(_mappingConfiguration);

            return await query.ToArrayAsync();
        }
    }
}
