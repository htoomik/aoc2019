using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day14
    {
        private Dictionary<string, Reaction> _reactions;
        private Dictionary<string, int> _stash;
        private int _oreUsed;

        public int Solve(string data)
        {
            var reactions = ParseReactions(data);
            _reactions = reactions.ToDictionary(r => r.Output.Name, r => r);
            _stash = reactions.ToDictionary(r => r.Output.Name, r => 0);
            _stash["ORE"] = 0;

            Make("FUEL", 1);

            return _oreUsed;
        }

        private void Make(string chemical, int target)
        {
            if (chemical != "ORE")
            {
                while (_stash[chemical] < target)
                {
                    var reaction = _reactions[chemical];
                    foreach (var input in reaction.Inputs)
                    {
                        Make(input.Name, input.Quantity);
                        _stash[input.Name] -= input.Quantity;
                    }

                    _stash[chemical] += reaction.Output.Quantity;
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