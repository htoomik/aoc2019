using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

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
        private const double Pi = Math.PI;
        private const double FullCircle = Pi * 2;

        public int X { get; }
        public int Y { get; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double AngleTo(Asteroid station)
        {
            var targetXDistance = X - station.X;
            var targetYDistance = Y - station.Y;

            if (targetXDistance == 0)
            {
                return targetYDistance < 0
                    ? Pi * 3 / 2 // straight up
                    : Pi / 2; // straight down
            }

            if (targetYDistance == 0)
            {
                return targetXDistance > 0
                    ? 0 // to the right
                    : Pi; // to the left
            }

            var tan = (double)targetYDistance / targetXDistance;

            var angle = Math.Atan(tan);

            return targetXDistance < 0
                ? angle + Pi
                : targetYDistance < 0
                    ? angle + FullCircle
                    : angle;
        }

        public double AdjustedAngleTo(Asteroid station)
        {
            var angle = AngleTo(station);

            // Adjust so that straight up becomes 0
            angle += Pi / 2;
            if (angle >= FullCircle)
                angle -= FullCircle;

            return angle;
        }

        public double DistanceTo(Asteroid station)
        {
            var targetXDistance = X - station.X;
            var targetYDistance = Y - station.Y;

            return Math.Abs(targetXDistance) + Math.Abs(targetYDistance);
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

                var targetAngle = target.AngleTo(_station);
                var blockerAngle = blocker.AngleTo(_station);

                var angleIsSame = Math.Abs(targetAngle - blockerAngle) < 0.0001;
                var targetDistance = target.DistanceTo(_station);
                var blockerDistance = blocker.DistanceTo(_station);
                var targetIsBeyondBlocker = targetDistance > blockerDistance;
                if (angleIsSame && targetIsBeyondBlocker)
                    return (false, blocker);
            }

            return (true, null);
        }

        public List<Asteroid> GetAsteroidsStationCanView()
        {
            return Asteroids.Where(a => a != _station).Where(asteroid => StationCanView(asteroid).canView).ToList();
        }
    }
}