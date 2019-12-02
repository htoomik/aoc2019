using System;

namespace aoc2019
{
    public class Day01
    {
        public int Solve(int input)
        {
            return (int)Math.Floor((double) (input / 3)) - 2;
        }

        public int Solve2(int input)
        {
            var totalFuel = 0;
            var fuel = Solve(input);
            while (fuel > 0)
            {
                totalFuel += fuel;
                fuel = Solve(fuel);
            }

            return totalFuel;
        }
    }
}