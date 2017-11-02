using System;
using IsolationExamples;

namespace IsolationExamples
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager extensionManager;
        private readonly ILogger logger;

        public LogAnalyzer(IExtensionManager extensionManager, ILogger logger)
        {
            this.extensionManager = extensionManager;
            this.logger = logger;
        }

        public bool IsValidLogFileName(string fileName)
        {
            try
            {
                return this.extensionManager.IsValid(fileName);
            }
            catch (Exception e)
            {
                this.logger.LogError($"Error from extension manager: {e}");
                return false;
            }
        }
    }

    // Virtual method.
    public class LogAnalyzerVirtual
    {
        public bool IsValidLogFileName(string fileName)
        {
            IExtensionManager extensionManager = this.GetExtensionManager();
            return extensionManager.IsValid(fileName);
        }

        protected virtual IExtensionManager GetExtensionManager()
        {
            return new RealExensionManager();
        }
    }
}
