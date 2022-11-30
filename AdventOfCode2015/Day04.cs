using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2015
{
    public class Day04
    {
        public static int HashUntilSmallestIntIsFound(string key, bool use6ZeroPrefix = false)
        {
            MD5 hash = MD5.Create();

            int counter = 0;
            while(counter < int.MaxValue)
            {
                counter++;
                byte[] plainTextBytes = Encoding.UTF8.GetBytes($"{key}{counter}");

                // Compute hash value of our plain text  
                byte[] hashBytes = hash.ComputeHash(plainTextBytes);

                // Convert result into a hex string.
                string hashValue = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                string prefix = use6ZeroPrefix ? "000000" : "00000";
                if (hashValue.StartsWith(prefix))
                {
                    break;
                }
            }

            return counter;
        }
    }
}
