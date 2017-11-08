using System;
using IsolationExamples.UnitTests.CreationalPatterns;
using Moq;
using NUnit.Framework;

namespace IsolationExamples.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTestsUsingFixturePattern
    {
        [Test]
        public void IsValidLogFileName_NotSupportedFileExtension_ReturnsFalse()
        {
            var logAnalyzer = new LogAnalyzerFixture().CreateSut();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_SupportedFileExtension_ReturnsTrue()
        {
            var fixture = new LogAnalyzerFixture();

            fixture.extensionManager.AsMock()
                .Setup(em => em.IsValid(It.IsAny<string>()))
                .Returns(true);

            var logAnalyzer = fixture.CreateSut();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            var fixture = new LogAnalyzerFixture();

            fixture.extensionManager.AsMock()
                .Setup(em => em.IsValid(It.IsAny<string>()))
                .Throws(new Exception("Exception fake"));

            LogAnalyzer logAnalyzer = fixture.CreateSut();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_CallsLogger()
        {
            var fixture = new LogAnalyzerFixture();

            fixture.extensionManager.AsMock()
                .Setup(em => em.IsValid(It.IsAny<string>()))
                .Throws(new Exception("Exception fake"));

            LogAnalyzer logAnalyzer = fixture.CreateSut();

            logAnalyzer.IsValidLogFileName("myfile.exe");

            // Verify interaction.
            fixture.logger.AsMock().Verify(l => l.LogError(
                It.Is<string>(
                    message => message.Contains("Exception fake"))));
        }
    }
}
