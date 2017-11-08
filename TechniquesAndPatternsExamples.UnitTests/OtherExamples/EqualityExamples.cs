using ExpectedObjects;
using FluentAssertions;
using NUnit.Framework;

namespace TechniquesAndPatternsExamples.UnitTests.OtherExamples
{
    public class Foo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class FooResemblance : Foo
    {
        public override bool Equals(object obj)
        {
            var other = obj as Foo;
            if (other != null)
            {
                return string.Equals(this.FirstName, other.FirstName)
                       && string.Equals(this.LastName, other.LastName);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.FirstName != null ? this.FirstName.GetHashCode() : 0) * 397) ^ (this.LastName != null ? this.LastName.GetHashCode() : 0);
            }
        }
    }

    [TestFixture]
    public class EqualityExamples
    {
        [Test]
        public void TestResemblance()
        {
            var actual = new Foo {FirstName = "first", LastName = "last"};
            var expected = new FooResemblance {FirstName = "first", LastName = "last"};

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestToExpectedObject()
        {
            var actual = new Foo
            {
                FirstName = "first", LastName = "last"
            };

            var expected = new Foo
            {
                FirstName = "first", LastName = "last"
            }.ToExpectedObject();
            
            expected.ShouldEqual(actual);

            // Odd error messages if use this, favor ShouldEqual(actual) method.
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestShouldBeEquivalent()
        {
            var actual = new Foo { FirstName = "first", LastName = "last" };

            var expected = new Foo
            {
                FirstName = "first",
                LastName = "last"
            };

            expected.ShouldBeEquivalentTo(actual);
        }
    }
}
