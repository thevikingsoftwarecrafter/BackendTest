using System.Collections.Generic;
using System.Linq;
using BackendTest.Api.V1.Models.BillBoards;
using BackendTest.Api.V1.Models.Recomendations;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Api.V1.Models
{
    public static class Extensions
    {
        public static IntelligentBillboardResponse ToDto(this BillboardLine billboardLine)
        {
            return new IntelligentBillboardResponse()
            {
                InitialDate = billboardLine.WeekStart,
                MoviesOnBigScreen = billboardLine.MoviesOnBigScreen.ToDto().ToList(),
                MoviesOnSmallScreen = billboardLine.MoviesOnSmallScreen.ToDto().ToList()
            };
        }

        public static IEnumerable<MovieOnScreenRecomendation> ToDto(this IReadOnlyList<(Screen, QueriedMovie)> screenQueriedMovies)
        {
            foreach (var (screen, queriedMovie) in screenQueriedMovies)
            {
                yield return new MovieOnScreenRecomendation()
                {
                    Screen = screen,
                    MovieRecomendation = queriedMovie == null ? null :
                        new MovieRecomendationResponse()
                        {
                            Title = queriedMovie.Title,
                            Overview = queriedMovie.Overview,
                            Genre = queriedMovie.Genre,
                            Language = queriedMovie.Language,
                            ReleaseDate = queriedMovie.ReleaseDate,
                            Keywords = queriedMovie.Keywords == null ? null : new List<string>(queriedMovie.Keywords),
                            WebSite = queriedMovie.WebSite
                        }
                };
            }
        }
    }
}