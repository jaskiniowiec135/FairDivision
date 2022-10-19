using NUnit.Framework;
using System.IO;

namespace FairDivisionAlgorithmTests.XMLHandlers
{
    [SetUpFixture]
    public class Preparation
    {
        private string testPath = "..\\..\\TestResults\\";

        [OneTimeSetUp]
        public void Cleanup()
        {
            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }

            Directory.CreateDirectory(testPath);
        }
    }
}
