using LondonAPI.GenericsT;
using LondonAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LondonAPI.DataSource
{
    public static class SeedData
    {
        public static async Task InitilizeAsync(IServiceProvider serviceProvider)
        {
          await AddTestData(serviceProvider.GetRequiredService<HotelApiDbContext>(),
                serviceProvider.GetRequiredService<IDateLogicService>());
        }
        public static async Task AddTestData(HotelApiDbContext hotelApiDbContext,
            IDateLogicService dateLogicService)
        {
            if (hotelApiDbContext.Rooms.Any())
            {
                return;
            }
            //await hotelApiDbContext.Rooms.AddRangeAsync(GetRoomEntities);

            //var today = DateTimeOffset.Now;
            //var start = dateLogicService.AlignStartTime(today);
            //var end = start.Add(dateLogicService.GetMinimumStay());


            //await hotelApiDbContext.SaveChangesAsync();

            var oxford = hotelApiDbContext.Rooms.Add(new RoomEntity
            {
                Id = Guid.Parse("7baacf57-8a63-4951-b5bf-35d70dc0490e"),
                Name = "Oxford Suite",
                Rate = 10119,
            }).Entity;

            hotelApiDbContext.Rooms.Add(new RoomEntity
            {
                Id = Guid.Parse("6c2fe0e9-b03b-4846-b98a-c27682cf3229"),
                Name = "Driscoll Suite",
                Rate = 23959
            });

            var today = DateTimeOffset.Now;
            var start = dateLogicService.AlignStartTime(today);
            var end = start.Add(dateLogicService.GetMinimumStay());

            hotelApiDbContext.Bookings.Add(new BookingEntity
            {
                Id = Guid.Parse("2eac8dea-2749-42b3-9d21-8eb2fc0fd6bd"),
                Room = oxford,
                CreatedAt = DateTimeOffset.UtcNow,
                StartAt = start,
                EndAt = end,
                Total = oxford.Rate,
            });

            await hotelApiDbContext.SaveChangesAsync();

        }
        //private static IEnumerable<RoomEntity> GetRoomEntities
        //{
        //    get
        //    {
        //        List<RoomEntity> entities =
        //        [
        //        new()
        //        {
        //            Id = Guid.Parse("7baacf57-8a63-4951-b5bf-35d70dc0490e"),
        //            Name = "Oxford Suite",
        //            Rate = 10119
        //        },

        //        new()
        //        {
        //            Id = Guid.Parse("6c2fe0e9-b03b-4846-b98a-c27682cf3229"),
        //            Name = "Driscoll Suite",
        //            Rate = 23959
        //        },
        //       ];

        //        return entities;
        //    }
        //}


    }
}

