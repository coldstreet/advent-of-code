using System.Reflection;
using NUnit.Framework;

namespace AdventOfCode2015.Tests
{
    [TestFixture]
    public class Day06Tests
    {
        [TestCase("turn on", 0, 0, 999, 999, 1000000, false)]
        [TestCase("turn on", 0, 0, 999, 0, 1000, false)]
        [TestCase("toggle", 499, 499, 500, 500, 4, false)]
        [TestCase("turn on", 0, 0, 0, 0, 1, true)]
        [TestCase("toggle", 0, 0, 999, 999, 2000000, true)]
        public void ChangeStateOfLights_VariousScenarios_CorrectLightCountIsMet(string command, int fromX, int fromY, int toX, int toY, int expected, bool useNewLightLevel)
        {
            // arrange
            Day06.LightManager lightManager = new Day06.LightManager();

            // act
            lightManager.ChangeStateOfLight(command, fromX, fromY, toX, toY, useNewLightLevel);
            var result = lightManager.MeasureLightLevel();
            
            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ChangeStateOfLights_OfficialInput_CorrectLightCountIsMet()
        {
            // arrange
            var commands = ExtractLightCommandTestInputs();

            // act
            Day06.LightManager lightManager = new Day06.LightManager();
            lightManager.ProcessListOfCommands(commands.ToArray());
            var result = lightManager.MeasureLightLevel();

            // assert
            Assert.AreEqual(543903, result);
        }

        [Test]
        public void ChangeStateOfLights_OfficialInputUsingNewLightLevel_CorrectLightCountIsMet()
        {
            // arrange
            var commands = ExtractLightCommandTestInputs();

            // act
            Day06.LightManager lightManager = new Day06.LightManager();
            lightManager.ProcessListOfCommands(commands.ToArray(), true);
            var result = lightManager.MeasureLightLevel();

            // assert
            Assert.AreEqual(14687245, result);
        }

        private static List<Day06.LightCommand> ExtractLightCommandTestInputs()
        {
            List<Day06.LightCommand> commands = new List<Day06.LightCommand>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("AdventOfCode2015.Tests.Day06Input.txt")!)
            {
                using (StreamReader reader = new StreamReader(stream!))
                {
                    do
                    {
                        string instructions = reader.ReadLine()!;
                        if (instructions == null)
                        {
                            continue;
                        }

                        string[] commandAttributes = instructions.Split(',', ' ');
                        if (commandAttributes.Length == 7)
                        {
                            Day06.LightCommand command = new Day06.LightCommand(
                               $"{commandAttributes[0]} {commandAttributes[1]}", 
                               int.Parse(commandAttributes[2]),
                               int.Parse(commandAttributes[3]), 
                               int.Parse(commandAttributes[5]), 
                               int.Parse(commandAttributes[6]));
                            commands.Add(command);
                        }
                        else if (commandAttributes.Length == 6)
                        {
                            Day06.LightCommand command = new Day06.LightCommand(
                              $"{commandAttributes[0]}",
                              int.Parse(commandAttributes[1]),
                              int.Parse(commandAttributes[2]),
                              int.Parse(commandAttributes[4]),
                              int.Parse(commandAttributes[5]));
                            commands.Add(command);
                        }
                    } while (reader.Peek() != -1);
                }
            }
            return commands;
        }
    }
}
