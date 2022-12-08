using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    internal class Day07_NoSpaceLeftOnDeviceTests
    {
        [Test]
        public void TestFindSumOfDirsLessThanSpecifiedSize()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day07_NoSpaceLeftOnDeviceTests.txt").ToArray();

            // act
            var result = Day07_NoSpaceLeftOnDevice.FindSumOfDirsLessThanSpecifiedSize(input);

            // assert
            Assert.AreEqual(1367870, result);
        }

        [Test]
        public void TestFindSizeOfDirToDelete()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day07_NoSpaceLeftOnDeviceTests.txt").ToArray();

            // act
            var result = Day07_NoSpaceLeftOnDevice.FindSizeOfDirToDelete(input);

            // assert
            Assert.AreEqual(549173, result);
        }
    }
}
