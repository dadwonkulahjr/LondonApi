using LondonAPI.Models;

namespace LondonAPI.ApiServices
{
    public interface IApiRoomService
    {
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room?> GetRoomById(Guid roomId);
    }
}
