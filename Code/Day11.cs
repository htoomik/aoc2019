using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc2019
{
    public class Day11
    {
        public int Run((int, int)[] instructions)
        {
            var squares = new Dictionary<Coords, int>();

            var x = 0;
            var y = 0;
            var d = D.North;

            foreach (var (color, turnDirection) in instructions)
            {
                var coords = new Coords(x, y);
                squares[coords] = color;
                d = Turn(d, turnDirection);
                (x, y) = Move(x, y, d);
            }

            return squares.Count;
        }

        public Dictionary<Coords, int> Solve(string code, int firstColor)
        {
            var squares = new Dictionary<Coords, int>();

            var x = 0;
            var y = 0;
            var d = D.North;

            var computer = new IntCodeComputer(code);
            computer.AddInput(firstColor);

            var outputPos = 0;
            while (!computer.Halted)
            {
                computer.Run();
                var output = computer.Output();
                var color = (int)output[outputPos++];
                var turnDirection = (int)output[outputPos++];

                var coords = new Coords(x, y);
                squares[coords] = color;
                d = Turn(d, turnDirection);
                (x, y) = Move(x, y, d);

                var colorHere = 0;
                if (squares.ContainsKey(new Coords(x, y)))
                {
                    colorHere = squares[new Coords(x, y)];
                }

                computer.AddInput(colorHere);
            }

            return squares;
        }

        private D Turn(D current, int turnDirection)
        {
            var newD = turnDirection == 0 ? current - 1 : current + 1;

            if (newD == D.WestAgain)
                newD = D.West;
            if (newD == D.NorthAgain)
                newD = D.North;

            return newD;
        }

        private (int, int) Move(int x, int y, D d)
        {
            switch (d)
            {
                case D.North: return (x, y - 1);
                case D.East: return (x + 1, y);
                case D.South: return (x, y + 1);
                case D.West: return (x - 1, y);
                default: throw new Exception("Unexpected direction " + d);
            }
        }

        [DebuggerDisplay("X {X}, Y {Y}")]
        public struct Coords
        {
            public int X { get; }
            public int Y { get; }

            public Coords(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"X {X}, Y {Y}";
            }
        }

        private enum D
        {
            WestAgain,
            North,
            East,
            South,
            West,
            NorthAgain
        }

        public string Standardize(Dictionary<Coords,int> squares)
        {
            var minX = squares.Min(s => s.Key.X);
            var minY = squares.Min(s => s.Key.Y);
            var maxX = squares.Max(s => s.Key.X);
            var maxY = squares.Max(s => s.Key.Y);

            var xRange = maxX - minX + 1;
            var yRange = maxY - minY + 1;
            var grid = new int[xRange, yRange];

            for (int y = 0; y < yRange; y++)
            {
                var adjustedY = y + minY;
                for (int x = 0; x < xRange; x++)
                {
                    var adjustedX = x + minX;
                    int c;
                    var coords = new Coords(adjustedX, adjustedY);
                    if (squares.ContainsKey(coords))
                        c = squares[coords];
                    else
                        c = 0;

                    grid[x, y] = c;
                }
            }

            var result = string.Empty;
            for (int y = 0; y < yRange; y++)
            {
                for (int x = 0; x < xRange; x++)
                {
                    var number = grid[x, y];
                    result += number == 1 ? "#" : ".";
                }

                result += "\r\n";
            }

            return result.Trim();
        }
    }
}