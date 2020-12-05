using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test15
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test15(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(15);
            var solver = new Day15();
            var result = solver.Solve(input);
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}