using System;
using AutoFixture;
using AutoFixture.Xunit2;
using BackendTest.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.Entities
{
    public class EntityShould
    {
        internal class EntityToTest : Entity<Guid>
        {
            public string SomeProperty { get; private set; }
            public EntityToTest(Guid id, string someProperty) : base(id)
            {
                SomeProperty = someProperty;
            }
        }

        [Theory, AutoData]
        public void BeEqualWhenIdIsTheSame(string someProperty1, string someProperty2)
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var entity1 = new EntityToTest(id, someProperty1);
            var entity2 = new EntityToTest(id, someProperty2);

            //Assert
            entity1.Should().Be(entity2);
        }
    }
}