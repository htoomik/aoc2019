using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day08
    {
        public int Solve(string input, int width, int height)
        {
            var layers = ParseLayers(input, width * height);

            var best = int.MaxValue;
            var bestLayer = new Layer(null);
            foreach (var layer in layers)
            {
                var countOfZeros = layer.CountOf('0');
                if (countOfZeros < best)
                {
                    best = countOfZeros;
                    bestLayer = layer;
                }
            }

            return bestLayer.CountOf('1') * bestLayer.CountOf('2');
        }

        public string Solve2(string input, int width, int height)
        {
            const char transparent = '2';

            var layerSize = width * height;
            var layers = ParseLayers(input, layerSize);

            var final = new char[layerSize];

            for (var i = 0; i < layerSize; i++)
            {
                var layer = layers.First(l => l[i] != transparent);
                final[i] = layer[i];
            }

            var result = string.Empty;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    result += final[y * width + x];
                }

                result += "\r\n";
            }

            return result.Trim();
        }

        private static List<Layer> ParseLayers(string input, int layerSize)
        {
            var pos = 0;
            var layers = new List<Layer>();
            while (true)
            {
                layers.Add(new Layer(input.Substring(pos, layerSize)));
                pos += layerSize;

                if (pos >= input.Length)
                    break;
            }

            return layers;
        }

        private class Layer
        {
            private readonly string _s;

            public Layer(string s)
            {
                _s = s;
            }

            public int CountOf(char c1)
            {
                return _s.Count(c => c == c1);
            }

            public char this[int i] => _s[i];

            public override string ToString()
            {
                return _s;
            }
        }
    }
}