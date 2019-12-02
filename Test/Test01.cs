using System;
using System.IO;
using System.Linq;
using System.Net;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test01
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test01(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void Part1(int input, int expected)
        {
            var solver = new Day01();
            var result = solver.Solve(input);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Part1Solution()
        {
            var solver = new Day01();
            var data = File.ReadAllLines("C:\\Code\\aoc2019\\Data\\input01_1.txt");
            var result = data.Sum(d => solver.Solve(int.Parse(d)));

            _testOutputHelper.WriteLine(result.ToString());
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void Part2(int input, int expected)
        {
            var solver = new Day01();
            var result = solver.Solve2(input);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Part2Solution()
        {
            var solver = new Day01();
            var data = File.ReadAllLines("C:\\Code\\aoc2019\\Data\\input01_1.txt");
            var result = data.Sum(d => solver.Solve2(int.Parse(d)));

            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}