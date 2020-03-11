using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class StartTimeShould
    {
        [Fact]
        public void Fails_When_StartTime_Is_Default_DateTime()
        {
            //Arrange
            DateTime startTime = default;
            
            //Act
            Action action = () =>
            {
                var sut = new StartTime(startTime);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_StartTime_Is_Valid_Value(DateTime startTime)
        {
            //Arrange & Act
            var sut = new StartTime(startTime);

            //Assert
            sut.Value.Should().Be(startTime);
        }
    }
}