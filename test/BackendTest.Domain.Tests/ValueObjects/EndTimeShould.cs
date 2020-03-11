using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class EndTimeShould
    {
        [Fact]
        public void Fails_When_EndTime_Is_Default_DateTime()
        {
            //Arrange
            DateTime endTime = default;
            
            //Act
            Action action = () =>
            {
                var sut = new EndTime(endTime);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_EndTime_Is_Valid_Value(DateTime endTime)
        {
            //Arrange & Act
            var sut = new EndTime(endTime);

            //Assert
            sut.Value.Should().Be(endTime);
        }
    }
}