using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace aoc2019
{
    public class IntCodeComputer
    {
        private readonly Dictionary<int, long> _codes;
        private readonly Dictionary<long, long> _input = new Dictionary<long, long>();
        private readonly List<long> _output = new List<long>();
        private int _inputPos;
        private int _pos;
        private int _relBase;

        public bool Halted { get; private set; }

        public IntCodeComputer(string instructions)
        {
            var split = instructions.Split(',');
            _codes = new Dictionary<int, long>();
            for (var i = 0; i < split.Length; i++)
            {
                _codes[i] = long.Parse(split[i]);
            }
        }

        public IntCodeComputer AddInput(params long[] i)
        {
            var offset = _input.Any() ? _input.Keys.Max() + 1 : 0;
            for (var j = 0; j < i.Length; j++)
            {
                _input[j + offset] = i[j];
            }
            return this;
        }

        public void SetAddress(int a, int value)
        {
            _codes[a] = value;
        }

        public ReadOnlyCollection<long> Output()
        {
            return _output.AsReadOnly();
        }

        public IntCodeComputer Run()
        {
            var move = 0;

            while (true)
            {
                long instruction = _codes[_pos];
                var intCode = instruction % 100;
                var paramMode1 = GetDigit(instruction, 3);
                var paramMode2 = GetDigit(instruction, 4);
                var paramMode3 = GetDigit(instruction, 5);
                var jumped = false;

                if (paramMode1 < 0 || paramMode1 > 2)
                    throw new Exception("Wrong paramMode1: " + paramMode1);
                if (paramMode2 < 0 || paramMode2 > 2)
                    throw new Exception("Wrong paramMode2: " + paramMode2);
                if (paramMode3 < 0 || paramMode3 > 2)
                    throw new Exception("Wrong paramMode3: " + paramMode3);

                if (intCode == 1)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    var term2 = GetTerm(paramMode2, 2);
                    var outPos = (int) _codes[_pos + 3];
                    if (paramMode3 == 2)
                        outPos += _relBase;

                    _codes[outPos] = term1 + term2;
                    move = 4;
                }
                else if (intCode == 2)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    var term2 = GetTerm(paramMode2, 2);
                    var outPos = GetPosition(paramMode3, 3);
                    _codes[outPos] = term1 * term2;
                    move = 4;
                }
                else if (intCode == 3)
                {
                    var term1 = _input.ContainsKey(_inputPos) ? _input[_inputPos] : 0;
                    _inputPos++;
                    var outPos = GetPosition(paramMode1, 1);
                    _codes[outPos] = term1;
                    move = 2;
                }
                else if (intCode == 4)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    _output.Add(term1);
                    move = 2;
                }
                else if (intCode == 5)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    var term2 = GetTerm(paramMode2, 2);
                    if (term1 != 0)
                    {
                        _pos = (int)term2;
                        jumped = true;
                    }
                    else
                    {
                        move = 3;
                    }
                }
                else if (intCode == 6)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    var term2 = GetTerm(paramMode2, 2);
                    if (term1 == 0)
                    {
                        _pos = (int)term2;
                        jumped = true;
                    }
                    else
                    {
                        move = 3;
                    }
                }
                else if (intCode == 7)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    var term2 = GetTerm(paramMode2, 2);
                    var outPos = (int) _codes[_pos + 3];
                    if (paramMode3 == 2)
                        outPos += _relBase;

                    var value = term1 < term2 ? 1 : 0;
                    _codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 8)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    var term2 = GetTerm(paramMode2, 2);
                    var outPos = (int) _codes[_pos + 3];
                    if (paramMode3 == 2)
                        outPos += _relBase;

                    var value = term1 == term2 ? 1 : 0;
                    _codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 9)
                {
                    var term1 = GetTerm(paramMode1, 1);
                    _relBase += (int)term1;
                    move = 2;
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

        private long GetTerm(int paramMode, int offset)
        {
            var value = _codes[_pos + offset];
            switch (paramMode)
            {
                case 0:
                    return _codes.ContainsKey((int)value) ? _codes[(int)value] : 0;
                case 1:
                    return value;
                case 2:
                    return _codes.ContainsKey(_relBase + (int)value) ? _codes[_relBase + (int)value] : 0;
                default:
                    throw new Exception("Unexpected paramMode " + paramMode);
            }
        }

        private int GetPosition(int paramMode, int offset)
        {
            var outPos = (int) _codes[_pos + offset];
            switch (paramMode)
            {
                case 0:
                    return outPos;
                case 2:
                    return outPos + _relBase;
                default:
                    throw new Exception("Unexpected paramMode " + paramMode);
            }
        }

        private static int GetDigit(long number, int indexFromEnd)
        {
            var asString = number.ToString();
            return indexFromEnd > asString.Length
                ? 0
                : asString[asString.Length - indexFromEnd] - 48;
        }
    }
}