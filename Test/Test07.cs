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

        [Fact]
        public void Part2A()
        {
            const string instructions = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            var permutation = new List<int> { 9, 8, 7, 6, 5 };
            var solver = new Day07();
            var computers = solver.CreateComputers(instructions);
            var output = new Day07().GetOutputForPermutation(computers, permutation);
            output.ShouldBe(139629729);
        }

        [Theory]
        [InlineData("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", 139629729)]
        [InlineData("3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", 18216)]
        public void Part2(string instructions, int expected)
        {
            var output = new Day07().Solve2(instructions);
            output.ShouldBe(expected);
        }

        [Fact]
        public void Solve2()
        {
            var input = InputDataHelper.Get(7);
            var output = new Day07().Solve2(input);
            _testOutputHelper.WriteLine(output.ToString());
        }
    }
}