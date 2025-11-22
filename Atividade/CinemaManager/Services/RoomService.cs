using CinemaManager.Models;
using CinemaManager.Repositories;

namespace CinemaManager.Services
{
    public class RoomService : IRoomService
    {
        private readonly IGenericRepository<Room> _roomRepository;

        public RoomService(IGenericRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task CreateRoomAsync(Room room)
        {
            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(Room room)
        {
            _roomRepository.Update(room);
            await _roomRepository.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room != null)
            {
                _roomRepository.Delete(room);
                await _roomRepository.SaveChangesAsync();
            }
        }
    }
}