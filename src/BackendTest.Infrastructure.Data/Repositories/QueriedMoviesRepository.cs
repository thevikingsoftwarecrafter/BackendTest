using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Domain.Entities;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.Repositories;
using BackendTest.Domain.ValueObjects;
using BackendTest.Infrastructure.Data.DBContext;
using BackendTest.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace BackendTest.Infrastructure.Data.Repositories
{
    public class QueriedMoviesRepository : IQueriedMoviesRepository
    {
        private readonly beezycinemaContext _context;

        public QueriedMoviesRepository(beezycinemaContext context)
        {
            _context = context;
        }
        public async Task<Option<ReadOnlyCollection<QueriedMovie>>> GetAllMovies()
        {
            var movies = await MoviesFromDb();
            return new ReadOnlyCollection<QueriedMovie>(movies.ToList()).SomeNotNull();
        }

        private async Task<IEnumerable<QueriedMovie>> MoviesFromDb()
        {
            var movies = await _context.Movie
                .Include(m => m.Session)
                .ThenInclude(s => s.Room)
                .ToListAsync();

            var genres = await _context.Genre.ToListAsync();
            var moviesgenres = await _context.MovieGenre.ToListAsync();

            return BuildQueriedMovieList(movies, genres, moviesgenres);
        }

        private IEnumerable<QueriedMovie> BuildQueriedMovieList(List<Movie> movies, List<Genre> genres, List<MovieGenre> moviesgenres)
        {
            return movies
                .SelectMany(m => m.Session.Select(s => new { movie = m, seats = s.SeatsSold, size = s.Room?.Size }))
                .GroupBy(m => new { m.movie, m.size }, m => m.seats.Value, (key, g) => new { movieWithSize = key, suma = g.Sum() })
                .Select(movieFull => new QueriedMovie(
                    movieFull.movieWithSize.movie.OriginalTitle,
                    "",
                    genres.FirstOrDefault(g => g.Id == moviesgenres.FirstOrDefault(mg => mg.MovieId == movieFull.movieWithSize.movie.Id)?.GenreId)?.Name,
                    movieFull.movieWithSize.movie.OriginalLanguage,
                    movieFull.movieWithSize.movie.ReleaseDate,
                    "",
                    null,
                    new SeatsSold(movieFull.suma ?? 0),
                    movieFull.movieWithSize.size
                ));
        }

        public Task<Option<ReadOnlyCollection<QueriedMovie>>> GetAllMoviesFromCity()
        {
            throw new System.NotImplementedException();
        }
    }
}