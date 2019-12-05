using System;
using System.IO;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test02
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test02(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", 3500)]
        [InlineData("1,0,0,0,99", 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 30)]
        public void Part1(string input, int expected)
        {
            var solver = new Day02();
            var result = solver.Solve(input);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Part1Solution()
        {
            var data = InputDataHelper.Get(2);
            var solver = new Day02();

            var result = solver.Solve(data, input =>
            {
                input[1] = 12;
                input[2] = 2;
            });

            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2Solution()
        {
            var data = InputDataHelper.Get(2);

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var noun = i;
                    var verb = j;
                    var result = new Day02().Solve(data, input =>
                    {
                        input[1] = noun;
                        input[2] = verb;
                    });

                    if (result == 19690720)
                    {
                        _testOutputHelper.WriteLine($"noun: {i}, verb: {j}, final result: {100 * noun + verb}");
                    }
                }
            }
        }
    }
}