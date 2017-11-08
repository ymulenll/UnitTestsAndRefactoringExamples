using Moq;

namespace IsolationExamples.UnitTests.CreationalPatterns
{
    internal class LogAnalyzerFixture
    {
        internal IExtensionManager extensionManager;
        internal ILogger logger;

        internal LogAnalyzerFixture()
        {
            this.extensionManager = new Mock<IExtensionManager>().Object;
            this.logger = new Mock<ILogger>().Object;
        }

        internal LogAnalyzer CreateSut()
        {
            return new LogAnalyzer(this.extensionManager, this.logger);
        }

        public static implicit operator LogAnalyzer(LogAnalyzerFixture logAnalyzerFixture)
        {
            return logAnalyzerFixture.CreateSut();
        }
    }
}
