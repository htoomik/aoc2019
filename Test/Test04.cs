using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test04
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test04(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("111111", true)]
        [InlineData("223450", false)]
        [InlineData("123789", false)]
        public void Part1RuleTest(string value, bool matches)
        {
            var solver = new Day04();
            var matchesRules = solver.Matches1(value);
            matchesRules.ShouldBe(matches);
        }

        [Fact]
        public void Part1Solution()
        {
            var solver = new Day04();
            var answer = solver.Solve(256310, 732736);
            _testOutputHelper.WriteLine(answer.ToString());
        }

        [Theory]
        [InlineData("112233", true)]
        [InlineData("123444", false)]
        [InlineData("111122", true)]
        public void Part2RuleTest(string value, bool matches)
        {
            var solver = new Day04();
            var matchesRules = solver.Matches2(value);
            matchesRules.ShouldBe(matches);
        }

        [Fact]
        public void Part2Solution()
        {
            var solver = new Day04();
            var answer = solver.Solve2(256310, 732736);
            _testOutputHelper.WriteLine(answer.ToString());
        }
    }
}