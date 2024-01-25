using LondonAPI.Models;

namespace LondonAPI.DataSource
{
    public static class SeedData
    {
        public static async Task InitilizeAsync(IServiceProvider serviceProvider)
        {
          await AddTestData(serviceProvider.GetRequiredService<HotelApiDbContext>());
        }
        public static async Task AddTestData(HotelApiDbContext hotelApiDbContext)
        {
            if (hotelApiDbContext.Rooms.Any())
            {
                return;
            }

            await hotelApiDbContext.Rooms.AddRangeAsync(GetRoomEntities);
            await hotelApiDbContext.SaveChangesAsync();

        }
        private static IEnumerable<RoomEntity> GetRoomEntities
        {
            get
            {
                List<RoomEntity> entities =
            [
                new()
                {
                    Id = Guid.Parse("7baacf57-8a63-4951-b5bf-35d70dc0490e"),
                    Name = "Oxford Suite",
                    Rate = 10119
                },

                new()
                {
                    Id = Guid.Parse("6c2fe0e9-b03b-4846-b98a-c27682cf3229"),
                    Name = "Driscoll Suite",
                    Rate = 23959
                },
            ];
                return entities;
            }
        }


    }
}

