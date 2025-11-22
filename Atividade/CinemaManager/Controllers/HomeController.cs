using CinemaManager.Data;
using CinemaManager.Models;
using CinemaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaManager.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly CinemaDbContext _context;

        public HomeController(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
      
            if (User.IsInRole("Admin"))
            {
                var dataHoje = DateTime.Now.Date;
                var dataAmanha = dataHoje.AddDays(1);

                var ticketsToday = await _context.Tickets
                    .Where(t => t.PurchaseDate >= dataHoje && t.PurchaseDate < dataAmanha)
                    .Include(t => t.Session)
                    .ToListAsync();

                var activeSessions = await _context.Sessions.CountAsync(s => s.ScheduledTime >= DateTime.Now);

                var dashModel = new DashboardViewModel
                {
                    TotalMovies = await _context.Movies.CountAsync(),
                    ActiveSessions = activeSessions,
                    TicketsSoldToday = ticketsToday.Count,
                    SalesToday = ticketsToday.Sum(t => t.Session.Price)
                };

                return View("Dashboard", dashModel); 
            }

            
            // Busca sessões futuras com dados do filme e sala
            var sessoesFuturas = await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .Where(s => s.ScheduledTime > DateTime.Now)
                .OrderBy(s => s.ScheduledTime)
                .ToListAsync();

            return View(sessoesFuturas); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}