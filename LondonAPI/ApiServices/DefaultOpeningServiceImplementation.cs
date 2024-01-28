using AutoMapper;
using LondonAPI.DataSource;
using LondonAPI.GenericsT;
using LondonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LondonAPI.ApiServices
{
    public class DefaultOpeningServiceImplementation(
        HotelApiDbContext context,
        IDateLogicService dateLogicService,
        IMapper mapper) : IOpeningService
    {
        private readonly HotelApiDbContext _context = context;
        private readonly IDateLogicService _dateLogicService = dateLogicService;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResults<Opening>> GetOpeningsAsync(PagingOptions pagingOptions)
        {
            var rooms = await _context.Rooms.ToArrayAsync();

            var allOpenings = new List<Opening>();

            foreach (var room in rooms)
            {
                // Generate a sequence of raw opening slots
                var allPossibleOpenings = _dateLogicService.GetAllSlots(
                        DateTimeOffset.UtcNow,
                        _dateLogicService.FurthestPossibleBooking(DateTimeOffset.UtcNow))
                    .ToArray();

                var conflictedSlots = await GetConflictingSlots(
                    room.Id,
                    allPossibleOpenings.First().StartAt.Value,
                    allPossibleOpenings.Last().EndAt.Value);

                // Remove the slots that have conflicts and project
                var openings = allPossibleOpenings
                    .Except(conflictedSlots, new BookingRangeComparer())
                    .Select(slot => new OpeningEntity
                    {
                        RoomId = room.Id,
                        Rate = room.Rate,
                        StartAt = slot.StartAt,
                        EndAt = slot.EndAt
                    })
                    .Select(_mapper.Map<Opening>);

                allOpenings.AddRange(openings);
            }

            var pagedOpenings = allOpenings
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);

            return new PagedResults<Opening>
            {
                Items = pagedOpenings,
                TotalSize = allOpenings.Count
            };
        }

        public async Task<IEnumerable<BookingRange>> GetConflictingSlots(
            Guid roomId,
            DateTimeOffset start,
            DateTimeOffset end) => await _context.Bookings
                .Where(b => b.Room.Id == roomId && _dateLogicService.DoesConflict(b, start, end))
                // Split each existing booking up into a set of atomic slots
                .SelectMany(existing => _dateLogicService
                    .GetAllSlots(existing.StartAt.Value, existing.EndAt))
                .ToArrayAsync();
    }
}
