using System;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test17
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test17(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FindIntersections()
        {
            var input = @"
..#..........
..#..........
#######...###
#.#...#...#.#
#############
..#...#...#..
..#####...^..".Trim();

            var solver = new Day17();
            var intersections = solver.FindIntersections(input);

            intersections.Count.ShouldBe(4);

            intersections[0].X.ShouldBe(2);
            intersections[0].Y.ShouldBe(2);

            intersections[1].X.ShouldBe(2);
            intersections[1].Y.ShouldBe(4);
        }

        [Fact]
        public void Calibrate()
        {
            var input = @"
..#..........
..#..........
#######...###
#.#...#...#.#
#############
..#...#...#..
..#####...^..".Trim();

            var solver = new Day17();
            var result = solver.Calibrate(input);

            result.ShouldBe(76);
        }

        [Fact]
        public void Solve()
        {
            var code = InputDataHelper.Get(17);
            var computer = new IntCodeComputer(code);
            computer.Run();

            var output = computer.Output();
            var input = new string(output.Select(l => (char) l).ToArray());

            _testOutputHelper.WriteLine(input);

            var solver = new Day17();
            var result = solver.Calibrate(input);
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Theory]
        [InlineData("L,6,L,2", "76, 44, 54, 44, 76, 44, 50, 10")]
        public void Translate(string input, string expected)
        {
            var actual = Day17.Translate(input);
            var comp = string.Join(", ", actual);
            comp.ShouldBe(expected);
        }

        [Fact]
        public void Part2()
        {
            var code = InputDataHelper.Get(17);
            const string a = "R,8,L,12,R,8";
            const string b = "R,12,L,8,R,10";
            const string c = "R,8,L,8,L,8,R,8,R,10";
            const string p = "A,B,B,A,C,A,A,C,B,C";

            var solver = new Day17();
            var result = solver.Solve(code, a, b, c, p);

            _testOutputHelper.WriteLine(result);
        }
    }
}