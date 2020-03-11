using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class ScreenShould
    {
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Fails_When_Screen_Is_Zero_Or_Negative(int screen)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new Screen(screen);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_Screen_Is_Valid_Value(int screen)
        {
            //Arrange & Act
            var sut = new Screen(screen);

            //Assert
            sut.Value.Should().Be(screen);
        }
    }
}