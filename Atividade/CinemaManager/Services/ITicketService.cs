using CinemaManager.Models;

namespace CinemaManager.Services
{
    public interface ITicketService
    {
        Task<(bool Success, string? Error, Ticket? SoldTicket)> PurchaseTicketAsync(int sessionId, int seatNumber, string userId, string? customerName = null, string? customerCpf = null);
        Task<Ticket> GetTicketByIdAsync(int id);
    }
}