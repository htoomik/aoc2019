using System;
using System.Collections.Generic;
using System.Linq;
using aoc2019.Test;

namespace aoc2019
{
    public class Day13
    {
        public GameState Solve(string input)
        {
            var computer = new IntCodeComputer(input);
            computer.Run();
            var data = computer.Output();

            return new GameState().Update(data);
        }

        public int Solve2(string input)
        {
            var computer = new IntCodeComputer(input);
            computer.SetAddress(0, 2);

            var log = new LogOutputHelper(13);

            var outputPos = 0;
            var gameState = new GameState();
            while (true)
            {
                computer.Run();
                var output = computer.Output();
                var newOutput = new List<long>();
                for (var i = outputPos; i < output.Count; i++)
                {
                    newOutput.Add(output[i]);
                }

                outputPos += newOutput.Count;

                gameState.Update(newOutput);

                log.WriteLine(gameState.ToString());

                if (gameState.BlocksRemaining == 0)
                {
                    return gameState.Score;
                }

                var ballX = gameState.BallX();
                var paddleX = gameState.PaddleX();
                if (ballX < paddleX) // move left
                    computer.AddInput(-1);
                else if (ballX > paddleX) // move right
                    computer.AddInput(1);
                else
                    computer.AddInput(0);
            }
        }

        public class GameState
        {
            private Dictionary<Coords, Tile> Tiles { get; } = new Dictionary<Coords, Tile>();
            public int BlocksRemaining { get { return Tiles.Values.Count(t => t.T == 2); } }
            public int Score { get; private set; }

            public GameState Update(IList<long> data)
            {
                for (var i = 0; i < data.Count; i += 3)
                {
                    var x = (int) data[i];
                    var y = (int) data[i + 1];
                    var t = (int) data[i + 2];

                    if (x == -1 && y == 0)
                    {
                        Score = (int) data[i + 2];
                    }

                    Tiles[new Coords(x,y)] = new Tile(x, y, t);
                }

                return this;
            }

            public int BallX()
            {
                return Tiles.Values.Single(t => t.T == 4).X;
            }

            public int PaddleX()
            {
                return Tiles.Values.Single(t => t.T == 3).X;
            }

            public override string ToString()
            {
                var s = string.Empty;
                s += Score;
                s += "\r\n\r\n";

                for (int y = 0; y <= Tiles.Keys.Max(t => t.Y); y++)
                {
                    for (int x = 0; x <= Tiles.Keys.Max(t => t.X); x++)
                    {
                        var tile = Tiles[new Coords(x, y)];
                        s += tile.ToString();
                    }

                    s += "\r\n";
                }

                s += "\r\n\r\n";

                return s;
            }
        }

        private struct Coords
        {
            public int X;
            public int Y;

            public Coords(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private class Tile
        {
            public Tile(int x, int y, int t)
            {
                X = x;
                Y = y;
                T = t;
            }

            public int X { get; }
            public int Y { get; }
            public int T { get; }

            public override string ToString()
            {
                switch (T)
                {
                    case 0 : return ".";
                    case 1: return "#";
                    case 2: return "B";
                    case 3: return "-";
                    case 4: return "o";
                    default: throw new Exception("Unexpected tile " + T);
                }
            }
        }
    }
}