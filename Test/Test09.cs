using System;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace aoc2019.Test
{
    public class Test09
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test09(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Part1A()
        {
            const string instructions = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
            var computer = new IntCodeComputer(instructions);
            var output = computer.Run().Output();
            var expected = instructions.Split(',').Select(long.Parse);
            output.ShouldBe(expected);
        }

        [Fact]
        public void Part1B()
        {
            const string instructions = "1102,34915192,34915192,7,4,7,99,0";
            var computer = new IntCodeComputer(instructions);
            var output = computer.Run().Output();
            output.Single().ToString().Length.ShouldBe(16);
        }

        [Fact]
        public void Part1C()
        {
            const string instructions = "104,1125899906842624,99";
            var computer = new IntCodeComputer(instructions);
            var output = computer.Run().Output();
            output.Single().ToString().ShouldBe(instructions.Split(",")[1]);
        }

        [Theory]
        [InlineData("109, -1, 4, 1, 99", -1)]
        [InlineData("109, -1, 104, 1, 99", 1)]
        [InlineData("109, -1, 204, 1, 99", 109)]
        [InlineData("109, 1, 9, 2, 204, -6, 99", 204)]
        [InlineData("109, 1, 3, 3, 204, 2, 99", 11, 11)]
        [InlineData("109, 1, 203, 2, 204, 2, 99", 11, 11)]
        public void Part1Reddit(string instructions, int expected, int input = 0)
        {
            var computer = new IntCodeComputer(instructions);
            var output = computer.AddInput(input).Run().Output();
            output.Single().ShouldBe(expected);
        }

        [Fact]
        public void Solve()
        {
            var instructions = InputDataHelper.Get(9).Trim();
            var computer = new IntCodeComputer(instructions);
            var output = computer.AddInput(1).Run().Output();
            _testOutputHelper.WriteLine(string.Join(",", output));
        }

        [Fact]
        public void Solve2()
        {
            var instructions = InputDataHelper.Get(9).Trim();
            var computer = new IntCodeComputer(instructions);
            var output = computer.AddInput(2).Run().Output();
            _testOutputHelper.WriteLine(string.Join(",", output));
        }
    }
}