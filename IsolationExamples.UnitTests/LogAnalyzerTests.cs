using System;
using Moq;
using NUnit.Framework;

namespace IsolationExamples.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_SupportedFileExtension_ReturnsTrue()
        {
            var extensionManagerStub = new Mock<IExtensionManager>();
            extensionManagerStub
                .Setup(em => em.IsValid(It.IsAny<string>()))
                .Returns(true);

            var logAnalyzer = CreateLogAnalyzer(extensionManagerStub.Object);

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidLogFileName_NotSupportedFileExtension_ReturnsFalse()
        {
            var logAnalyzer = CreateLogAnalyzer();

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            var extensionManagerStub = new Mock<IExtensionManager>();
            extensionManagerStub
                .Setup(em => em.IsValid(It.IsAny<string>()))
                .Throws(new Exception("Exception fake"));

            var logAnalyzer = CreateLogAnalyzer(extensionManagerStub.Object);

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidLogFileName_ExtensionManagerThrowsException_CallsLogger()
        {
            var extensionManagerStub = new Mock<IExtensionManager>();
            extensionManagerStub.Setup(em => em.IsValid(It.IsAny<string>())).Throws(new Exception("Exception fake"));

            var loggerMock = new Mock<ILogger>();

            var logAnalyzer = CreateLogAnalyzer(extensionManagerStub.Object, loggerMock.Object);

            logAnalyzer.IsValidLogFileName("myfile.exe");

            // Verify interaction.
            loggerMock.Verify(l => l.LogError(
                It.Is<string>(
                    message => message.Contains("Exception fake"))));
        }

        private static LogAnalyzer CreateLogAnalyzer()
        {
            return new LogAnalyzer(new Mock<IExtensionManager>().Object, new Mock<ILogger>().Object);
        }

        private static LogAnalyzer CreateLogAnalyzer(IExtensionManager extensionManager)
        {
            return new LogAnalyzer(extensionManager, new Mock<ILogger>().Object);
        }

        private static LogAnalyzer CreateLogAnalyzer(IExtensionManager extensionManager, ILogger logger)
        {
            return new LogAnalyzer(extensionManager, logger);
        }

        // Virtual method alternative with manual fakes.
        [Test]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue()
        {
            var extensionManagerStub = new FakeExtensionManager();
            extensionManagerStub.WillBeValid = true;

            var logAnalyzer = new TestableLogAnalyzer(extensionManagerStub);

            var result = logAnalyzer.IsValidLogFileName("myfile.exe");

            Assert.IsTrue(result);
        }

        internal class FakeExtensionManager : IExtensionManager
        {
            public bool WillBeValid; // false by default
            public Exception WillThrow; // null by default

            public bool IsValid(string fileName)
            {
                if (this.WillThrow != null)
                {
                    throw this.WillThrow;
                }

                return this.WillBeValid;
            }
        }

        internal class TestableLogAnalyzer : LogAnalyzerVirtual
        {
            private readonly IExtensionManager extensionManager;

            public TestableLogAnalyzer(IExtensionManager extensionManager)
            {
                this.extensionManager = extensionManager;
            }

            protected override IExtensionManager GetExtensionManager()
            {
                return this.extensionManager;
            }
        }
    }
}
