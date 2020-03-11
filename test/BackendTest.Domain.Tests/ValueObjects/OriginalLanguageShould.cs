using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class OriginalLanguageShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Fails_When_OriginalLanguage_Is_Null_Or_Empty_Or_WhiteSpace(string originalLanguage)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new OriginalLanguage(originalLanguage);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_OriginalLanguage_Is_Valid_Value(string originalLanguage)
        {
            //Arrange & Act
            var sut = new OriginalLanguage(originalLanguage);

            //Assert
            sut.Value.Should().Be(originalLanguage);
        }
    }
}