using CinemaManager.Models;
using CinemaManager.Repositories;
using Microsoft.AspNetCore.Identity; 

namespace CinemaManager.Services
{
    public class TicketService : ITicketService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly UserManager<ApplicationUser> _userManager; 

        public TicketService(
            ISessionRepository sessionRepository,
            IGenericRepository<Ticket> ticketRepository,
            UserManager<ApplicationUser> userManager) 
        {
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
            _userManager = userManager;
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            // Busca o ingresso pelo ID
            var ticket = (await _ticketRepository.FindAsync(i => i.Id == id)).FirstOrDefault();

            if (ticket != null)
            {
               //Carrega a Sessão (com Filme e Sala)
                ticket.Session = await _sessionRepository.GetSessionWithDetailsAsync(ticket.SessionId);

               
                if (!string.IsNullOrEmpty(ticket.ApplicationUserId))
                {
                    ticket.ApplicationUser = await _userManager.FindByIdAsync(ticket.ApplicationUserId);
                }
            }
            return ticket;
        }

        public async Task<(bool Success, string? Error, Ticket? SoldTicket)> PurchaseTicketAsync(
            int sessionId,
            int seatNumber,
            string userId,
            string? customerName = null,
            string? customerCpf = null)
        {
            var session = await _sessionRepository.GetSessionWithDetailsAsync(sessionId);

            if (session == null) return (false, "Sessão não encontrada.", null);
            if (session.ScheduledTime <= DateTime.Now) return (false, "Esta sessão já ocorreu.", null);

            if (session.Tickets.Any(t => t.SeatNumber == seatNumber))
            {
                return (false, $"O assento {seatNumber} já está ocupado.", null);
            }

            if (seatNumber <= 0 || seatNumber > session.Room.SeatCount)
            {
                return (false, "Número de assento inválido para esta sala.", null);
            }

            if (session.AvailableSeats <= 0)
            {
                return (false, "Sessão lotada.", null);
            }

            var ticket = new Ticket
            {
                SessionId = sessionId,
                ApplicationUserId = userId,
                SeatNumber = seatNumber,
                PurchaseDate = DateTime.Now,
                CustomerName = customerName,
                CustomerCPF = customerCpf
            };

            session.AvailableSeats--;

            await _ticketRepository.AddAsync(ticket);
            _sessionRepository.Update(session);

            await _sessionRepository.SaveChangesAsync();

            return (true, "Venda realizada com sucesso!", ticket);
        }
    }
}