using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day02
    {
        public int Solve(string input, Action<List<int>> modify = null)
        {
            var codes = input.Split(',').Select(int.Parse).ToList();

            modify?.Invoke(codes);

            var pos = 0;
            while (true)
            {
                var intCode = codes[pos];
                if (intCode == 1)
                {
                    var term1 = codes[codes[pos + 1]];
                    var term2 = codes[codes[pos + 2]];
                    var outPos = codes[pos + 3];
                    codes[outPos] = term1 + term2;
                }
                else if (intCode == 2)
                {
                    var term1 = codes[codes[pos + 1]];
                    var term2 = codes[codes[pos + 2]];
                    var outPos = codes[pos + 3];
                    codes[outPos] = term1 * term2;
                }
                else if (intCode == 99)
                {
                    break;
                }

                pos += 4;
            }

            return codes[0];
        }
    }
}