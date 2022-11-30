using System.Reflection;

namespace AdventOfCode2018.Tests.Helpers
{
    public class EmbeddedResource
    {
        public static string[] ReadFile(string fileName)
        {
            List<string> lines = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                if (stream == null)
                {
                    return lines.ToArray();
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() >= 0)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
            }

            return lines.ToArray();
        }
    }
}
