using Cinesimbiose.API.Data;
using Cinesimbiose.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinesimbiose.API.Controllers
{
    [Route("api/[controller]")] // Rota: api/Movies
    [ApiController]
    public class MoviesController : ControllerBase // 💡 POO: Herança
    {
        private readonly CinesimbioseContext _context;

        // 💡 POO: Injeção de Dependência do nosso objeto de contexto
        public MoviesController(CinesimbioseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            // Usando o objeto _context para acessar a coleção de objetos Movie
            return await _context.Movies.ToListAsync();
        }
    }
}