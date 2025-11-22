using CinemaManager.Models;
using CinemaManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

      
        public async Task<IActionResult> Index()
        {
            return View(await _roomService.GetAllRoomsAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();


            var room = await _roomService.GetRoomByIdAsync(id.Value);

            if (room == null) return NotFound();
            return View(room);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,SeatCount,Type")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(room);
                TempData["Success"] = "Sala criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // Busca a sala pelo ID
            var room = await _roomService.GetRoomByIdAsync(id.Value);

            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SeatCount,Type")] Room room)
        {
            if (id != room.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _roomService.UpdateRoomAsync(room);
                TempData["Success"] = "Sala atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Busca a sala pelo ID
            var room = await _roomService.GetRoomByIdAsync(id.Value);

            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            TempData["Success"] = "Sala excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}