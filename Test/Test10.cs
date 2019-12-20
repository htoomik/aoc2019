using System;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test10
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test10(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string Map = @"
.#..#
.....
#####
....#
...##";

        [Fact]
        public void Parsing()
        {
            var asteroids = Day10.Parse(Map);
            asteroids.Count.ShouldBe(10);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(4, 2)]
        [InlineData(4, 0)]
        [InlineData(4, 4)]
        public void CanView(int x, int y)
        {
            var map = Day10.Parse(Map);
            map.SetStationAt(1, 0);
            map.StationCanView(x, y).canView.ShouldBeTrue();
        }


        [Theory]
        [InlineData(3, 4, 2, 2)]
        [InlineData(4, 3, 3, 2)]
        public void CannotView(int x, int y, int blockerX, int blockerY)
        {
            var map = Day10.Parse(Map);
            map.SetStationAt(1, 0);

            var blocked = map.StationCanView(x, y);
            blocked.canView.ShouldBeFalse();
            blocked.blocker.X.ShouldBe(blockerX);
            blocked.blocker.Y.ShouldBe(blockerY);
        }

        [Theory]
        [InlineData(3, 1, 6, 2)]
        public void Blocking(int blockerX, int blockerY, int targetX, int targetY)
        {
            const string input = @"
#.........
...A......
...B..a...
.EDCG....a
..F.c.b...
.....c....
..efd.c.gb
.......c..
....f...c.
...e..d..c";
            var map = Day10.Parse(input);
            var blocker = map.Find(blockerX, blockerY);
            var target = map.Find(targetX, targetY);

            map.SetStationAt(0, 0);
            map.StationCanView(blocker).canView.ShouldBeTrue("Cannot see blocker");

            map.StationCanView(target).canView.ShouldBeFalse("Blocker does not block");

            map.Remove(blocker);
            map.StationCanView(target).canView.ShouldBeTrue("Cannot see target after removing blocker");
        }

        [Theory]
        [InlineData(1, 0, 7)]
        public void TotalAsteroidsVisible(int x, int y, int expected)
        {
            var map = Day10.Parse(Map);
            map.SetStationAt(x, y);
            map.GetAsteroidsStationCanView().Count.ShouldBe(expected);
        }

        [Fact]
        public void Part1A()
        {
            var result = Day10.Solve(Map);
            result.count.ShouldBe(8);
        }

        [Fact]
        public void Part1B()
        {
            const string input = @"
......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            var map = Day10.Parse(input);
            map.SetStationAt(5, 8);
            map.GetAsteroidsStationCanView().Count.ShouldBe(33);

            var result = Day10.Solve(input);
            result.count.ShouldBe(33);
        }

        [Theory]
        [InlineData(6, 0)]
        [InlineData(8, 0)]
        [InlineData(0, 1)]
        [InlineData(3, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(4, 2)]
        [InlineData(6, 2)]
        [InlineData(7, 2)]
        [InlineData(8, 2)]
        [InlineData(1, 3)]
        [InlineData(3, 3)]
        [InlineData(5, 3)]
        [InlineData(6, 3)]
        [InlineData(7, 3)]
        [InlineData(4, 4)]
        [InlineData(7, 5)]
        [InlineData(9, 5)]
        [InlineData(0, 6)]
        [InlineData(8, 6)]
        [InlineData(1, 7)]
        [InlineData(2, 7)]
        [InlineData(4, 7)]
        [InlineData(7, 7)]
        [InlineData(8, 7)]
        [InlineData(9, 7)]
        [InlineData(1, 8)]
        [InlineData(8, 8)]
        [InlineData(1, 9)]
        [InlineData(6, 9)]
        [InlineData(7, 9)]
        [InlineData(8, 9)]
        [InlineData(9, 9)]
        public void Part1BCanView(int x, int y)
        {
            const string input = @"
......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            var map = Day10.Parse(input);
            map.SetStationAt(5, 8);
            map.StationCanView(x, y).canView.ShouldBeTrue();
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 2)]
        [InlineData(1, 4)]
        [InlineData(2, 5)]
        [InlineData(3, 6)]
        [InlineData(0, 8)]
        public void Part1BCannotView(int x, int y)
        {
            const string input = @"
......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            var map = Day10.Parse(input);
            map.SetStationAt(5, 8);
            map.StationCanView(x, y).canView.ShouldBeFalse();
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(10).Replace("\n", "\r\n");
            var result = Day10.Solve(input);

            _testOutputHelper.WriteLine(result.ToString());
        }

        [Theory]
        [InlineData(3, 2, 0)]
        [InlineData(3, 3, 0.25)]
        [InlineData(2, 3, 0.5)]
        [InlineData(1, 3, 0.75)]
        [InlineData(1, 2, 1.0)]
        [InlineData(1, 1, 1.25)]
        [InlineData(2, 1, 1.5)]
        [InlineData(3, 1, 1.75)]
        public void Angle(int x, int y, double expected)
        {
            var station = new Asteroid(2, 2);
            var asteroid = new Asteroid(x, y);
            (asteroid.AngleTo(station) / Math.PI).ShouldBe(expected);
        }

        [Theory]
        [InlineData(1, 8, 1)]
        [InlineData(2, 9, 0)]
        [InlineData(3, 9, 1)]
        [InlineData(4, 10, 0)]
        [InlineData(5, 9, 2)]
        [InlineData(6, 11, 1)]
        [InlineData(7, 12, 1)]
        [InlineData(8, 11, 2)]
        [InlineData(9, 15, 1)]
        [InlineData(18, 4, 4)]
        public void Part2(int i, int x, int y)
        {
            const string input = @"
.#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....X...###..
..#.#.....#....##";
            var result = Day10.Solve2(input, 8, 3, i);
            result.X.ShouldBe(x);
            result.Y.ShouldBe(y);
        }

        [Fact]
        public void Solve2()
        {
            var input = InputDataHelper.Get(10).Replace("\n", "\r\n");
            var result = Day10.Solve2(input, 17, 22, 200);
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}