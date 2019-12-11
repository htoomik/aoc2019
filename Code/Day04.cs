namespace aoc2019
{
    public class Day04
    {
        public int Solve(int value1, int value2)
        {
            var count = 0;
            for (int i = value1; i <= value2; i++)
            {
                var s = i.ToString();
                if (Matches1(s))
                    count++;
            }

            return count;
        }

        public int Solve2(int value1, int value2)
        {
            var count = 0;
            for (int i = value1; i <= value2; i++)
            {
                var s = i.ToString();
                if (Matches2(s))
                    count++;
            }

            return count;
        }

        public bool Matches1(string value)
        {
            if (value.Length != 6)
                return false;

            var chars = value.ToCharArray();
            var hasSameAdjacent = false;
            var neverDecreases = true;

            for (var i = 0; i < chars.Length - 1; i++)
            {
                var current = chars[i];
                var next = chars[i + 1];
                if (current == next)
                {
                    hasSameAdjacent = true;
                }

                if (current > next)
                {
                    neverDecreases = false;
                }
            }

            return hasSameAdjacent && neverDecreases;
        }


        public bool Matches2(string value)
        {
            if (value.Length != 6)
                return false;

            var chars = value.ToCharArray();
            var hasSameAdjacent = false;
            var neverDecreases = true;

            for (int i = 0; i < chars.Length - 1; i++)
            {
                var current = chars[i];
                var next = chars[i + 1];
                if (current > next)
                {
                    neverDecreases = false;
                }
            }

            for (int i = 0; i <= 9; i++)
            {
                if (value.Contains($"{i}{i}") && !value.Contains($"{i}{i}{i}"))
                {
                    hasSameAdjacent = true;
                }
            }

            return hasSameAdjacent && neverDecreases;
        }
    }
}