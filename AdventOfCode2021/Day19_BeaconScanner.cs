namespace AdventOfCode2021
{
    public static class Day19_BeaconScanner
    {
        private class BeaconInfo
        {
            public int ScannerNumber { get; }
            public List<(int, int, int)> BeaconCoordinates { get; set; }
            public List<(int, int, string)> BeaconDistances { get; set; }

            public BeaconInfo(int scannerNumber)
            {
                ScannerNumber = scannerNumber;
                BeaconCoordinates = new List<(int, int, int)>();
                BeaconDistances = new List<(int, int, string)>();
            }

        }
        public static long DetermineBeaconCount(string[] input)
        {
            // Parse input into Scanner plus scanned beacon collections
            var scannerNumber = 0;
            var beaconsPerScanner = new List<BeaconInfo>();
            foreach(var line in input)
            {
                if (line.StartsWith("--- scanner "))
                {
                    scannerNumber = int.Parse(line.Replace("--- scanner ", "").Replace(" ---", ""));
                    beaconsPerScanner.Add(new BeaconInfo(scannerNumber));
                    continue;
                }

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var coordinates = line.Split(',').Select(x => int.Parse(x)).ToArray();
                beaconsPerScanner[scannerNumber].BeaconCoordinates.Add((coordinates[0], coordinates[1], coordinates[2]));
            }

            // For each Scanner's beacons, determine the distances between all
            for (int scannerIndex = 0; scannerIndex < beaconsPerScanner.Count; scannerIndex++)
            {
                BeaconInfo beaconInfo = beaconsPerScanner[scannerIndex];
                for (int i = 0; i < beaconInfo.BeaconCoordinates.Count; i++)
                {
                    (int x, int y, int z) a = beaconInfo.BeaconCoordinates[i];
                    for (int j = i; j < beaconInfo.BeaconCoordinates.Count; j++)
                    {
                        (int x, int y, int z) b = beaconInfo.BeaconCoordinates[i];
                        var dx = Math.Abs(a.x - b.x);
                        var dy = Math.Abs(a.y - b.y);
                        var dz = Math.Abs(a.z - b.z);
                        var offsetValues = new int[3] { dx, dy, dz };
                        var min = offsetValues.Min();
                        var max = offsetValues.Max();
                        var distance = dx*dx + dy*dy + dz*dz; // real distance would be the square root of this value
                        beaconInfo.BeaconDistances.Add((i, j, $"{distance}-{min}-{max}"));
                    }
                }
            }


            // Starting with scanner 0, see what other scanners has at least 12 of the same distances between beacons
            for (int scannerIndex = 0; scannerIndex < beaconsPerScanner.Count; scannerIndex++)
            {

            }

            // Determine that scanners location (relative to scanner 0)

            return 0;
        }
    }
}

