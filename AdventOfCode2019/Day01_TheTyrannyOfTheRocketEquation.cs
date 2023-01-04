namespace AdventOfCode2019
{
    public static class Day01_TheTyrannyOfTheRocketEquation
    {
        public static int CalculateFuelForModule(int mass)
        {
            var alpha = mass / 3.0;
            var result = (int)alpha - 2;
            return result;
        }

        public static int CalculateFuelForModules(List<int> masses)
        {
            return masses.Sum(CalculateFuelForModule);
        }

        public static int CalculateFuelForModulesAndFuelForFuel(List<int> masses)
        {
            var result = 0;
            foreach (var mass in masses)
            {
                var fuelForMassOrFuel = CalculateFuelForModule(mass);
                result += fuelForMassOrFuel;
                while (fuelForMassOrFuel > 0)
                {
                    fuelForMassOrFuel = CalculateFuelForModule(fuelForMassOrFuel);
                    if (fuelForMassOrFuel > 0)
                    {
                        result += fuelForMassOrFuel;
                    }
                }
            }

            return result;
        }
    }
    
}

