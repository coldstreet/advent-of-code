using AdventOfCode2017.Tests.Helpers;
using NUnit.Framework;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class RegisterTests
    {
        public void ConvertInput_FormattedInstructions_ConvertsToRegisterInstructionsArray()
        {
            // arrange
            string[] input =
            {
                "b inc 5 if a > 1",
                "a inc 1 if b < 5",
                "c dec -10 if a >= 1",
                "c inc -20 if c == 10"
            };

            // act
            var actual = Register.ConvertInstructions(input);

            // assert
            Assert.AreEqual("c", actual[2].RegisterName);
            Assert.AreEqual(RegisterInstructions.Operation.Dec, actual[2].Instruction);
            Assert.AreEqual(-10, actual[2].OperationAmount);
            Assert.AreEqual("a", actual[2].LogicRegisterName);
            Assert.AreEqual(RegisterInstructions.Conditional.GreaterThanOrEqual, actual[2].LogicConditional);
            Assert.AreEqual(1, actual[2].LogicAmount);
        }

        [Test]
        public void FindMaxRegisterValue_FormattedInstructions_FindsTheMaxRegisterValue()
        {
            // arrange
            string[] input =
            {
                "b inc 5 if a > 1",
                "a inc 1 if b < 5",
                "c dec -10 if a >= 1",
                "c inc -20 if c == 10"
            };

            // act
            var instructions = Register.ConvertInstructions(input);
            var actual = Register.ProcessRegister(instructions);

            // assert
            Assert.AreEqual(1, actual.register.Values.Max());
        }

        [Test]
        public void FindMaxRegisterValue_FormattedInstructionsFromInputFileV1_FindsTheMaxRegisterValue()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.RegisterInputV1.txt");
            

            // act
            var instructions = Register.ConvertInstructions(input);
            var actual = Register.ProcessRegister(instructions);

            // assert
            Assert.AreEqual(4832, actual.register.Values.Max());
        }

        [Test]
        public void FindMaxRegisterValueReached_FormattedInstructionsFromInputFileV1_FindsTheMaxRegisterValueReached()
        {
            // arrange
            string[] input = EmbeddedResource.ReadFile("AdventOfCode2017.Tests.RegisterInputV1.txt");


            // act
            var instructions = Register.ConvertInstructions(input);
            var actual = Register.ProcessRegister(instructions);

            // assert
            Assert.AreEqual(5443, actual.maxRegistryValueReached);
        }
    }
}
