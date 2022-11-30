using AdventOfCode2017.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class JumpOffsetsTests
    {
        [Test]
        public void JumpOffsetTest()
        {
            // arrange
            int[] inputs = {0, 3, 0, 1, -3};

            // act
            var actual = JumpOffsets.PerformJumpsAndCountSteps(inputs);

            // assert
            Assert.AreEqual(5, actual);
        }

        [Test]
        public void JumpOffsetV2Test()
        {
            // arrange
            int[] inputs = { 0, 3, 0, 1, -3 };

            // act
            var actual = JumpOffsets.PerformJumpsAndCountStepsV2(inputs);

            // assert
            Assert.AreEqual(10, actual);
        }

        [Test]
        public void CountJumpSets_UseFileWithTestInput_CountOfStepsMade()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.JumpOffsetsInputV1.txt");

            List<int> inputs = new List<int>();
            foreach (var row in input)
            {
                if (!int.TryParse(row, out int number))
                {
                    throw new ArgumentException("Unexpected non number input from input file");
                }

                inputs.Add(number);
            }

            // act
            var countSteps = JumpOffsets.PerformJumpsAndCountSteps(inputs.ToArray());


            // assert
            Assert.AreEqual(364539, countSteps);
        }

        [Test]
        public void CountJumpSets_UseFileWithTestInputV2_CountOfStepsMadeV2()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.JumpOffsetsInputV2.txt");

            List<int> inputs = new List<int>();
            foreach (var row in input)
            {
                if (!int.TryParse(row, out int number))
                {
                    throw new ArgumentException("Unexpected non number input from input file");
                }

                inputs.Add(number);
            }

            // act
            var countSteps = JumpOffsets.PerformJumpsAndCountStepsV2(inputs.ToArray());


            // assert
            Assert.AreEqual(27477714, countSteps);
        }
    }
}