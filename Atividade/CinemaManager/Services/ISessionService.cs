using CinemaManager.Models;

namespace CinemaManager.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAvailableSessionsAsync();
        Task<IEnumerable<Session>> GetAllSessionsAsync(); // Para o Admin
        Task<Session> GetSessionByIdAsync(int id);
        Task<Session> GetSessionWithDetailsAsync(int id);
        Task<(bool Success, string? Error)> CreateSessionAsync(Session session);
        Task<(bool Success, string? Error)> UpdateSessionAsync(Session session);
        Task DeleteSessionAsync(int id);
    }
}