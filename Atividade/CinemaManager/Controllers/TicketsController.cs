using CinemaManager.Data;
using CinemaManager.Models;
using CinemaManager.Services;
using CinemaManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CinemaManager.Controllers
{
    [Authorize(Roles = "Admin,Atendente,Cliente")]
    public class TicketsController : Controller
    {
        // Campos Readonly
        private readonly ITicketService _ticketService;
        private readonly ISessionService _sessionService;
        private readonly CinemaDbContext _context; // Adicionado
        private readonly UserManager<ApplicationUser> _userManager; // Adicionado

        // Construtor (Tem que estar DENTRO da classe)
        public TicketsController(
            ITicketService ticketService,
            ISessionService sessionService,
            CinemaDbContext context,
            UserManager<ApplicationUser> userManager) // Injeção do UserManager
        {
            _ticketService = ticketService;
            _sessionService = sessionService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionService.GetAvailableSessionsAsync();
            return View(sessions);
        }

        [HttpGet]
        public async Task<IActionResult> Purchase(int? id)
        {
            if (id == null) return NotFound();
            var session = await _sessionService.GetSessionWithDetailsAsync(id.Value);
            if (session == null) return NotFound();

            if (session.ScheduledTime <= DateTime.Now)
            {
                TempData["Error"] = "Esta sessão já ocorreu.";
                return RedirectToAction(nameof(Index));
            }

            var occupiedSeats = session.Tickets.Select(t => t.SeatNumber).ToList();

            var viewModel = new PurchaseViewModel
            {
                Session = session,
                OccupiedSeats = occupiedSeats
            };

            return View(viewModel);
        }

        //Tickets/Purchase 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase(int sessionId, int seatNumber, PurchaseViewModel viewModel)
        {
            if (seatNumber <= 0)
            {
                TempData["Error"] = "Você deve selecionar um assento.";
                return RedirectToAction(nameof(Purchase), new { id = sessionId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                // Usuário não logado, redirecionar para login
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

         
            var (success, error, soldTicket) = await _ticketService.PurchaseTicketAsync(
                sessionId,
                seatNumber,
                userId,
                viewModel.CustomerName,
                viewModel.CustomerCPF
            );

            if (success && soldTicket != null)
            {
                TempData["Success"] = "Operação realizada com sucesso!";
                return RedirectToAction(nameof(Receipt), new { id = soldTicket.Id });
            }
            else
            {
                TempData["Error"] = error ?? "Ocorreu um erro.";
                // Recarregar o ViewModel completo para a tela de erro
                var session = await _sessionService.GetSessionWithDetailsAsync(sessionId);
                viewModel.Session = session;
                viewModel.OccupiedSeats = session.Tickets.Select(t => t.SeatNumber).ToList();

                return View(nameof(Purchase), viewModel); // Retorna a view com os dados atualizados
            }
        }

        // Tickets/Receipt/10 (Comprovante)
        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _ticketService.GetTicketByIdAsync(id.Value);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // Tickets/MyTickets (Histórico do Cliente)
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> MyTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var meusIngressos = await _context.Tickets
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .Include(t => t.Session)
                .ThenInclude(s => s.Room)
                .Include(t => t.ApplicationUser) // Carregar o usuário para o nome
                .Where(t => t.ApplicationUserId == userId)
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();

            return View(meusIngressos);
        }

        // Tickets Relatório de Vendas para Admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminReport()
        {
            var vendas = await _context.Tickets
                .Include(t => t.Session).ThenInclude(s => s.Movie)
                .Include(t => t.Session).ThenInclude(s => s.Room)
                .Include(t => t.ApplicationUser)
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();

            return View(vendas);
        }
    }
}