using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace aoc2019
{
    public class Day17
    {
        public int Calibrate(string input)
        {
            var intersections = FindIntersections(input);

            var sum = intersections.Sum(point => point.X * point.Y);

            return sum;
        }

        public List<Point> FindIntersections(string input)
        {
            var lines = input.Trim().Replace("\r\n", "\n").Split("\n");

            var result = new List<Point>();
            for (var y = 1; y < lines.Length - 1; y++)
            {
                for (var x = 1; x < lines[y].Length - 1; x++)
                {
                    var o = lines[y][x];
                    var n = lines[y - 1][x];
                    var s = lines[y + 1][x];
                    var e = lines[y][x + 1];
                    var w = lines[y][x - 1];

                    if (o == '#' &&
                        n == '#' &&
                        e == '#' &&
                        s == '#' &&
                        w == '#')
                    {
                        result.Add(new Point(x, y));
                    }
                }
            }

            return result.OrderBy(point => point.X).ThenBy(point => point.Y).ToList();
        }

        public string Solve(string code, string a, string b, string c, string p)
        {
            if (code[0] != '1')
            {
                throw new ArgumentException();
            }

            code = '2' + code.Substring(1);

            var computer = new IntCodeComputer(code);

            computer.AddInput(Translate(p));
            computer.AddInput(Translate(a));
            computer.AddInput(Translate(b));
            computer.AddInput(Translate(c));
            computer.AddInput(Translate("n"));
            computer.Run();

            var output = computer.Output();

            return output.Last().ToString();
        }

        public static long[] Translate(string s)
        {
            return (s + "\n").ToCharArray().Select(c => (long) c).ToArray();
        }

        public readonly struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}