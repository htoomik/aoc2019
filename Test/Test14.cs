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
            var result = solver.Solve(input, 1);
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
            var result = solver.Solve(input, 1);
            result.ShouldBe(165);
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(14);
            var solver = new Day14();
            var result = solver.Solve(input, 1);
            _outputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2A()
        {
            var input = @"
157 ORE => 5 NZVS
165 ORE => 6 DCFZ
44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
179 ORE => 7 PSHF
177 ORE => 5 HKGWZ
7 DCFZ, 7 PSHF => 2 XJWVT
165 ORE => 2 GPVTF
3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT".Trim();
            var solver = new Day14();
            var result = solver.Solve2(input);
            result.ShouldBe(82892753);
        }

        [Fact]
        public void Solve2()
        {
            var input = InputDataHelper.Get(14);
            var solver = new Day14();
            var result = solver.Solve2(input);
            _outputHelper.WriteLine(result.ToString());
        }
    }
}