using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class OpenDateShould
    {
        [Fact]
        public void Fails_When_CinemaOpenDate_Is_Default_DateTime()
        {
            //Arrange
            DateTime openDate = default;
            
            //Act
            Action action = () =>
            {
                var sut = new OpenDate(openDate);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_CinemaOpenDate_Is_Valid_Value(DateTime openDate)
        {
            //Arrange & Act
            var sut = new OpenDate(openDate);

            //Assert
            sut.Value.Should().Be(openDate);
        }
    }
}