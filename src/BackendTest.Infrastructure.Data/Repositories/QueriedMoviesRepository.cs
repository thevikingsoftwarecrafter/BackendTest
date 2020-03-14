using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendTest.Domain.Entities;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.Repositories;
using BackendTest.Domain.ValueObjects;
using BackendTest.Infrastructure.Data.DBContext;
using BackendTest.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Optional;
using RestSharp;

namespace BackendTest.Infrastructure.Data.Repositories
{
    public class QueriedMoviesRepository : IQueriedMoviesRepository
    {
        private readonly beezycinemaContext _context;
        private readonly IConfiguration _configuration;

        public QueriedMoviesRepository(beezycinemaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Option<ReadOnlyCollection<QueriedMovie>>> GetAllMoviesFromCity()
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
                    new SeatsSold(movieFull.suma ?? 1),
                    movieFull.movieWithSize.size
                ));
        }

        public async Task<Option<ReadOnlyCollection<QueriedMovie>>> GetAllMovies()
        {
            var client = new RestClient("https://api.themoviedb.org");
            var request = new RestRequest("3/discover/movie");
            request.Parameters.Add(new Parameter("api_key", _configuration.GetValue(typeof(string), "TheMovieDbApiKey") , ParameterType.QueryString));
            request.Parameters.Add(new Parameter("sort_by", "popularity.desc", ParameterType.QueryString));
            request.Parameters.Add(new Parameter("include_video", "false", ParameterType.QueryString));
            request.Parameters.Add(new Parameter("include_adult", "false", ParameterType.QueryString));
            request.Parameters.Add(new Parameter("page", "1", ParameterType.QueryString));

            var apiQueriedMovies = new List<QueriedMovie>();

            var movies = await client.ExecuteAsync<MoviesFromExternalApi>(request);
            apiQueriedMovies.AddRange(GetApiQueriedMovies(movies?.Data, QueriedMovie.BigSize));

            request.Parameters.First(p => p.Name == "page").Value = "2";
            movies = await client.ExecuteAsync<MoviesFromExternalApi>(request);
            apiQueriedMovies.AddRange(GetApiQueriedMovies(movies?.Data, QueriedMovie.BigSize));

            request.Parameters.First(p => p.Name == "page").Value = "3";
            movies = await client.ExecuteAsync<MoviesFromExternalApi>(request);
            apiQueriedMovies.AddRange(GetApiQueriedMovies(movies?.Data, QueriedMovie.SmallSize));

            request.Parameters.First(p => p.Name == "page").Value = "4";
            movies = await client.ExecuteAsync<MoviesFromExternalApi>(request);
            apiQueriedMovies.AddRange(GetApiQueriedMovies(movies?.Data, QueriedMovie.SmallSize));

            return new ReadOnlyCollection<QueriedMovie>(apiQueriedMovies).SomeNotNull();
        }

        private IEnumerable<QueriedMovie> GetApiQueriedMovies(MoviesFromExternalApi moviesData, Size size)
        {
            if (moviesData?.Results == null) yield break;
            foreach (var result in moviesData.Results)
            {
                yield return new QueriedMovie(
                    new OriginalTitle(result.OriginalTitle), 
                    result.Overview,
                    result.GenreIds.FirstOrDefault().ToString(),
                    new OriginalLanguage(result.OriginalLanguage),
                    result.ReleaseDate,
                    "",
                    new List<string>(),
                    new SeatsSold(1),
                    size
                );
            }
        }
    }
}