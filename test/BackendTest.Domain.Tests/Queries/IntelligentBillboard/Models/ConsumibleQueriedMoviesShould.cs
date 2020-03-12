using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace BackendTest.Domain.Tests.Queries.IntelligentBillboard.Models
{
    public class ConsumibleQueriedMoviesShould
    {
        private readonly List<QueriedMovie> _queriedMoviesFeed;

        public ConsumibleQueriedMoviesShould()
        {
            _queriedMoviesFeed = FeedMoviesRepository();
        }

        [Fact]
        public void Generate_ConsulmibleQueriedMovies_From_List_Sorting()
        {
            //Arrange
            //Act
            var consumibleQueriedMovies = new ConsumibleQueriedMovies(_queriedMoviesFeed);

            //Assert
            consumibleQueriedMovies.QueriedMovies.Should().HaveCount(_queriedMoviesFeed.Count);
            consumibleQueriedMovies.QueriedMovies.Should().Match(q => q.First().Title.Equals(_queriedMoviesFeed[2].Title));
        }

        [Fact]
        public void Not_Consume_Any_Movie_If_Date_Is_Far_Behind()
        {
            //Arrange
            //Act
            var consumibleQueriedMovies = new ConsumibleQueriedMovies(_queriedMoviesFeed);
            var conumedMovie = consumibleQueriedMovies.PopFirstValidMovieByDate(new DateTime(2000, 1, 1));

            //Assert
            consumibleQueriedMovies.QueriedMovies.Should().HaveCount(_queriedMoviesFeed.Count);
            conumedMovie.ValueOr(() => null).Should().BeNull();
        }

        [Theory, MemberData(nameof(ConsumeByDateData))]
        public void Consume_First_Matching_QueriedMovie_By_Date(DateTime date, string expectedTitle)
        {
            //Arrange
            //Act
            var consumibleQueriedMovies = new ConsumibleQueriedMovies(_queriedMoviesFeed);
            var conumedMovie = consumibleQueriedMovies.PopFirstValidMovieByDate(date);

            //Assert
            conumedMovie.ValueOr(() => null).Title.Value.Should().Be(expectedTitle);
            consumibleQueriedMovies.QueriedMovies.Should().HaveCount(_queriedMoviesFeed.Count - 1);
        }

        private static List<QueriedMovie> FeedMoviesRepository()
        {
            return new List<QueriedMovie>()
            {
                new QueriedMovie(new OriginalTitle("Some Title 1"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 1, 1), "www.google.com", null, new SeatsSold(100), QueriedMovie.BigSize),
                new QueriedMovie(new OriginalTitle("Some Title 2"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 3, 20), "www.google.com", null, new SeatsSold(80), QueriedMovie.BigSize),
                new QueriedMovie(new OriginalTitle("Some Title 3"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 3, 20), "www.google.com", null, new SeatsSold(120), QueriedMovie.BigSize),
            };
        }

        public static IEnumerable<object[]> ConsumeByDateData =>
            new[]
            {
                new object[] { new DateTime(2020, 4, 1), FeedMoviesRepository()[2].Title },
                new object[] { new DateTime(2020, 2, 1), FeedMoviesRepository()[0].Title },
            };
    }
}