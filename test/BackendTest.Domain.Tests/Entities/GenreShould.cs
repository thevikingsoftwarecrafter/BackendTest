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
    public class GenreShould
    {
        [Theory, AutoData]
        public void Be_Created_With_Expected_Attributes(string name)
        {
            //Arrange & Act
            var Genre = new Genre(
                new Name(name)
            );

            //Assert
            Genre.Id.Should().NotBe(Guid.Empty.GetHashCode());
            Genre.Name.Value.Should().Be(name);
        }
    }
}
