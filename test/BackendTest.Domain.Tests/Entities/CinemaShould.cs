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
    public class CinemaShould
    {
        [Theory, AutoData]
        public void Be_Created_With_Expected_Attributes(string name, DateTime openSince, string cityName, int cityPopulation, string roomName)
        {
            //Arrange & Act
            var cinema = new Cinema(
                new Name(name),
                new CinemaOpenDate(openSince), 
                new City(new Name(cityName), new Population(cityPopulation)), 
                new List<Room>()
                {
                    new Room(){Name = roomName}
                }
            );

            //Assert
            cinema.Id.Should().NotBe(Guid.Empty.GetHashCode());
            cinema.Name.Value.Should().Be(name);
            cinema.OpenSince.Value.Should().Be(openSince);
            cinema.City.Name.Value.Should().Be(cityName);
            cinema.City.Population.Value.Should().Be(cityPopulation);
            cinema.Room.First().Name.Should().Be(roomName);
        }
    }
}
