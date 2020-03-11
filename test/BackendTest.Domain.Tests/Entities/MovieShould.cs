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
    public class MovieShould
    {
        [Theory, AutoData]
        public void Be_Created_With_Expected_Attributes(string originalTitle, DateTime releaseDate, string originalLanguage, bool adult)
        {
            //Arrange & Act
            var movie = new Movie(
                new OriginalTitle(originalTitle), 
                new ReleaseDate(releaseDate), 
                new OriginalLanguage(originalLanguage), 
                new Adult(adult), 
                null
            );

            //Assert
            movie.Id.Should().NotBe(Guid.Empty.GetHashCode());
            movie.OriginalTitle.Value.Should().Be(originalTitle);
            movie.ReleaseDate.Value.Should().Be(releaseDate);
            movie.OriginalLanguage.Value.Should().Be(originalLanguage);
            movie.Adult.Value.Should().Be(adult);
        }
    }
}
