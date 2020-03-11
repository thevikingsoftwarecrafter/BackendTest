using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class ReleaseDateShould
    {
        [Fact]
        public void Fails_When_ReleaseDate_Is_Default_DateTime()
        {
            //Arrange
            DateTime releaseDate = default;
            
            //Act
            Action action = () =>
            {
                var sut = new ReleaseDate(releaseDate);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_ReleaseDate_Is_Valid_Value(DateTime releaseDate)
        {
            //Arrange & Act
            var sut = new ReleaseDate(releaseDate);

            //Assert
            sut.Value.Should().Be(releaseDate);
        }
    }
}