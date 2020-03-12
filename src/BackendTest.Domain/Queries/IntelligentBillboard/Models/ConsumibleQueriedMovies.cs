using System;
using System.Collections.Generic;
using System.Linq;
using Optional;

namespace BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class ConsumibleQueriedMovies
    {
        private readonly List<QueriedMovie> _queriedMovies;
        public IReadOnlyList<QueriedMovie> QueriedMovies => _queriedMovies;

        public ConsumibleQueriedMovies(IEnumerable<QueriedMovie> queriedMovies)
        {
            _queriedMovies = queriedMovies.OrderByDescending(q => q.SeatsSold.Value).ThenByDescending(q => q.ReleaseDate).ToList();
        }

        public Option<QueriedMovie> PopFirstValidMovieByDate(DateTime date)
        {
            if (_queriedMovies == null || _queriedMovies.Count == 0) return Option.None<QueriedMovie>();
            var movie = _queriedMovies.FirstOrDefault(m => m.ReleaseDate <= date);
            if (movie != null) _queriedMovies.Remove(movie);
            return movie.SomeNotNull();
        }
    }
}