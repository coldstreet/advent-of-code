using System.Diagnostics;
using System.Text;

namespace AdventOfCode2015
{
    public class Day03
    {
        private readonly Dictionary<RouteCoordinates, int> _routeHistory;

        public Day03(string directions, bool useRoboSanta = false)
        {
            var startingCoordinates = new RouteCoordinates(0, 0);
            Dictionary<RouteCoordinates, int> routeHistory = new Dictionary<RouteCoordinates, int>
            {
                {startingCoordinates, 1}
            };

            if (useRoboSanta)
            {
                // split directions
                Tuple<string, string> results = SplitDirections(directions);

                // carry out both
                int visits = routeHistory[startingCoordinates];
                routeHistory[startingCoordinates] = visits + 1;
                routeHistory = CarryOutDirections(results.Item1, routeHistory);
                _routeHistory = CarryOutDirections(results.Item2, routeHistory);
            }
            else
            {
                _routeHistory = CarryOutDirections(directions, routeHistory);
            }
        }

        public int HouseStops()
        {
            return _routeHistory.Count;
        }

        private Tuple<string, string> SplitDirections(string directions)
        {
            StringBuilder d1 = new StringBuilder();
            StringBuilder d2 = new StringBuilder();

            int counter = 0;
            foreach (char d in directions)
            {
                if (++counter % 2 == 0)
                {
                    d2.Append(d);
                }
                else
                {
                    d1.Append(d);
                }
            }

            return new Tuple<string, string>(d1.ToString(), d2.ToString());
        }

        private Dictionary<RouteCoordinates, int> CarryOutDirections(string directions, Dictionary<RouteCoordinates, int> routeHistory)
        {
            short x = 0;
            short y = 0;
            foreach (char d in directions)
            {
                switch (d)
                {
                    case '^':
                        y++;
                        break;
                    case '>':
                        x++;
                        break;
                    case 'v':
                        y--;
                        break;
                    case '<':
                        x--;
                        break;
                    default:
                        continue;
                }

                var routeCoordinates = new RouteCoordinates(x, y);
                if (routeHistory.ContainsKey(routeCoordinates))
                {
                    var visits = routeHistory[routeCoordinates];
                    routeHistory[routeCoordinates] = visits + 1;
                }
                else
                {
                    routeHistory.Add(routeCoordinates, 1);
                }
            }

            return routeHistory;
        }
        

        public class RouteCoordinates
        {
            private readonly short _x;
            private readonly short _y;
            private readonly int? _hashCode;

            public RouteCoordinates(short x, short y)
            {
                _x = x;
                _y = y;
                _hashCode = CreateHashCode(x, y);
            }

            public override int GetHashCode()
            {
                Debug.Assert(_hashCode != null, "_hashCode != null");

                return _hashCode.Value;
            }

            public override bool Equals(object? obj)
            {
                return Equals((obj as RouteCoordinates)!);
            }

            public bool Equals(RouteCoordinates rc)
            {
                return rc != null && rc._x == _x && rc._y == _y;
            }

            private static int CreateHashCode(short x, short y)
            {
                var a = (uint)(x >= 0 ? 2 * x : -2 * x - 1);
                var b = (uint)(y >= 0 ? 2 * y : -2 * y - 1);
                var c = (int)((a >= b ? a * a + a + b : a + b * b) / 2);

                return x < 0 && y < 0 || x >= 0 && y >= 0 ? c : -c - 1;
            }
        }
    }
}
