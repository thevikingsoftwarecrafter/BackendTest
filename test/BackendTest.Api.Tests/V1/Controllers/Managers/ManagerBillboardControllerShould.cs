using System;
using System.Collections.Generic;
using BackendTest.Api.V1.Controllers.Managers;
using BackendTest.Api.V1.Models.BillBoards;
using BackendTest.Api.V1.Models.Pagination;
using BackendTest.Domain.Queries.IntelligentBillboard;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Optional;
using Xunit;

namespace BackendTest.Api.Tests.V1.Controllers.Managers
{
    public class ManagerBillboardControllerShould
    {
        private readonly IMediator _mediator;

        public ManagerBillboardControllerShould()
        {
            _mediator = Substitute.For<IMediator>();
            _mediator
                .Send(Arg.Any<GetIntelligentBillboardRequest>())
                .Returns(SeedIntelligentBillboard());
        }

        private GetIntelligentBillboardResponse SeedIntelligentBillboard()
        {
            return new GetIntelligentBillboardResponse(
                (new List<BillboardLine> {
                    new BillboardLine(
                        new WeekStart(new DateTime(2020, 1, 1)), 
                        new List<(Screen, QueriedMovie)>()
                        {
                            (new Screen(1), new QueriedMovie(new OriginalTitle("Some Title 1"), "", "", new OriginalLanguage("en"),new DateTime(2000,1,1), "", null, new SeatsSold(100), QueriedMovie.BigSize)),
                            (new Screen(2), new QueriedMovie(new OriginalTitle("Some Title 2"), "", "", new OriginalLanguage("en"),new DateTime(2000,1,1), "", null, new SeatsSold(90), QueriedMovie.BigSize)),
                            (new Screen(3), new QueriedMovie(new OriginalTitle("Some Title 3"), "", "", new OriginalLanguage("en"),new DateTime(2000,1,1), "", null, new SeatsSold(80), QueriedMovie.BigSize)),
                        }, 
                        new List<(Screen, QueriedMovie)>()
                        {
                            (new Screen(1), new QueriedMovie(new OriginalTitle("Some Title 4"), "", "", new OriginalLanguage("en"),new DateTime(1980,1,1), "", null, new SeatsSold(10), QueriedMovie.SmallSize)),
                            (new Screen(2), new QueriedMovie(new OriginalTitle("Some Title 5"), "", "", new OriginalLanguage("en"),new DateTime(1980,1,1), "", null, new SeatsSold(9), QueriedMovie.SmallSize)),
                        }),
                    new BillboardLine(
                        new WeekStart(new DateTime(2020, 1, 8)), 
                        new List<(Screen, QueriedMovie)>()
                        {
                            (new Screen(1), new QueriedMovie(new OriginalTitle("Some Title 10"), "", "", new OriginalLanguage("en"),new DateTime(2000,1,1), "", null, new SeatsSold(100), QueriedMovie.BigSize)),
                            (new Screen(2), new QueriedMovie(new OriginalTitle("Some Title 20"), "", "", new OriginalLanguage("en"),new DateTime(2000,1,1), "", null, new SeatsSold(90), QueriedMovie.BigSize)),
                            (new Screen(3), new QueriedMovie(new OriginalTitle("Some Title 30"), "", "", new OriginalLanguage("en"),new DateTime(2000,1,1), "", null, new SeatsSold(80), QueriedMovie.BigSize)),
                        }, 
                        new List<(Screen, QueriedMovie)>()
                        {
                            (new Screen(1), new QueriedMovie(new OriginalTitle("Some Title 40"), "", "", new OriginalLanguage("en"),new DateTime(1980,1,1), "", null, new SeatsSold(10), QueriedMovie.SmallSize)),
                            (new Screen(2), new QueriedMovie(new OriginalTitle("Some Title 50"), "", "", new OriginalLanguage("en"),new DateTime(1980,1,1), "", null, new SeatsSold(9), QueriedMovie.SmallSize)),
                        }),
                }).SomeNotNull());
        }

        [Fact]
        public async void Get_Intelligent_Billboard()
        {
            //Arrange
            var controller = new ManagersBillboardsController(_mediator);

            //Act
            var result =
                await controller.GetIntelligent(1, 1, 1, true, new PageParameters() {PageNumber = 1, PageSize = 1});

            //Assert
            var typedResult = (PagedList<IntelligentBillboardResponse>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.TotalPages.Should().Be(2);
            typedResult.Count.Should().Be(1);
        }
    }
}