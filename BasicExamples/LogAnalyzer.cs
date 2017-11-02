using System;

namespace BasicExamples
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(nameof(fileName), "fileName can not be null or empty");
            }

            if (!fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }


    }

    public class LogAnalyzerWithState
    {
        //** State-based test.
        public bool WasLastFileNameValid { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(nameof(fileName), "fileName can not be null or empty");
            }

            if (!fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            WasLastFileNameValid = true;
            return true;
        }
    }
}
