using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test14
    {
        private readonly ITestOutputHelper _outputHelper;

        public Test14(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Part1A()
        {
            var input = @"
10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL".Trim();
            var solver = new Day14();
            var result = solver.Solve(input);
            result.ShouldBe(31);
        }

        [Fact]
        public void Part1B()
        {
            var input = @"
9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL".Trim();
            var solver = new Day14();
            var result = solver.Solve(input);
            result.ShouldBe(165);
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(14);
            var solver = new Day14();
            var result = solver.Solve(input);
            _outputHelper.WriteLine(result.ToString());
        }
    }
}