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

            var ints = string.Join(" - ", intersections.Select(i => i.ToString()));

            var nearest = intersections.Min(p => p.ManhattanDistance);
            return nearest;
        }

        private List<Point> GetIntersections(List<Point> wire1, List<Point> wire2)
        {
            return wire1
                .Intersect(wire2)
                .Where(p => p.ManhattanDistance != 0)
                .ToList();
        }

        private List<Point> Trace(string path)
        {
            var result = new List<Point>();
            var segments = path.Split(',');
            var current = new Point(0, 0);

            result.Add(current);
            foreach (var segment in segments)
            {
                var direction = segment[0];
                var steps = int.Parse(segment.Substring(1));
                var action = Movements[direction];
                for (var i = 0; i < steps; i++)
                {
                    current = action(current);
                    result.Add(current);
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

            public override string ToString()
            {
                return $"{X},{Y}";
            }
        }
    }
}