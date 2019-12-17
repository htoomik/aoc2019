using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class IntCodeComputer
    {
        public List<int> Run(string instructions, params int[] input)
        {
            var codes = instructions.Split(',').Select(int.Parse).ToList();

            var output = new List<int>();
            var inputPos = 0;

            var pos = 0;
            var move = 0;


            while (true)
            {
                var instruction = codes[pos];
                var intCode = instruction % 100;
                var paramMode1 = GetDigit(instruction, 3);
                var paramMode2 = GetDigit(instruction, 4);
                var jumped = false;

                if (paramMode1 != 0 && paramMode1 != 1)
                    throw new Exception("Wrong paramMode1: " + paramMode1);
                if (paramMode2 != 0 && paramMode2 != 1)
                    throw new Exception("Wrong paramMode2: " + paramMode2);

                if (intCode == 1)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    var term2 = GetTerm(codes, pos, paramMode2, 2);
                    var outPos = codes[pos + 3];
                    codes[outPos] = term1 + term2;
                    move = 4;
                }
                else if (intCode == 2)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    var term2 = GetTerm(codes, pos, paramMode2, 2);
                    var outPos = codes[pos + 3];
                    codes[outPos] = term1 * term2;
                    move = 4;
                }
                else if (intCode == 3)
                {
                    var term1 = input[inputPos++];
                    var outPos = codes[pos + 1];
                    codes[outPos] = term1;
                    move = 2;
                }
                else if (intCode == 4)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    output.Add(term1);
                    move = 2;
                }
                else if (intCode == 5)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    var term2 = GetTerm(codes, pos, paramMode2, 2);
                    if (term1 != 0)
                    {
                        pos = term2;
                        jumped = true;
                    }
                    else
                    {
                        move = 3;
                    }
                }
                else if (intCode == 6)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    var term2 = GetTerm(codes, pos, paramMode2, 2);
                    if (term1 == 0)
                    {
                        pos = term2;
                        jumped = true;
                    }
                    else
                    {
                        move = 3;
                    }
                }
                else if (intCode == 7)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    var term2 = GetTerm(codes, pos, paramMode2, 2);
                    var outPos = codes[pos + 3];

                    var value = term1 < term2 ? 1 : 0;
                    codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 8)
                {
                    var term1 = GetTerm(codes, pos, paramMode1, 1);
                    var term2 = GetTerm(codes, pos, paramMode2, 2);
                    var outPos = codes[pos + 3];

                    var value = term1 == term2 ? 1 : 0;
                    codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 99)
                {
                    break;
                }
                else
                {
                    throw new Exception("Unknown instruction " + intCode);
                }

                if (!jumped)
                    pos += move;
            }

            return output;
        }


        private static int GetTerm(List<int> codes, int pos, int paramMode, int offset)
        {
            var term1 = paramMode == 0 ? codes[codes[pos + offset]] : codes[pos + offset];
            return term1;
        }

        private static int GetDigit(int number, int indexFromEnd)
        {
            var asString = number.ToString();
            return indexFromEnd > asString.Length
                ? 0
                : asString[asString.Length - indexFromEnd] - 48;
        }
    }
}