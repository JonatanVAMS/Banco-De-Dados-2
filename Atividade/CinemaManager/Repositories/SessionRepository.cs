using CinemaManager.Data;
using CinemaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(CinemaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Session>> GetAvailableSessionsAsync()
        {
            return await _dbSet
                .Include(s => s.Movie) //dados do Movie
                .Include(s => s.Room)  //dados da Room
                .Where(s => s.ScheduledTime > DateTime.Now) 
                .OrderBy(s => s.Movie.Title)
                .ThenBy(s => s.ScheduledTime)
                .ToListAsync();
        }

        public async Task<Session> GetSessionWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .Include(s => s.Tickets) // 
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> CheckScheduleConflictAsync(int sessionId, int roomId, DateTime newScheduledTime, int movieDuration)
        {
            var newSessionEnd = newScheduledTime.AddMinutes(movieDuration);

            // Busca sessões na mesma sala, que não sejam a própria sessão (em caso de edição)
            var conflictingSessions = await _dbSet
                .Where(s => s.RoomId == roomId && s.Id != sessionId)
                .Include(s => s.Movie) // Essa é a condição que precisa do filme para saber a duração
                .ToListAsync();

            foreach (var existingSession in conflictingSessions)
            {
                var existingEnd = existingSession.ScheduledTime.AddMinutes(existingSession.Movie.DurationMinutes);

                // Lógica de sobreposição de tempo
                if (newScheduledTime < existingEnd && newSessionEnd > existingSession.ScheduledTime)
                {
                    return true; 
                }
            }
            return false; 
        }
    }
}