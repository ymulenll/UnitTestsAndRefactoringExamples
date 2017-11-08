using System;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit3;

namespace IsolationExamples.UnitTests.AutoMockingContainer
{
    [TestFixture]
    public class LogAnalyzerAutoMockingContainerTests
    {
        //***** Auto Mocking container.
        [Test]
        public void IsValidLogFileName_NotSupportedFileExtension_ReturnsFalse()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var logAnalyzer = fixture.Create<LogAnalyzer>();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_SupportedFileExtension_ReturnsTrue()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var extensionManagerStub = fixture.Freeze<Mock<IExtensionManager>>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Returns(true);

            var logAnalyzer = fixture.Create<LogAnalyzer>();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var extensionManagerStub = fixture.Freeze<Mock<IExtensionManager>>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Throws(new Exception("Exception fake"));

            var logAnalyzer = fixture.Create<LogAnalyzer>();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_CallsLogger()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var extensionManagerStub = fixture.Freeze<Mock<IExtensionManager>>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Throws(new Exception("Exception fake"));

            var loggerMock = fixture.Freeze<Mock<ILogger>>();

            var logAnalyzer = fixture.Create<LogAnalyzer>();

            logAnalyzer.IsValidLogFileName("myfile.exe");

            // Verify interaction.
            loggerMock.Verify(l => l.LogError(
                It.Is<string>(
                    message => message.Contains("Exception fake"))));
        }

        [Test, AutoMoqData]
        public void IsValidLogFileName_ExtensionManagerThrowsException_CallsLoggerAutoData(
            [Frozen]Mock<IExtensionManager> extensionManagerStub,
            [Frozen]Mock<ILogger> loggerMock,
            LogAnalyzer logAnalyzer)
        {
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Throws(new Exception("Exception fake"));

            logAnalyzer.IsValidLogFileName("myfile.exe");

            // Verify interaction.
            loggerMock.Verify(l => l.LogError(
                It.Is<string>(
                    message => message.Contains("Exception fake"))));
        }
    }
}
