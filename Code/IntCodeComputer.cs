using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace aoc2019
{
    public class IntCodeComputer
    {
        private readonly Dictionary<int, long> _codes;
        private readonly List<long> _input = new List<long>();
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
            _input.AddRange(i);
            return this;
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
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = (int) _codes[_pos + 3];
                    if (paramMode3 == 2)
                        outPos += _relBase;

                    _codes[outPos] = term1 + term2;
                    move = 4;
                }
                else if (intCode == 2)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = GetPosition(_codes, _pos, paramMode3, 3);
                    _codes[outPos] = term1 * term2;
                    move = 4;
                }
                else if (intCode == 3)
                {
                    if (_inputPos > _input.Count - 1)
                        return this;

                    var term1 = _input[_inputPos++];
                    var outPos = GetPosition(_codes, _pos, paramMode1, 1);
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
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
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
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = (int) _codes[_pos + 3];
                    if (paramMode3 == 2)
                        outPos += _relBase;

                    var value = term1 < term2 ? 1 : 0;
                    _codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 8)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
                    var term2 = GetTerm(_codes, _pos, paramMode2, 2);
                    var outPos = (int) _codes[_pos + 3];
                    if (paramMode3 == 2)
                        outPos += _relBase;

                    var value = term1 == term2 ? 1 : 0;
                    _codes[outPos] = value;
                    move = 4;
                }
                else if (intCode == 9)
                {
                    var term1 = GetTerm(_codes, _pos, paramMode1, 1);
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

        private long GetTerm(Dictionary<int, long> codes, long pos, int paramMode, int offset)
        {
            var value = codes[(int)pos + offset];
            switch (paramMode)
            {
                case 0:
                    return codes.ContainsKey((int)value) ? codes[(int)value] : 0;
                case 1:
                    return value;
                case 2:
                    return codes.ContainsKey(_relBase + (int)value) ? codes[_relBase + (int)value] : 0;
                default:
                    throw new Exception("Unexpected paramMode " + paramMode);
            }
        }

        private int GetPosition(Dictionary<int, long> codes, int pos, int paramMode, int offset)
        {
            var outPos = (int) codes[_pos + offset];
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