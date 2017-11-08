using System;
using NUnit.Framework;
using TechniquesAndPatternsExamples.SystemTimeExamples;

namespace TechniquesAndPatternsExamples.UnitTests.SystemTimeExamples
{
    [TestFixture]
    public class TimeLoggerTests
    {
        [Test]
        [SetCulture("en-US")]
        public void CreateMessage_Always_CreatesMessageWithCurrentTime()
        {
            string output = TimeLogger.CreateMessage("error info");

            StringAssert.Contains("11/8/2017", output);
        }

        [Test]
        [SetCulture("en-US")]
        public void SettingSystemTime_Always_ChangesTime()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));

            string output = TimeLogger.CreateMessage("a");

            StringAssert.Contains("1/1/2000", output);
        }

        [TearDown]
        public void AfterEachTest()
        {
            SystemTime.Reset();
        }
    }
}
