using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day03
    {
        public int Solve(string w1, string w2)
        {
            var wire1 = Trace(w1);
            var wire2 = Trace(w2);
            var intersections = GetIntersections(wire1, wire2);

            return intersections.Min(p => p.ManhattanDistance);
        }

        public int Solve2(string w1, string w2)
        {
            var wire1 = Trace(w1);
            var wire2 = Trace(w2);
            var intersections = GetIntersections(wire1, wire2);

            return intersections.Min(p => wire1[p] + wire2[p]);
        }

        private static IEnumerable<Point> GetIntersections(Dictionary<Point, int> wire1, Dictionary<Point, int> wire2)
        {
            return wire1.Keys.Where(wire2.ContainsKey);
        }

        private Dictionary<Point, int> Trace(string path)
        {
            var result = new Dictionary<Point, int>();
            var segments = path.Split(',');
            var current = new Point(0, 0);
            var totalSteps = 0;

            foreach (var segment in segments)
            {
                var direction = segment[0];
                var steps = int.Parse(segment.Substring(1));
                for (var i = 0; i < steps; i++)
                {
                    current = Movements[direction](current);
                    if (!result.ContainsKey(current))
                    {
                        result.Add(current, ++totalSteps);
                    }
                }
            }

            return result;
        }

        private static readonly Dictionary<char, Func<Point, Point>> Movements =
            new Dictionary<char, Func<Point, Point>>
            {
                { 'D', point => new Point(point.X, point.Y - 1) },
                { 'U', point => new Point(point.X, point.Y + 1) },
                { 'L', point => new Point(point.X - 1, point.Y) },
                { 'R', point => new Point(point.X + 1, point.Y) }
            };

        private struct Point
        {
            public readonly int X;
            public readonly int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);
        }
    }
}