using CinemaManager.Models;
using CinemaManager.Repositories;

namespace CinemaManager.Services
{

    public class MovieService : IMovieService
    {
        private readonly IGenericRepository<Movie> _movieRepository;

        public MovieService(IGenericRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            await _movieRepository.AddAsync(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _movieRepository.Update(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie != null)
            {
                _movieRepository.Delete(movie);
                await _movieRepository.SaveChangesAsync();
            }
        }
    }
}