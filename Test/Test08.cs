using System;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test08
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test08(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Part1()
        {
            const string input = "123456789012";
            var result = new Day08().Solve(input, 3, 2);
            result.ShouldBe(1);
        }

        [Fact]
        public void Solve1()
        {
            var input = InputDataHelper.Get(8).Trim();
            var result = new Day08().Solve(input, 25, 6);
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            const string input = "0222112222120000";
            var result = new Day08().Solve2(input, 2, 2);
            result.ShouldBe("01\r\n10");
        }


        [Fact]
        public void Solve2()
        {
            var input = InputDataHelper.Get(8).Trim();
            var result = new Day08().Solve2(input, 25, 6);
            _testOutputHelper.WriteLine(result.Replace('0', ' '));
        }
    }
}