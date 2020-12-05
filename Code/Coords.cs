using System.Diagnostics;

namespace aoc2019
{
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
}