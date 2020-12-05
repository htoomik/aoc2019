using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2019
{
    public class Day15
    {
        private const string OutputPath = "C:\\Code\\aoc2019\\Data\\output15.txt";
        private readonly HashSet<Coords> _walls = new HashSet<Coords>();
        private Coords _oxygen;

        public int Solve(string input)
        {
            var frontier = new Queue<Coords>();
            var paths = new Dictionary<Coords, List<long>>();
            var explored = new HashSet<Coords>();

            var origin = new Coords(0, 0);
            frontier.Enqueue(origin);
            paths[origin] = new List<long>();

            ClearOutput();

            while (frontier.TryDequeue(out var current))
            {
                if (explored.Contains(current))
                {
                    continue;
                }

                explored.Add(current);
                var pathToCurrent = paths[current];

                for (var i = 1; i < 5; i++)
                {
                    var computer = new IntCodeComputer(input);
                    var newCoords = NewCoords(current, i);

                    if (_walls.Contains(newCoords))
                    {
                        continue;
                    }

                    var newCode = new List<long>(pathToCurrent) { i };
                    paths[newCoords] = newCode;

                    computer.AddInput(newCode.ToArray());
                    computer.Run();
                    var output = computer.Output().Last();

                    if (output == 0)
                    {
                        _walls.Add(newCoords);
                    }
                    else if (output == 1)
                    {
                        frontier.Enqueue(newCoords);
                    }
                    else if (output == 2)
                    {
                        frontier.Enqueue(newCoords);
                        _oxygen = newCoords;
                    }
                }
            }

            PrintMap(_walls, paths.Keys.ToList(), _oxygen);

            return paths[_oxygen].Count;
        }

        public int Solve2(string input)
        {
            Solve(input);
            var frontier = new Queue<Coords>();
            var paths = new Dictionary<Coords, List<Coords>>();

            frontier.Enqueue(_oxygen);
            paths[_oxygen] = new List<Coords>();

            while (frontier.TryDequeue(out var current))
            {
                var neighbours = GetNextSteps(current);
                foreach (var neighbour in neighbours)
                {
                    if (!paths.ContainsKey(neighbour))
                    {
                        frontier.Enqueue(neighbour);
                        paths[neighbour] = new List<Coords>(paths[current]) { current };
                    }
                }
            }

            return paths.Values.Max(p => p.Count);
        }

        private IEnumerable<Coords> GetNextSteps(Coords current)
        {
            if (!_walls.Contains(new Coords(current.X, current.Y - 1)))
            {
                yield return new Coords(current.X, current.Y - 1);
            }

            if (!_walls.Contains(new Coords(current.X, current.Y + 1)))
            {
                yield return new Coords(current.X, current.Y + 1);
            }

            if (!_walls.Contains(new Coords(current.X - 1, current.Y)))
            {
                yield return new Coords(current.X - 1, current.Y);
            }

            if (!_walls.Contains(new Coords(current.X + 1, current.Y)))
            {
                yield return new Coords(current.X + 1, current.Y);
            }
        }

        private static void ClearOutput()
        {
            File.Delete(OutputPath);
        }

        private static void PrintMap(HashSet<Coords> walls, List<Coords> visited, Coords oxygen)
        {
            var minX = Math.Min(visited.Min(v => v.X), walls.Min(v => v.X));
            var maxX = Math.Max(visited.Max(v => v.X), walls.Max(v => v.X));
            var minY = Math.Min(visited.Min(v => v.Y), walls.Min(v => v.Y));
            var maxY = Math.Max(visited.Max(v => v.Y), walls.Max(v => v.Y));

            var yRange = maxY - minY + 1;
            var xRange = maxX - minX + 1;
            var output = new char[yRange, xRange];
            for (var x = 0; x < xRange; x++)
            {
                for (var y = 0; y < yRange; y++)
                {
                    var coords = new Coords(minX + x, minY + y);
                    if (walls.Contains(coords))
                    {
                        output[y, x] = '#';
                    }
                    else if (coords.Equals(oxygen))
                    {
                        output[y, x] = 'O';
                    }
                    else if (visited.Contains(coords))
                    {
                        output[y, x] = '.';
                    }
                    else
                    {
                        output[y, x] = ' ';
                    }
                }
            }

            var sb = new StringBuilder();
            for (var x = 0; x < xRange; x++)
            {
                for (var y = 0; y < yRange; y++)
                {
                    sb.Append(output[y, x]);
                }

                sb.AppendLine();
            }

            File.AppendAllText(OutputPath, sb.ToString());
        }

        private static Coords NewCoords(Coords current, int direction)
        {
            return direction switch
            {
                1 => new Coords(current.X, current.Y - 1),
                2 => new Coords(current.X, current.Y + 1),
                3 => new Coords(current.X - 1, current.Y),
                4 => new Coords(current.X + 1, current.Y),
                _ => throw new Exception("Unexpected direction")
            };
        }
    }
}