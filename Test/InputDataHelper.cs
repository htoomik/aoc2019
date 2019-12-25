using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2019.Test
{
    public class InputDataHelper
    {
        public static List<string> GetLines(int day)
        {
            return File.ReadAllLines($"C:\\Code\\aoc2019\\Data\\input{day:00}.txt").ToList();
        }

        public static string Get(int day)
        {
            return File.ReadAllText($"C:\\Code\\aoc2019\\Data\\input{day:00}.txt").Replace("\n", "\r\n").Trim();
        }
    }
}