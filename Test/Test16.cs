using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test16
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test16(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(0, "1,0,-1,0,1,0,-1,0")]
        [InlineData(1, "0,1,1,0,0,-1,-1,0")]
        [InlineData(2, "0,0,1,1,1,0,0,0")]
        [InlineData(3, "0,0,0,1,1,1,1,0")]
        public void GetPattern(int position, string expected)
        {
            var solver = new Day16();
            var pattern = solver.GetPattern(position, 8);
            var actual = string.Join(",", pattern);
            actual.ShouldBe(expected);
        }

        [Fact]
        public void Multiply()
        {
            var solver = new Day16();
            var a = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var b = new List<int> { 1, 0, -1, 0, 1, 0, -1, 0 };
            var product = solver.Multiply(a, b);
            product.ShouldBe(4);
        }

        [Theory]
        [InlineData("12345678", 1, "48226158")]
        [InlineData("12345678", 2, "34040438")]
        [InlineData("80871224585914546619083218645595", 100, "24176176")]
        public void Part1(string input, int phases, string expected)
        {
            var solver = new Day16();
            var result = solver.Solve(input, phases);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(16);
            var solver = new Day16();
            var result = solver.Solve(input, 100);

            _testOutputHelper.WriteLine(result);
        }

        [Theory(Skip = "too slow")]
        [InlineData("03036732577212944063491565474664", "84462026")]
        public void Part2(string input, string expected)
        {
            var solver = new Day16();
            var answer = solver.Solve2(input);
            answer.ShouldBe(expected);
        }

        [Fact(Skip = "too slow")]
        public void Solve2()
        {
            var input = InputDataHelper.Get(16);
            var solver = new Day16();
        }
    }
}