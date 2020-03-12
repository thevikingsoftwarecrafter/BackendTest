using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendTest.Domain.Queries.IntelligentBillboard;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.Repositories;
using BackendTest.Domain.Services;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using Optional;
using Xunit;

namespace BackendTest.Domain.Tests.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardHandlerShould
    {
        private readonly IQueriedMoviesRepository _repository;
        private readonly IDateTimeService _dateTimeService;

        public GetIntelligentBillboardHandlerShould()
        {
            _repository = Substitute.For<IQueriedMoviesRepository>();
            _repository.GetAllMovies().Returns(FeedMoviesRepository().SomeNotNull());
            _repository.GetAllMoviesFromCity().Returns(FeedMoviesRepository().SomeNotNull());
            _dateTimeService = Substitute.For<IDateTimeService>();
            _dateTimeService.Now().Returns(new DateTime(2020, 3, 11));
        }

        [Theory]
        [InlineData(10, 2)]
        [InlineData(20, 3)]
        [InlineData(70, 10)]
        public async Task Make_A_Billboard_With_Many_Weeks_As_Needed(int periodOfTimeInDays, int weeksExpected)
        {
            //Arrange
            var handler = new GetIntelligentBillboardHandler(_repository, _dateTimeService);

            //Act
            var response = await handler.Handle(new GetIntelligentBillboardRequest(periodOfTimeInDays, 1, 1, true),
                CancellationToken.None);

            //Assert
            response.Billboard.ValueOr(() => null)
                .Should().HaveCount(weeksExpected);
            response.Billboard.ValueOr(() => null)
                .ToList().Should().Match(l => l.First().WeekStart == _dateTimeService.Now());
        }

        [Fact]
        public async Task Make_A_Billboard()
        {
            //Arrange
            var handler = new GetIntelligentBillboardHandler(_repository, _dateTimeService);

            //Act
            var response = await handler.Handle(new GetIntelligentBillboardRequest(10, 2, 2, true),
                CancellationToken.None);
            var (firstBigScreen, firstBigScreenMovie) = response.Billboard.ValueOr(() => null).First().MoviesOnBigScreen.First();
            var (secondBigScreen, secondBigScreenMovie) = response.Billboard.ValueOr(() => null).First().MoviesOnBigScreen.Skip(1).First();
            var (firstSmallScreen, firstSmallScreenMovie) = response.Billboard.ValueOr(() => null).First().MoviesOnSmallScreen.First();
            var (firstBigScreen2NdWeek, firstBigScreenMovie2NdWeek) = response.Billboard.ValueOr(() => null).Skip(1).First().MoviesOnBigScreen.First();

            //Assert
            response.Billboard.ValueOr(() => null).ToList().Should()
                .Match(l => l.First().MoviesOnBigScreen.Count == 2)
                .And.Match(l => l.First().MoviesOnSmallScreen.Count == 2);
            firstBigScreen.Value.Should().Be(1);
            firstBigScreenMovie.Title.Value.Should().Be("Some Title 1");
            secondBigScreen.Value.Should().Be(2);
            secondBigScreenMovie.Title.Value.Should().Be("Some Title 2.1");
            firstSmallScreen.Value.Should().Be(1);
            firstSmallScreenMovie.Title.Value.Should().Be("Some Title 4");
            firstBigScreen2NdWeek.Value.Should().Be(1);
            firstBigScreenMovie2NdWeek.Title.Value.Should().Be("Some Title 2");
        }

        private ReadOnlyCollection<QueriedMovie> FeedMoviesRepository()
        {
            return new ReadOnlyCollection<QueriedMovie>(new List<QueriedMovie>()
            {
                new QueriedMovie(new OriginalTitle("Some Title 1"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 1, 1), "www.google.com", null, new SeatsSold(100), QueriedMovie.BigSize),
                new QueriedMovie(new OriginalTitle("Some Title 2"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 3, 17), "www.google.com", null, new SeatsSold(80), QueriedMovie.BigSize),
                new QueriedMovie(new OriginalTitle("Some Title 2.1"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 3, 1), "www.google.com", null, new SeatsSold(70), QueriedMovie.BigSize),
                new QueriedMovie(new OriginalTitle("Some Title 3"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 4, 1), "www.google.com", null, new SeatsSold(20), QueriedMovie.SmallSize),
                new QueriedMovie(new OriginalTitle("Some Title 4"), "Some Overview", "Terror", new OriginalLanguage("en"), new DateTime(2020, 1, 1), "www.google.com", null, new SeatsSold(5), QueriedMovie.SmallSize),
            });
        }
    }
}