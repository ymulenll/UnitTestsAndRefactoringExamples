using Moq;

namespace IsolationExamples.UnitTests.CreationalPatterns
{
    internal class LogAnalyzerBuilder
    {
        private IExtensionManager extensionManager;
        private ILogger logger;

        internal LogAnalyzerBuilder()
        {
            this.extensionManager = new Mock<IExtensionManager>().Object;
            this.logger = new Mock<ILogger>().Object;
        }

        internal LogAnalyzerBuilder WithLogger(ILogger logger)
        {
            this.logger = logger;
            return this;
        }

        internal LogAnalyzerBuilder WithExtensionManager(IExtensionManager extensionManager)
        {
            this.extensionManager = extensionManager;
            return this;
        }

        internal LogAnalyzer Build()
        {
            return new LogAnalyzer(this.extensionManager, this.logger);
        }

        public static implicit operator LogAnalyzer(LogAnalyzerBuilder logAnalyzerBuilder)
        {
            return logAnalyzerBuilder.Build();
        }
    }
}
