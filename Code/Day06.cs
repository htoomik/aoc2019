using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace aoc2019
{
    public class Day06
    {
        public int Solve(List<string> lines)
        {
            var links = new Dictionary<string, Orbit>();
            foreach (var line in lines)
            {
                var parts = line.Split(")");
                var orbitsAroundCom = parts[0] == "COM";
                var orbit = new Orbit(parts[1], parts[0], orbitsAroundCom ? 1 : 0);
                links.Add(parts[1], orbit);
            }

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

            var sum = links.Values.Sum(v => v.Distance);
            return sum;
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