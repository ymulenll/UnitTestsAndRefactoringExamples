using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BasicExamples.UnitTests
{
    [TestFixture]
    public class NUnitStructure
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestMethod1()
        {

        }

        [Test]
        //[Timeout(100)]
        //[Explicit("Because it is too slow")] //or you can use [Ignore("Reason")]
        //[Category("Slow tests")]
        public void TestMethod2()
        {
            //Thread.Sleep(1000);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}
