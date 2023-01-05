using NUnit.Framework;

namespace AdventOfCode2019.Tests
{
    [TestFixture]
    internal class Day02_1202ProgramAlarmTests
    {
        [Test]
        public void TestRestoreAndRunProgram()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day02_1202ProgramAlarmTests.txt").ToArray();

            // act
            var result = Day02_1202ProgramAlarm.RestoreAndRunProgram(input);

            // assert
            Assert.AreEqual(4484226, result);
        }

        [Test]
        public void TestRunProgramWithVariousNounVerbsUntilOutputIsKnownValue()
        {
            // arrange 
            var input = File.ReadLines("Tests/Day02_1202ProgramAlarmTests.txt").ToArray();

            // act
            var result = Day02_1202ProgramAlarm.RunProgramWithVariousNounVerbsUntilOutputIsKnownValue(input);

            // assert
            Assert.AreEqual(5696, result);
        }
    }
}
