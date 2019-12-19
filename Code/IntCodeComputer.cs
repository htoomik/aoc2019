using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace aoc2019
{
    public class IntCodeComputer
    {
        private readonly List<int> _codes;
        private readonly List<int> _input = new List<int>();
        private readonly List<int> _output = new List<int>();
        private int _inputPos;
        private int _pos;

        public bool Halted { get; private set; }

        public IntCodeComputer(string instructions)
        {
            _codes = instructions.Split(',').Select(int.Parse).ToList();
        }

        public IntCodeComputer AddInput(params int[] i)
        {
            _input.AddRange(i);
            return this;
        }

        public ReadOnlyCollection<int> Output()
        {
            return _output.AsReadOnly();
        }

        public IntCodeComputer Run()
        {
            var move = 0;

            while (true)
            {
                var instruction = _codes[_pos];
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
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = _codes[_pos + 3];
                    _codes[outPos] = term1 + term2;
                    move = 4;
                }
                else if (intCode == 2)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = _codes[_pos + 3];
                    _codes[outPos] = term1 * term2;
                    move = 4;
                }
                else if (intCode == 3)
                {
                    if (_inputPos > _input.Count - 1)
                        return this;

                    var term1 = _input[_inputPos++];
                    var outPos = _codes[_pos + 1];
                    _codes[outPos] = term1;
                    move = 2;
                }
                else if (intCode == 4)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    _output.Add(term1);
                    move = 2;
                }
                else if (intCode == 5)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    if (term1 != 0)
                    {
                        _pos = term2;
                        jumped = true;
                    }
                    else
                    {
                        move = 3;
                    }
                }
                else if (intCode == 6)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    if (term1 == 0)
                    {
                        _pos = term2;
                        jumped = true;
                    }
                    else
                    {
                        move = 3;
                    }
                }
                else if (intCode == 7)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = _codes[_pos + 3];

                    var value = term1 < term2 ? 1 : 0;
                    _codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 8)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = _codes[_pos + 3];

                    var value = term1 == term2 ? 1 : 0;
                    _codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 99)
                {
                    Halted = true;
                    break;
                }
                else
                {
                    throw new Exception("Unknown instruction " + intCode);
                }

                if (!jumped)
                    _pos += move;
            }

            return this;
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