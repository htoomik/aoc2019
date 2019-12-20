using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test11
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test11(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Part1()
        {
            var instructions = new[] { (1, 0), (0, 0), (1, 0), (1, 0), (0, 1), (1, 0), (1, 0) };
            var solver = new Day11();
            var result = solver.Run(instructions);
            result.ShouldBe(6);
        }

        [Fact]
        public void Solve()
        {
            var code = InputDataHelper.Get(11);
            var solver = new Day11();
            var result = solver.Solve(code, 0);
            _testOutputHelper.WriteLine(result.Count.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var code = InputDataHelper.Get(11);
            var solver = new Day11();
            var result = solver.Solve(code, 1);

            var standardized = solver.Standardize(result);
            _testOutputHelper.WriteLine(standardized);
        }
    }
}