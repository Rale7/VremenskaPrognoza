using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VremenskaPrognoza.APICalling
{
    internal static class EnvReader
    {
        public static void LoadEnvironmentVariables(String filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue; // Skip empty lines and comments

                var parts = line.Split('=', 2);
                if (parts.Length != 2)
                    continue; // Skip lines that are not key-value pairs

                var key = parts[0].Trim();
                var value = parts[1].Trim();
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}
