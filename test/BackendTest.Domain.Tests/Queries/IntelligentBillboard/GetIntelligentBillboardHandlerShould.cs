using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using BackendTest.Domain.Queries.IntelligentBillboard;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.Repositories;
using BackendTest.Domain.Services;
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
            _dateTimeService = Substitute.For<IDateTimeService>();
            _dateTimeService.Now().Returns(new DateTime(2020, 3, 11));
        }

        [Theory]
        [InlineData(10, 2)]
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
    }
}