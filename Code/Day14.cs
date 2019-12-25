using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace aoc2019
{
    public class Day14
    {
        private Dictionary<string, Reaction> _reactions;
        private Dictionary<string, long> _stash;
        private long _oreUsed;

        public long Solve(string data, long target = 1)
        {
            var reactions = ParseReactions(data);
            _reactions = reactions.ToDictionary(r => r.Output.Name, r => r);
            _stash = reactions.ToDictionary(r => r.Output.Name, r => 0L);
            _stash["ORE"] = 0;

            Make("FUEL", target);

            return _oreUsed;
        }

        public long Solve2(string data)
        {
            var reactions = ParseReactions(data);
            _reactions = reactions.ToDictionary(r => r.Output.Name, r => r);
            _stash = reactions.ToDictionary(r => r.Output.Name, r => 0L);
            _stash["ORE"] = 0;

            var oreForOneFuel = new Day14().Solve(data, 1);
            const long oreInCargo = 1000000000000;
            var min = oreInCargo / oreForOneFuel;
            var max = min * 2;

            var guess = max;

            while (true)
            {
                var oreForGuess = new Day14().Solve(data, guess);
                if (oreForGuess > oreInCargo)
                {
                    max = guess;
                }

                if (oreForGuess < oreInCargo)
                {
                    min = guess;
                }

                if (min == max || min == max - 1)
                {
                    return min;
                }

                guess = (min + max) / 2;
            }
        }

        private void Make(string chemical, long target)
        {
            if (chemical != "ORE")
            {
                while (_stash[chemical] < target)
                {
                    var quantityNeeded = target - _stash[chemical];
                    var reaction = _reactions[chemical];
                    var timesToRun = reaction.Output.Quantity >= quantityNeeded
                        ? 1
                        : quantityNeeded / reaction.Output.Quantity;
                    foreach (var input in reaction.Inputs)
                    {
                        Make(input.Name, input.Quantity * timesToRun);
                        _stash[input.Name] -= input.Quantity * timesToRun;
                    }

                    _stash[chemical] += reaction.Output.Quantity * timesToRun;
                }
            }
            else
            {
                var oreMined = target - _stash["ORE"];
                _stash["ORE"] += oreMined;
                _oreUsed += oreMined;
            }

        }

        private static List<Reaction> ParseReactions(string data)
        {
            var lines = data.Split("\r\n");
            var reactions = new List<Reaction>();
            foreach (var line in lines)
            {
                var parts = line.Split(" => ");
                var inputs = parts[0].Split(", ").Select(Chemical.Parse).ToList();
                var output = Chemical.Parse(parts[1]);
                reactions.Add(new Reaction(inputs, output));
            }

            return reactions;
        }

        private class Reaction
        {
            public readonly List<Chemical> Inputs;
            public readonly Chemical Output;

            public Reaction(List<Chemical> inputs, Chemical output)
            {
                Inputs = inputs;
                Output = output;
            }
        }

        private class Chemical
        {
            public int Quantity { get; }
            public string Name { get; }

            private Chemical(int quantity, string name)
            {
                Quantity = quantity;
                Name = name;
            }

            public static Chemical Parse(string arg)
            {
                var parts = arg.Trim().Split(" ");
                return new Chemical(int.Parse(parts[0]), parts[1]);
            }
        }
    }
}