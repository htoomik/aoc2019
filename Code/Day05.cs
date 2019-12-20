using System.Collections.Generic;

namespace aoc2019
{
    public class Day05
    {
        public IEnumerable<long> Solve(string instructions, int input)
        {
            return new IntCodeComputer(instructions)
                .AddInput(input)
                .Run()
                .Output();
        }
    }
}