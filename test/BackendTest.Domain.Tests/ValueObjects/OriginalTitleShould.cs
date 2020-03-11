using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class OriginalTitleShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Fails_When_OriginalTitle_Is_Null_Or_Empty_Or_WhiteSpace(string originalTitle)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new OriginalTitle(originalTitle);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_OriginalTitle_Is_Valid_Value(string originalTitle)
        {
            //Arrange & Act
            var sut = new OriginalTitle(originalTitle);

            //Assert
            sut.Value.Should().Be(originalTitle);
        }
    }
}