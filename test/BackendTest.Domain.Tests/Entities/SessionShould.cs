using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackendTest.Domain.Entities;
using BackendTest.Domain.ValueObjects;
using BackendTest.Infrastructure.Data.DBContext;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.Entities
{
    public class SessionShould
    {
        [Theory, AutoData]
        public void Be_Created_With_Expected_Attributes(DateTime starTime, DateTime endTime, int? seatsSold)
        {
            //Arrange & Act
            var session = new Session(
                new StartTime(starTime), 
                new EndTime(endTime), 
                new SeatsSold(seatsSold), 
                null,
                null
            );

            //Assert
            session.Id.Should().NotBe(Guid.Empty.GetHashCode());
            session.StartTime.Value.Should().Be(starTime);
            session.EndTime.Value.Should().Be(endTime);
            session.SeatsSold.Value.Should().Be(seatsSold);
        }
    }
}
