using CinemaManager.Models;

namespace CinemaManager.Repositories
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetAvailableSessionsAsync();
        Task<Session> GetSessionWithDetailsAsync(int id);
        Task<bool> CheckScheduleConflictAsync(int sessionId, int roomId, DateTime newScheduledTime, int movieDuration);
    }
}