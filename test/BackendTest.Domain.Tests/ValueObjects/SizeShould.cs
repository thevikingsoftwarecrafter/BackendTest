using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class SizeShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Fails_When_Size_Is_Null_Or_Empty_Or_WhiteSpace(string size)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new Size(size);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_Size_Is_Valid_Value(string size)
        {
            //Arrange & Act
            var sut = new Size(size);

            //Assert
            sut.Value.Should().Be(size);
        }
    }
}