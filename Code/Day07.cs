using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day07
    {
        public int Solve(string instructions)
        {
            var permutations = Permute(new List<int> { 0, 1, 2, 3, 4 });
            var computer = new IntCodeComputer(instructions);

            var maxOutput = 0;
            foreach (var permutation in permutations)
            {
                var input = 0;
                var output = 0;
                foreach (var amplifier in permutation)
                {
                    output = computer.AddInput(amplifier, input).Run().Output().Single();
                    input = output;
                }

                if (output > maxOutput)
                {
                    maxOutput = output;
                }
            }

            return maxOutput;
        }

        public int Solve2(string instructions)
        {
            var permutations = Permute(new List<int> { 5, 6, 7, 8, 9 });

            var maxOutput = 0;
            foreach (var permutation in permutations)
            {
                var amps = CreateComputers(instructions);
                var output = GetOutputForPermutation(amps, permutation);
                if (output > maxOutput)
                {
                    maxOutput = output;
                }
            }

            return maxOutput;
        }

        public List<IntCodeComputer> CreateComputers(string instructions)
        {
            var amps = new List<IntCodeComputer>();
            for (var i = 0; i < 5; i++)
            {
                amps.Add(new IntCodeComputer(instructions));
            }

            return amps;
        }

        public int GetOutputForPermutation(List<IntCodeComputer> amps, List<int> permutation)
        {
            var input = 0;
            var output = 0;
            for (var i = 0; i < amps.Count; i++)
            {
                var phaseSetting = permutation[i];
                amps[i].AddInput(phaseSetting);
            }

            var j = 0;
            while (true)
            {
                if (amps[j].Halted)
                {
                    output = amps.Last().Output().Last();
                    break;
                }

                amps[j].AddInput(input);
                amps[j].Run();
                input = amps[j].Output().Last();

                j++;
                if (j == amps.Count)
                    j = 0;
            }

            return output;
        }

        public static IEnumerable<List<int>> Permute(List<int> inputs)
        {
            if (!inputs.Any())
            {
                yield return new List<int>();
            }

            for (var startingIndex = 0; startingIndex < inputs.Count; startingIndex++)
            {
                var startingElement = inputs[startingIndex];
                var remainingElements = inputs.Where(i => i != startingElement);

                var permutations = Permute(remainingElements.ToList());
                foreach (var permutation in permutations)
                {
                    yield return Concat(startingElement, permutation);
                }
            }
        }

        private static List<int> Concat(int firstElement, IEnumerable<int> secondSequence)
        {
            var result = new List<int> { firstElement };

            result.AddRange(secondSequence);

            return result;
        }
    }
}