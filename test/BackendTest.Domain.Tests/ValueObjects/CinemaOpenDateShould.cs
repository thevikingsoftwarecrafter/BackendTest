using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class CinemaOpenDateShould
    {
        [Fact]
        public void Fails_When_CinemaOpenDate_Is_Default_DateTime()
        {
            //Arrange
            DateTime cinemaOpenDate = default;
            
            //Act
            Action action = () =>
            {
                var sut = new CinemaOpenDate(cinemaOpenDate);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_CinemaOpenDate_Is_Valid_Value(DateTime cinemaOpenDate)
        {
            //Arrange & Act
            var sut = new CinemaOpenDate(cinemaOpenDate);

            //Assert
            sut.Value.Should().Be(cinemaOpenDate);
        }
    }
}