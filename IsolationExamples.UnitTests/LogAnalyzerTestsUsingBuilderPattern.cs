using System;
using IsolationExamples.UnitTests.CreationalPatterns;
using Moq;
using NUnit.Framework;

namespace IsolationExamples.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTestsUsingBuilderPattern
    {
        [Test]
        public void IsValidLogFileName_NotSupportedFileExtension_ReturnsFalse()
        {
            var logAnalyzer =
                new LogAnalyzerBuilder()
                .Build();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_SupportedFileExtension_ReturnsTrue()
        {
            var extensionManagerStub = new Mock<IExtensionManager>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Returns(true);

            var logAnalyzer =
                new LogAnalyzerBuilder()
                .WithExtensionManager(extensionManagerStub.Object)
                .Build();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            var extensionManagerStub = new Mock<IExtensionManager>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Throws(new Exception("Exception fake"));

            LogAnalyzer logAnalyzer = new LogAnalyzerBuilder()
                .WithExtensionManager(extensionManagerStub.Object);

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_CallsLogger()
        {
            var extensionManagerStub = new Mock<IExtensionManager>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Throws(new Exception("Exception fake"));

            var loggerMock = new Mock<ILogger>();

            LogAnalyzer logAnalyzer = new LogAnalyzerBuilder()
                .WithExtensionManager(extensionManagerStub.Object)
                .WithLogger(loggerMock.Object);

            logAnalyzer.IsValidLogFileName("myfile.exe");

            // Verify interaction.
            loggerMock.Verify(l => l.LogError(
                It.Is<string>(
                    message => message.Contains("Exception fake"))));
        }
    }
}
