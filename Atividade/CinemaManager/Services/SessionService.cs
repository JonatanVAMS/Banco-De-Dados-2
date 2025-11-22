using CinemaManager.Models;
using CinemaManager.Repositories;

namespace CinemaManager.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IGenericRepository<Room> _roomRepository;
        private readonly IGenericRepository<Movie> _movieRepository;

        public SessionService(ISessionRepository sessionRepository, IGenericRepository<Room> roomRepository, IGenericRepository<Movie> movieRepository)
        {
            _sessionRepository = sessionRepository;
            _roomRepository = roomRepository;
            _movieRepository = movieRepository;
        }

        public async Task<(bool Success, string? Error)> CreateSessionAsync(Session session)
        {
            // Seta assentos disponíveis com base na sala
            var room = await _roomRepository.GetByIdAsync(session.RoomId);
            if (room == null) return (false, "Sala não encontrada."); 
            session.AvailableSeats = room.SeatCount;

            // Verifica conflito de horário
            var movie = await _movieRepository.GetByIdAsync(session.MovieId);
            if (movie == null) return (false, "Filme não encontrado."); 

            bool hasConflict = await _sessionRepository.CheckScheduleConflictAsync(session.Id, session.RoomId, session.ScheduledTime, movie.DurationMinutes);

            if (hasConflict)
            {
                return (false, "Já existe uma sessão nesta sala em horário conflitante."); 
            }

            await _sessionRepository.AddAsync(session);
            await _sessionRepository.SaveChangesAsync();
            return (true, null); 
        }

        public async Task<(bool Success, string? Error)> UpdateSessionAsync(Session session)
        {
            var movie = await _movieRepository.GetByIdAsync(session.MovieId);
            if (movie == null) return (false, "Filme não encontrado."); 

            bool hasConflict = await _sessionRepository.CheckScheduleConflictAsync(session.Id, session.RoomId, session.ScheduledTime, movie.DurationMinutes);

            if (hasConflict)
            {
                return (false, "Horário conflitante com outra sessão nesta sala."); 
            }


            _sessionRepository.Update(session);
            await _sessionRepository.SaveChangesAsync();
            return (true, null);
        }

        public async Task<IEnumerable<Session>> GetAvailableSessionsAsync()
        {
            return await _sessionRepository.GetAvailableSessionsAsync();
        }

        public async Task<Session> GetSessionWithDetailsAsync(int id)
        {
            return await _sessionRepository.GetSessionWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Session>> GetAllSessionsAsync()
        {
    

            return await _sessionRepository.GetAllAsync();
        }
        public async Task<Session> GetSessionByIdAsync(int id)
        {
            return await _sessionRepository.GetByIdAsync(id);
        }

        public async Task DeleteSessionAsync(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session != null)
            {
                _sessionRepository.Delete(session);
                await _sessionRepository.SaveChangesAsync();
            }
        }
    }
}