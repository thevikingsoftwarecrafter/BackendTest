using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class WeekStartShould
    {
        [Fact]
        public void Fails_When_WeekStart_Is_Default_DateTime()
        {
            //Arrange
            DateTime weekStart = default;
            
            //Act
            Action action = () =>
            {
                var sut = new WeekStart(weekStart);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_WeekStart_Is_Valid_Value(DateTime weekStart)
        {
            //Arrange & Act
            var sut = new WeekStart(weekStart);

            //Assert
            sut.Value.Should().Be(weekStart);
        }
    }
}