using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test06
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test06(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Part1()
        {
            var data = @"
COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L".Trim().Split("\r\n").ToList();
            var solver = new Day06();
            var result = solver.Solve(data);
            result.ShouldBe(42);
        }

        [Fact]
        public void Solve1()
        {
            var data = InputDataHelper.GetLines(6);
            var solver = new Day06();
            var result = solver.Solve(data);
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Part2()
        {
            var data = @"
COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
K)YOU
I)SAN".Trim().Split("\r\n").ToList();
            var solver = new Day06();
            var result = solver.Solve2(data);
            result.ShouldBe(4);
        }

        [Fact]
        public void Solve2()
        {
            var data = InputDataHelper.GetLines(6);
            var solver = new Day06();
            var result = solver.Solve2(data);
            _testOutputHelper.WriteLine(result.ToString());
        }

    }
}