using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019
{
    public class Day16
    {
        public string Solve(string input, int phases)
        {
            var nums = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();

            for (var phase = 0; phase < phases; phase++)
            {
                var newNums = new List<int>();
                for (var position = 0; position < nums.Count; position++)
                {
                    var pattern = GetPattern(position, nums.Count);
                    var product = Multiply(nums, pattern);
                    newNums.Add(product);
                }

                nums = newNums;
            }

            return string.Join("", nums).Substring(0, 8);
        }

        public string Solve2(string input)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 1000; i++)
            {
                sb.Append(input);
            }

            var fullInput = sb.ToString();
            var output = Solve(fullInput, 100);

            var offset = int.Parse(input.Substring(0, 7));

            var result = output.Substring(offset, 8);

            return result;
        }

        public List<int> GetPattern(int position, int length)
        {
            var basePattern = new[] { 0, 1, 0, -1 };
            var positionAdjusted = new List<int>();

            var done = false;
            while (!done)
            {
                foreach (var t in basePattern)
                {
                    for (var j = 0; j <= position; j++)
                    {
                        positionAdjusted.Add(t);
                    }
                }

                if (positionAdjusted.Count > length)
                {
                    done = true;
                }
            }

            positionAdjusted.RemoveAt(0);

            return positionAdjusted.Take(length).ToList();
        }

        public int Multiply(List<int> a, List<int> b)
        {
            if (a.Count != b.Count)
            {
                throw new ArgumentException();
            }

            var sum = 0;
            for (var i = 0; i < a.Count; i++)
            {
                sum += a[i] * b[i];
            }

            return Math.Abs(sum) % 10;
        }
    }
}