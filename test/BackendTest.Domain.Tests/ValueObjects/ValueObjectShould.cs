using System.Linq;
using BackendTest.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.ValueObjects
{
    public class ValueObjectShould
    {
        internal class ValueObjectToTest : ValueObject
        {
            public string SomeValue1 { get; }
            public int SomeValue2 { get; }

            public ValueObjectToTest(string someValue1, int someValue2)
            {
                SomeValue1 = someValue1;
                SomeValue2 = someValue2;
            }
        }

        [Fact]
        public void Be_Equal_To_Null_Object_When_Is_Null()
        {
            //Arrange & Act
            ValueObjectToTest sut = null;
            ValueObjectToTest other = null;

            //Assert
            sut.Should().Be((object)other);
        }

        [Fact]
        public void Be_Equal_To_Object_With_Same_Values()
        {
            //Arrange & Act
            var sut = new ValueObjectToTest("foo", 23);
            var other = new ValueObjectToTest("foo", 23);

            //Assert
            sut.Should().Be((object)other);
            sut.Should().Be(other);
        }

        [Fact]
        public void Not_Be_Equal_To_Object_With_Other_Values()
        {
            //Arrange & Act
            var sut = new ValueObjectToTest("foo", 23);
            var other = new ValueObjectToTest("bar", 11);

            //Assert
            sut.Should().NotBe((object)other);
            sut.Should().NotBe(other);
        }

        [Fact]
        public void Be_Contained_In_Collection_With_Same_Value()
        {
            //Arrange & Act
            var sut = new[] { new ValueObjectToTest("foo", 23) };
            var other = new ValueObjectToTest("foo", 23);

            //Assert
            sut.Should().Match(m => m.Contains(other));
            sut.Should().Match(m => m.Contains((object)other));
        }
    }
}