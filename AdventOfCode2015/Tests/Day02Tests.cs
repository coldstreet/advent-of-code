using System.Reflection;
using NUnit.Framework;

namespace AdventOfCode2015.Tests
{
    [TestFixture]
    public class Day02Tests
    {
        [TestCase(58, "2x3x4")]
        [TestCase(43, "1x1x10")]
        public void CalculateTotalSqFt_VariousSizedBoxes_CorrectArea(int expected, string input)
        {
            // arrange
            Day02 day02 = new Day02();
            
            // act
            var result = day02.CalculatePaperAndRibbonNeeded(new[] {input});
            
            // assert
            Assert.AreEqual(expected, result.Item1);
        }

        [TestCase(34, "2x3x4")]
        [TestCase(14, "1x1x10")]
        public void CalculateRibbonNeeded_VariousSizedBoxes_CorrectRibbonLength(int expected, string input)
        {
            // arrange
            Day02 day02 = new Day02();

            // act
            var result = day02.CalculatePaperAndRibbonNeeded(new[] { input });

            // assert
            Assert.AreEqual(expected, result.Item2);
        }

        [Test]
        public void CalculateTotalSqFtAndRibbon_OfficialInput_CorrectAreaAndRibbon()
        {
            // arrange
            List<string> inputs = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("AdventOfCode2015.Tests.Day02Input.txt")!)
            {
                using (StreamReader reader = new StreamReader(stream!))
                {
                    do
                    {
                        inputs.Add(reader.ReadLine()!);
                    }
                    while (reader.Peek() != -1);
                }
            }
            
            // act
            Day02 day02 = new Day02();
            var result = day02.CalculatePaperAndRibbonNeeded(inputs.ToArray());

            // assert
            Assert.AreEqual(1606483, result.Item1);
            Assert.AreEqual(3842356, result.Item2);
        }
    }
}
