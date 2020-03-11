using AutoFixture.Xunit2;
using System;
using BackendTest.Domain.Entities;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.Entities
{
    public class RoomShould
    {
        [Theory, AutoData]
        public void Be_Created_With_Expected_Attributes(string name, string size, int seats)
        {
            //Arrange & Act
            var room = new Room(
                new Name(name), 
                new Size(size), 
                new Seats(seats), 
                null,
                null
            );

            //Assert
            room.Id.Should().NotBe(Guid.Empty.GetHashCode());
            room.Name.Value.Should().Be(name);
            room.Size.Value.Should().Be(size);
            room.Seats.Value.Should().Be(seats);
        }
    }
}
