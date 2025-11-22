using Cinesimbiose.API.Data;
using Cinesimbiose.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinesimbiose.API.Controllers
{
    [Route("api/[controller]")] // Rota: api/Sessions
    [ApiController]
    public class SessionsController : ControllerBase // 💡 POO: Herança
    {
        private readonly CinesimbioseContext _context;

        // 💡 POO: Injeção de Dependência
        public SessionsController(CinesimbioseContext context)
        {
            _context = context;
        }

        [HttpGet("Details")]
        public async Task<ActionResult<IEnumerable<SessionDetails>>> GetScheduledSessions()
        {
            // Usando o objeto _context para acessar a coleção de objetos da View
            var sessions = await _context.SessionDetails
                                        .Where(s => s.SessionStatus == "AGENDADA")
                                        .OrderBy(s => s.StartTime)
                                        .ToListAsync();
            return Ok(sessions);
        }
    }
}