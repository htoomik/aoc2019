using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test13
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test13(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(13);
            var solver = new Day13();
            var tiles = solver.Solve(input);

            _testOutputHelper.WriteLine(tiles.ToString());

            var result = tiles.BlocksRemaining;
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Solve2()
        {
            var input = InputDataHelper.Get(13);
            var solver = new Day13();
            var score = solver.Solve2(input);
            _testOutputHelper.WriteLine(score.ToString());
        }
    }
}