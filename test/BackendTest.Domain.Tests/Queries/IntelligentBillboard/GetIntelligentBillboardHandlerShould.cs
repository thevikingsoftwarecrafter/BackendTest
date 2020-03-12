using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            response.Billboard.ValueOr(new ReadOnlyCollection<BillboardLine>(new List<BillboardLine>())).Should().HaveCount(weeksExpected);
        }

        private ReadOnlyCollection<QueriedMovie> FeedMoviesRepository()
        {
            return new ReadOnlyCollection<QueriedMovie>(new List<QueriedMovie>()
            {
                new QueriedMovie(new OriginalTitle("aaa"), "", "g1", new OriginalLanguage("l1"), new DateTime(2020, 1, 1), "", null, new SeatsSold(100), QueriedMovie.BigSize)
            });
        }
    }
}