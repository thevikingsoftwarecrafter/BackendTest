using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class NameShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Fails_When_Name_Is_Null_Or_Empty_Or_WhiteSpace(string name)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new Name(name);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_Name_Is_Valid_Value(string name)
        {
            //Arrange & Act
            var sut = new Name(name);

            //Assert
            sut.Value.Should().Be(name);
        }
    }
}