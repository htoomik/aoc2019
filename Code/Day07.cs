using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day07
    {
        public int Solve(string instructions)
        {
            var permutations = Permute(new List<int> { 0, 1, 2, 3, 4 });
            var computer = new IntCodeComputer();

            var maxOutput = 0;
            foreach (var permutation in permutations)
            {
                var output = GetFinalOutput(instructions, permutation, computer);

                if (output > maxOutput)
                {
                    maxOutput = output;
                }
            }

            return maxOutput;
        }

        public static int GetFinalOutput(string instructions, IEnumerable<int> permutation, IntCodeComputer computer)
        {
            var input = 0;
            var output = 0;
            foreach (var amplifier in permutation)
            {
                output = computer.Run(instructions, amplifier, input).Single();
                input = output;
            }

            return output;
        }

        public static IEnumerable<IEnumerable<int>> Permute(List<int> inputs)
        {
            if (!inputs.Any())
            {
                yield return Enumerable.Empty<int>();
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

        private static IEnumerable<int> Concat(int firstElement, IEnumerable<int> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }
    }
}