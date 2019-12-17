using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test07
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test07(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Permutations()
        {
            var nums = new List<int> { 1, 2, 3, 4 };
            const int expected = 24;
            var permutations = Day07.Permute(nums);
            permutations.Count().ShouldBe(expected);
        }

        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 43210)]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", 54321)]
        [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 65210)]
        public void Part1(string instructions, int expected)
        {
            var output = new Day07().Solve(instructions);
            output.ShouldBe(expected);
        }

        [Fact]
        public void Solve1()
        {
            var input = InputDataHelper.Get(7);
            var output = new Day07().Solve(input);
            _testOutputHelper.WriteLine(output.ToString());
        }
    }
}