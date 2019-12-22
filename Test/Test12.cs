using System;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test12
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test12(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string Raw = @"
<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

        private static string Input => Raw.Trim();

        [Fact]
        public void Parse()
        {
            var solver = new Day12();
            var moons = solver.Parse(Input);

            moons.Count.ShouldBe(4);
            moons[0].X.ShouldBe(-1);
            moons[1].Y.ShouldBe(-10);
            moons[2].Z.ShouldBe(8);
        }

        [Fact]
        public void ApplyGravity()
        {
            var solver = new Day12();
            var moons = solver.Parse(Input);
            moons = solver.ApplyGravity(moons);

            moons[0].ToString().ShouldBe("pos=<x= 2, y=-1, z= 1>, vel=<x= 3, y=-1, z=-1>");
        }

        [Fact]
        public void Energy()
        {
            const string input = @"
pos=<x= 2, y= 1, z=-3>, vel=<x=-3, y=-2, z= 1>
pos=<x= 1, y=-8, z= 0>, vel=<x=-1, y= 1, z= 3>
pos=<x= 3, y=-6, z= 1>, vel=<x= 3, y= 2, z=-3>
pos=<x= 2, y= 0, z= 4>, vel=<x= 1, y=-1, z=-1>";
            var solver = new Day12();
            var moons = solver.Parse2(input.Trim());
            moons.Sum(m => m.Energy()).ShouldBe(179);
        }

        [Fact]
        public void Part1()
        {
            var solver = new Day12();
            var result = solver.Solve(Input, 10);
            result.ShouldBe(179);
        }

        [Fact]
        public void Solve()
        {
            var input = InputDataHelper.Get(12).Trim();
            var solver = new Day12();
            var result = solver.Solve(input, 1000);
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            var solver = new Day12();
            var result = solver.Solve2(Input, _testOutputHelper);
            result.ShouldBe(2772);
        }

        [Fact]
        public void Part2B()
        {
            var input = @"
<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>".Trim();
            var solver = new Day12();
            var result = solver.Solve2(input, _testOutputHelper);
            result.ShouldBe(4686774924);
        }

        [Fact]
        public void Solve2()
        {
            var input = InputDataHelper.Get(12).Trim();
            var solver = new Day12();
            var result = solver.Solve2(input, _testOutputHelper);
            _testOutputHelper.WriteLine(result.ToString());

            // too low: 5607666906324
            // trying: 307043147758488
            // too high: 1465099063465813344

        }
    }
}