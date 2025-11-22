using CinemaManager.Models;
using CinemaManager.Services;
using CinemaManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMovieService _movieService;
        private readonly IRoomService _roomService;

        public SessionsController(ISessionService sessionService, IMovieService movieService, IRoomService roomService)
        {
            _sessionService = sessionService;
            _movieService = movieService;
            _roomService = roomService;
        }

        // Sessions
        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionService.GetAvailableSessionsAsync();
            return View(sessions);
        }

        // Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

  
            var session = await _sessionService.GetSessionWithDetailsAsync(id.Value);

            if (session == null) return NotFound();
            return View(session);
        }

        // Sessions/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new SessionViewModel
            {
                ScheduledTime = DateTime.Now.Date.AddHours(18),
                MoviesList = new SelectList(await _movieService.GetAllMoviesAsync(), "Id", "Title"),
                RoomsList = new SelectList(await _roomService.GetAllRoomsAsync(), "Id", "Name")
            };
            return View(viewModel);
        }

        // Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var session = new Session
                {
                    MovieId = viewModel.MovieId,
                    RoomId = viewModel.RoomId,
                    ScheduledTime = viewModel.ScheduledTime,
                    Price = viewModel.Price
                };

                var (success, error) = await _sessionService.CreateSessionAsync(session);

                if (success)
                {
                    TempData["Success"] = "Sessão criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error ?? "Ocorreu um erro ao criar a sessão.");
                }
            }

            // Se falhar, recarrega as listas
            viewModel.MoviesList = new SelectList(await _movieService.GetAllMoviesAsync(), "Id", "Title", viewModel.MovieId);
            viewModel.RoomsList = new SelectList(await _roomService.GetAllRoomsAsync(), "Id", "Name", viewModel.RoomId);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // Garante que Movie e Room sejam carregados
            var session = await _sessionService.GetSessionWithDetailsAsync(id.Value);

            if (session == null) return NotFound();

            // Mapear dados para o ViewModel de Edição
            var viewModel = new SessionViewModel
            {
                Id = session.Id,
                ScheduledTime = session.ScheduledTime,
                Price = session.Price,
                MovieId = session.MovieId,
                RoomId = session.RoomId,
                MoviesList = new SelectList(await _movieService.GetAllMoviesAsync(), "Id", "Title", session.MovieId),
                RoomsList = new SelectList(await _roomService.GetAllRoomsAsync(), "Id", "Name", session.RoomId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingSession = await _sessionService.GetSessionByIdAsync(viewModel.Id);
                if (existingSession == null) return NotFound();

                existingSession.MovieId = viewModel.MovieId;
                existingSession.RoomId = viewModel.RoomId;
                existingSession.ScheduledTime = viewModel.ScheduledTime;
                existingSession.Price = viewModel.Price;

                var (success, error) = await _sessionService.UpdateSessionAsync(existingSession);

                if (success)
                {
                    TempData["Success"] = "Sessão atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error ?? "Ocorreu um erro ao atualizar a sessão.");
                }
            }

            // Se falhar, recarregar as listas
            viewModel.MoviesList = new SelectList(await _movieService.GetAllMoviesAsync(), "Id", "Title", viewModel.MovieId);
            viewModel.RoomsList = new SelectList(await _roomService.GetAllRoomsAsync(), "Id", "Name", viewModel.RoomId);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            //Garante que Movie e Room sejam carregados para a View de Confirmação
            var session = await _sessionService.GetSessionWithDetailsAsync(id.Value);

            if (session == null) return NotFound();
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sessionService.DeleteSessionAsync(id);
            TempData["Success"] = "Sessão excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}