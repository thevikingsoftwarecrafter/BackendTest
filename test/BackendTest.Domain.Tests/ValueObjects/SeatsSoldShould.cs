using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class SeatsSoldShould
    {
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Fails_When_SeatsSold_Is_Default(int seatsSold)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new SeatsSold(seatsSold);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_SeatsSold_Is_Valid_Value(int seatsSold)
        {
            //Arrange & Act
            var sut = new SeatsSold(seatsSold);

            //Assert
            sut.Value.Should().Be(seatsSold);
        }
    }
}