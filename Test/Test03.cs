using System;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test03
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test03(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void Part1(string w1, string w2, int expected)
        {
            var solver = new Day03();
            var result = solver.Solve(w1, w2);
            result.ShouldBe(expected);
        }

        [Fact]
        public void Part1Solution()
        {
            var lines = InputDataHelper.GetLines(3);
            var solver = new Day03();
            var result = solver.Solve(lines[0], lines[1]);
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}