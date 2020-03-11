using System;
using AutoFixture.Xunit2;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class PopulationShould
    {
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Fails_When_Population_Is_Default(int population)
        {
            //Arrange & Act
            Action action = () =>
            {
                var sut = new Population(population);
            };

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Creates_When_Population_Is_Valid_Value(int population)
        {
            //Arrange & Act
            var sut = new Population(population);

            //Assert
            sut.Value.Should().Be(population);
        }
    }
}