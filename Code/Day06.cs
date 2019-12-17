using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day06
    {
        public int Solve(List<string> lines)
        {
            var links = Parse(lines);

            CalculateDistances(links);

            var sum = links.Values.Sum(v => v.Distance);
            return sum;
        }

        private static void CalculateDistances(Dictionary<string, Orbit> links)
        {
            var workToDo = true;
            while (workToDo)
            {
                var toProcess = links.Select(kvp => kvp.Value).Where(planet => planet.Distance <= 0);

                workToDo = false;

                foreach (var planet in toProcess)
                {
                    workToDo = true;
                    var orbitsAround = links[planet.OrbitsAround];
                    if (orbitsAround.Distance > 0)
                        planet.Distance = orbitsAround.Distance + 1;
                }
            }
        }

        public int Solve2(List<string> lines)
        {
            var links = Parse(lines);
            CalculateDistances(links);

            var santa = links["SAN"];
            var you = links["YOU"];

            var santaPath = GetPath(santa, links);
            var yourPath = GetPath(you, links);

            var intersect = FindFirstSharedOrbit(santaPath, yourPath);
            var intersection = links[intersect];

            var santaDistanceToIntersection = santa.Distance - intersection.Distance;
            var yourDistanceToIntersection = you.Distance - intersection.Distance;

            var santaDistanceToYou = santaDistanceToIntersection + yourDistanceToIntersection;
            var moves = santaDistanceToYou - 2;

            return moves;
        }

        private static Dictionary<string, Orbit> Parse(List<string> lines)
        {
            var links = new Dictionary<string, Orbit>();
            foreach (var line in lines)
            {
                var parts = line.Split(")");
                var orbitsAroundCom = parts[0] == "COM";
                var orbit = new Orbit(parts[1], parts[0], orbitsAroundCom ? 1 : 0);
                links.Add(parts[1], orbit);
            }

            return links;
        }

        private static List<string> GetPath(Orbit orbit, Dictionary<string, Orbit> links)
        {
            var result = new List<string>();
            var current = orbit;

            while (true)
            {
                var currentOrbitsAround = current.OrbitsAround;

                result.Add(currentOrbitsAround);

                if (currentOrbitsAround == "COM")
                    break;

                current = links[currentOrbitsAround];
            }

            return result;
        }

        private static string FindFirstSharedOrbit(List<string> santaPath, List<string> yourPath)
        {
            return santaPath.First(yourPath.Contains);
        }

        private class Orbit
        {
            public string Planet { get; }
            public string OrbitsAround { get; }
            public int Distance { get; set; }

            public Orbit(string planet, string orbitsAround, int distance)
            {
                Planet = planet;
                OrbitsAround = orbitsAround;
                Distance = distance;
            }
        }
    }
}