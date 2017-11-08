using Moq;

namespace IsolationExamples.UnitTests.CreationalPatterns
{
    internal static class MoqMockExtensionMethods
    {
        internal static Mock<T> AsMock<T>(this T obj) where T : class
        {
            return Mock.Get(obj);
        }
    }
}
