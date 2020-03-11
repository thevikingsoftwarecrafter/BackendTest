using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class SeatsShould
    {
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Fails_When_Seats_Is_Zero_Or_Negative(int seats)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new Seats(seats);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_Seats_Is_Valid_Value(int seats)
        {
            //Arrange & Act
            var sut = new Seats(seats);

            //Assert
            sut.Value.Should().Be(seats);
        }
    }
}