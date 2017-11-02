using System;
using Moq;
using NUnit.Framework;

namespace IsolationExamples.UnitTests.MoqExamples
{
    [TestFixture]
    public class MoqBasics
    {
        public interface IFoo
        {
            int GetCount();

            bool DoSomething(string value);

            string DoSomethingElse(string value);

            Bar Bar { get; set; }
        }

        public class Bar
        {
            public virtual bool Submit() { return false; }
        }

        [Test]
        public void MoqTests()
        {
            var mock = new Mock<IFoo>();

            // By default returns default values.
            Assert.IsFalse(mock.Object.DoSomething("text"));

            // But you can set up the results.
            mock.Setup(foo => foo.DoSomething("text")).Returns(true);

            Assert.IsTrue(mock.Object.DoSomething("text"));

            // Or modify the result using the argument.
            mock.Setup(foo => foo.DoSomethingElse("text")).Returns<string>(s => s.ToUpper());

            // throwing when invoked with specific parameters
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));

            //*** Matching arguments
            // any value
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);

            // returning different values on each invocation
            mock = new Mock<IFoo>();
            var calls = 0;
            mock.Setup(foo => foo.GetCount())
                .Returns(() => calls)
                .Callback(() => calls++);
            // returns 0 on first invocation, 1 on the next, and so on

            //*** Verification
            mock = new Mock<IFoo>();

            // Method should never be called
            mock.Verify(foo => foo.DoSomething("ping"), Times.Never());

            // first call
            mock.Object.DoSomething("text");

            // Called at least once, by default
            mock.Verify(foo => foo.DoSomething("text"), Times.AtLeastOnce);
            mock.Verify(foo => foo.DoSomething("text"), Times.AtLeastOnce);

            //*** Customizing Mock Behavior
            // Strict mocks: Raising exceptions for anything that doesn't have a corresponding expectation.
            mock = new Mock<IFoo>(MockBehavior.Strict);

            Assert.Catch(() => mock.Object.GetCount());

            // Partial mocks: Invoke base class implementation if no expectation overrides the member.
            mock = new Mock<IFoo> { CallBase = true };

            // Recursive mocks: a mock that will return a new mock for every member that doesn't have an expectation and whose return value can be mocked
            mock = new Mock<IFoo> { DefaultValue = DefaultValue.Mock };

            // this property access would return a new mock of Bar as it's "mock-able"
            Bar value = mock.Object.Bar;

            // the returned mock is reused, so further accesses to the property return 
            // the same mock instance. this allows us to also use this instance to 
            // set further expectations on it if we want
            var barMock = Mock.Get(value);
            barMock.Setup(b => b.Submit()).Returns(true);

            // Autofixture uses by default: CallBase = true and DefaultValue = DefaultValue.Mock
        }
    }
}
