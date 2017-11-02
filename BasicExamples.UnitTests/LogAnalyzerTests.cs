using System;
using NUnit.Framework;

namespace BasicExamples.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_InvalidExtension_ReturnsFalse()
        {
            var logAnalyzer = CreateLogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("fileWithInvalidExtension.foo");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_ValidExtensionLowercase_ReturnsTrue()
        {
            var logAnalyzer = CreateLogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("fileWithValidExtension.slf");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidLogFileName_ValidExtensionUppercase_ReturnsTrue()
        {
            var logAnalyzer = CreateLogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("fileWithValidExtension.SLF");

            Assert.IsTrue(result);
        }

        //***** Refactor to parametrized tests.
        [TestCase("fileWithValidExtension.slf")]
        [TestCase("fileWithValidExtension.SLF")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string fileName)
        {
            var logAnalyzer = CreateLogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName(fileName);

            Assert.IsTrue(result);
        }

        //**** Catching exceptions.
        [Test]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            var logAnalyzer = CreateLogAnalyzer();

            TestDelegate methodCall = () => logAnalyzer.IsValidLogFileName("");

            var exceptionThrown = Assert.Throws<ArgumentException>(methodCall);
            StringAssert.Contains("fileName can not be null or empty", exceptionThrown.Message);

            // Do not use try catch block.

            // Fluent alternative.
            //Assert.That(() => logAnalyzer.IsValidLogFileName(""),
            //    Throws.ArgumentException
            //    .And.Message.Contains("fileName can not be null or empty"));
        }

        //********* Factory method.
        private static LogAnalyzer CreateLogAnalyzer()
        {
            return new LogAnalyzer();
        }

        //**** State-based tests.
        [TestCase("badfilename.foo", false)]
        [TestCase("goodfilename.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string fileName, bool expected)
        {
            var logAnalyzer = new LogAnalyzerWithState();

            logAnalyzer.IsValidLogFileName(fileName);

            Assert.AreEqual(expected, logAnalyzer.WasLastFileNameValid);
        }

        //***** SetUp and TearDown
        //private LogAnalyzer logAnalyzer;

        //[SetUp]
        //public void SetUp()
        //{
        //    this.logAnalyzer = CreateLogAnalyzer();
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    // This is an antipattern
        //    // Never do it, this is not needed.
        //    this.logAnalyzer = null;

        //    // Use this jut to reset the state of a static variable or singleton.
        //}
    }
}
