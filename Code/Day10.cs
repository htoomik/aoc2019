using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc2019
{
    public class Day10
    {
        public static int Solve(string input)
        {
            var map = Parse(input);

            var bestTotal = 0;
            foreach (var potentialStation in map.Asteroids)
            {
                map.SetStationAt(potentialStation);
                var visibleAsteroids = map.GetAsteroidsStationCanView();
                if (visibleAsteroids.Count > bestTotal)
                    bestTotal = visibleAsteroids.Count;
            }

            return bestTotal;
        }

        public static Map Parse(string input)
        {
            var asteroids = new List<Asteroid>();
            var lines = input.Trim().Split("\r\n");
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if(lines[y][x] != '.')
                        asteroids.Add(new Asteroid(x, y));
                }
            }

            return new Map(asteroids);
        }
    }

    [DebuggerDisplay("{X}, {Y}")]
    public class Asteroid
    {
        public int X { get; }
        public int Y { get; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Map
    {
        public List<Asteroid> Asteroids { get; }
        public int Count => Asteroids.Count;
        private Asteroid _station;

        public Map(List<Asteroid> asteroids)
        {
            Asteroids = asteroids;
        }

        public Asteroid Find(int x, int y)
        {
            return Asteroids.Single(a => a.X == x && a.Y == y);
        }

        public void SetStationAt(int x, int y)
        {
            SetStationAt(Find(x, y));
        }

        public void SetStationAt(Asteroid asteroid)
        {
            _station = asteroid;
        }

        public void Remove(Asteroid asteroid)
        {
            Asteroids.Remove(asteroid);
        }

        public (bool canView, Asteroid blocker) StationCanView(int x, int y)
        {
            return StationCanView(Find(x, y));
        }

        public (bool canView, Asteroid blocker) StationCanView(Asteroid target)
        {
            foreach (var blocker in Asteroids)
            {
                if (blocker == target)
                    continue;
                if (blocker == _station)
                    continue;

                var targetXDistance = target.X - _station.X;
                var targetYDistance = target.Y - _station.Y;

                var blockerXDistance = blocker.X - _station.X;
                var blockerYDistance = blocker.Y - _station.Y;

                if (targetXDistance == 0 || blockerXDistance == 0)
                {
                    if (targetXDistance == blockerXDistance &&
                        Math.Sign(targetYDistance) == Math.Sign(blockerYDistance) &&
                        Math.Abs(blockerYDistance) < Math.Abs(targetYDistance))
                        return (false, blocker);
                }
                else if (targetYDistance == 0 || blockerYDistance == 0)
                {
                    if (targetYDistance == blockerYDistance &&
                        Math.Sign(targetXDistance) == Math.Sign(blockerXDistance) &&
                        Math.Abs(blockerXDistance) < Math.Abs(targetXDistance))
                        return (false, blocker);
                }
                else
                {
                    var targetAngle = (decimal)targetXDistance / targetYDistance;
                    var blockerAngle = (decimal)blockerXDistance / blockerYDistance;

                    if (targetAngle == blockerAngle &&
                        Math.Sign(targetXDistance) == Math.Sign(blockerXDistance) &&
                        Math.Sign(targetYDistance) == Math.Sign(blockerYDistance) &&
                        Math.Abs(blockerXDistance) < Math.Abs(targetXDistance))
                        return (false, blocker);
                }
            }

            return (true, null);
        }

        public List<Asteroid> GetAsteroidsStationCanView()
        {
            return Asteroids.Where(a => a != _station).Where(asteroid => StationCanView(asteroid).canView).ToList();
        }
    }
}