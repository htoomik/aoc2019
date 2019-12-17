using System.Collections.Generic;

namespace aoc2019
{
    public class Day05
    {
        public List<int> Solve(string instructions, int input)
        {
            return new IntCodeComputer().Run(instructions, input);
        }
    }
}