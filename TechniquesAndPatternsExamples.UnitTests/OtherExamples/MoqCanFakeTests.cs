
using System;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace TechniquesAndPatternsExamples.UnitTests.OtherExamples
{
    [TestFixture]
    public class MoqCanFakeTests
    {
        public class MyClass
        {
            public int PublicNonVirtualMethod()
            {
                return 1;
            }

            public virtual int PublicVirtualMethod()
            {
                return 1;
            }

            protected virtual int ProtectedVirtualMethod()
            {
                return 1;
            }

            public int CallProtectedMethod()
            {
                return this.ProtectedVirtualMethod();
            }
        }

        [Test]
        public void NonVirtualMethod_CanNotBeFaked()
        {
            var mock = new Mock<MyClass>();

            // Setup will throw exception because non virtual methods can not be overrided nor mocked.
            TestDelegate setup = () => mock.Setup(m => m.PublicNonVirtualMethod()).Returns(10);

            Assert.Throws<NotSupportedException>(setup);

            Assert.AreEqual(1, mock.Object.PublicNonVirtualMethod());
        }

        [Test]
        public void PublicVirtualMethod_CanBeFaked()
        {
            var mock = new Mock<MyClass>();

            // Setup will throw exception because non virtual methods can not be overrided nor mocked.
            mock.Setup(m => m.PublicVirtualMethod()).Returns(10);

            Assert.AreEqual(10, mock.Object.PublicVirtualMethod());
        }

        [Test]
        public void ProtectedVirtualMethod_CanBeFaked()
        {
            var mock = new Mock<MyClass>();

            // You can setup protected methods but with strings, not intelli-sense friendly.
            mock.Protected().Setup<int>("ProtectedVirtualMethod").Returns(10);

            // Anyway you can not access directly a protected method.
            Assert.AreEqual(10, mock.Object.CallProtectedMethod());
        }
    }
}
