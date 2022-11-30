using System.Reflection;
using NUnit.Framework;

namespace AdventOfCode2015.Tests
{
    [TestFixture]
    public class Day05Tests
    {
        [TestCase("haegwjzuvuyypabu", false)]
        [TestCase("haegwjzuvuyypcdu", false)]
        [TestCase("haegwjzuvuyyppqu", false)]
        [TestCase("haegwjzuvuyypxyu", false)]
        [TestCase("haegwjzuvuyypu", true)]
        [TestCase("jchzalrnumimnmhp", false)]
        [TestCase("dvszwmarrgswjxmb", false)]
        public void DetermineIfNice_VariousInputs_CorrectDetermination(string input, bool expected)
        {
            // arrange

            // act
            var result = Day05.DetermineIfNice(input);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DetermineNiceCount_OfficalInput_CorrectCount()
        {
            // arrange
            List<string> inputs = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("AdventOfCode2015.Tests.Day05Input.txt")!)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    do
                    {
                        inputs.Add(reader.ReadLine()!);
                    }
                    while (reader.Peek() != -1);
                }
            }

            // act
            var result = Day05.CountNiceStrings(inputs.ToArray());

            // assert
            Assert.AreEqual(255, result);
        }
    }
}
