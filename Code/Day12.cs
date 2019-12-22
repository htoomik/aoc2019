using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2019
{
    public class Day12
    {
        public int Solve(string input, int i)
        {
            var moons = Parse(input);
            for (var j = 0; j < i; j++)
            {
                moons = ApplyGravity(moons);
            }

            return moons.Sum(m => m.Energy());
        }

        public List<Moon> Parse(string input)
        {
            var lines = input.Split("\n");
            var moons = new List<Moon>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line.Trim(), "<x=(-?\\d+), y=(-?\\d+), z=(-?\\d+)>");
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                var z = int.Parse(match.Groups[3].Value);
                moons.Add(new Moon(x, y, z));
            }

            return moons;
        }

        public List<Moon> Parse2(string input)
        {
            var lines = input.Split("\r\n");
            var moons = new List<Moon>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line, "pos=<x=\\s?(-?\\d+), y=\\s?(-?\\d+), z=\\s?(-?\\d+)>, vel=<x=\\s?(-?\\d+), y=\\s?(-?\\d+), z=\\s?(-?\\d+)>");
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                var z = int.Parse(match.Groups[3].Value);
                var vx = int.Parse(match.Groups[4].Value);
                var vy = int.Parse(match.Groups[5].Value);
                var vz = int.Parse(match.Groups[6].Value);
                moons.Add(new Moon(x, y, z, vx, vy, vz));
            }

            return moons;
        }

        public List<Moon> ApplyGravity(List<Moon> moons)
        {
            var newMoons = new List<Moon>();
            foreach (var moon in moons)
            {
                var newVx = moon.Vx;
                var newVy = moon.Vy;
                var newVz = moon.Vz;
                foreach (var otherMoon in moons.Where(m => !m.Equals(moon)))
                {
                    newVx += Math.Sign(otherMoon.X - moon.X);
                    newVy += Math.Sign(otherMoon.Y - moon.Y);
                    newVz += Math.Sign(otherMoon.Z - moon.Z);
                }
                newMoons.Add(new Moon(moon.X, moon.Y, moon.Z, newVx, newVy, newVz));
            }

            return newMoons.Select(m => m.Move()).ToList();
        }

        public struct Moon
        {
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public int Vx { get; }
            public int Vy { get; }
            public int Vz { get; }

            public Moon(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;

                Vx = 0;
                Vy = 0;
                Vz = 0;
            }

            public Moon(int x, int y, int z, int vx, int vy, int vz)
            {
                X = x;
                Y = y;
                Z = z;
                Vx = vx;
                Vy = vy;
                Vz = vz;
            }

            public Moon Move()
            {
                return new Moon( X + Vx, Y + Vy, Z + Vz, Vx, Vy, Vz);
            }

            public int Energy()
            {
                var potential = A(X) + A(Y) + A(Z);
                var kinetic = A(Vx) + A(Vy) + A(Vz);
                return potential * kinetic;
            }

            private int A(int a)
            {
                return Math.Abs(a);
            }

            public override string ToString()
            {
                return $"pos=<x={F(X)}, y={F(Y)}, z={F(Z)}>, vel=<x={F(Vx)}, y={F(Vy)}, z={F(Vz)}>";
            }

            private static string F(int i)
            {
                return i.ToString(" 0;-#");
            }

            public string Hash()
            {
                return $"{X},{Y},{Z},{Vx},{Vy},{Vz}";
            }
        }
    }
}