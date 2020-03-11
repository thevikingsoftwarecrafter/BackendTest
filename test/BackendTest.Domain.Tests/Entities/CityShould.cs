using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackendTest.Domain.Entities;
using BackendTest.Domain.ValueObjects;
using BackendTest.Infrastructure.Data.DBContext;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.Entities
{
    public class CityShould
    {
        [Theory, AutoData]
        public void Be_Created_With_Expected_Attributes(string name, int population)
        {
            //Arrange & Act
            var city = new City(
                new Name(name),
                new Population(population)
            );

            //Assert
            city.Id.Should().NotBe(Guid.Empty.GetHashCode());
            city.Name.Value.Should().Be(name);
            city.Population.Value.Should().Be(population);
        }
    }
}
