using System;

namespace IsolationExamples
{
    public class RealExensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            // read through the configuration file
            // returns true if configuration says extension is supported

            // this is just to compile
            return new Random().Next(2) % 2 == 0;
        }
    }
}
