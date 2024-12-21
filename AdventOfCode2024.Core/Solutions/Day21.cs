using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day21(string inputFileName) : BaseDay(inputFileName)
    {
        private readonly static string[][] numKeypad =
        {
                ["7", "8", "9"],
                ["4", "5", "6"],
                ["1", "2", "3"],
                [null, "0", "A"],
            };

        private readonly static string[][] arrowKeypad =
        {
                [null, "^", "A"],
                ["<", "v", ">"],
        };

        private Dictionary<(string, string), List<string>> ARROW_SEQS = GetSequenses(arrowKeypad);
        private Dictionary<(string, string), List<string>> NUM_SEQS = GetSequenses(numKeypad);

        internal override int Part1(List<string> input)
        {

            var sum = 0;
            foreach (var line in input)
            {
                var robot1 = Solve(line, NUM_SEQS);
                var next = robot1;
                for (var i = 0; i < 2; i++)
                {
                    var possibleNext = new List<string>();
                    foreach(var seq in next)
                    {
                        possibleNext.AddRange(Solve(seq, ARROW_SEQS));
                    }
                    var min = possibleNext.Min(x => x.Length);
                    next = possibleNext.Where(x => x.Length == min).ToList();
                }

                sum += next[0].Length * int.Parse(string.Join("", line.Where(char.IsDigit).ToList()));
            }

            return sum;
        }

        private static List<string> Solve(string value, Dictionary<(string, string), List<string>> seqs)
        {

            var options = Enumerable.Zip("A" + value, value, (a, b) => (a.ToString(), b.ToString()))
                                    .Where(seqs.ContainsKey)
                                    .Select(x => seqs[x])
                                    .ToList();

            // Returns all the possible paths
            return GenerateCombinations(options)
                         .Select(x => string.Join("", x))
                         .ToList();
        }

        private static Dictionary<(string, string), List<string>> GetSequenses(string[][] keypad)
        {
            var positions = new Dictionary<string, (int, int)>();
            for (int r = 0; r < keypad.Length; r++)
            {
                for (int c = 0; c < keypad[0].Length; c++)
                {
                    if (keypad[r][c] != null)
                    {
                        positions[keypad[r][c]] = (r, c);
                    }
                }
            }

            var seqs = new Dictionary<(string, string), List<string>>();

            for (int x = 0; x < positions.Count; x++)
            {
                for (int y = 0; y < positions.Count; y++)
                {
                    if (positions.ElementAt(x).Key == positions.ElementAt(y).Key)
                    {
                        seqs[(positions.ElementAt(x).Key, positions.ElementAt(y).Key)] = new List<string> { "A" };
                        continue;
                    }

                    var possibilities = GetAllPosibilities(keypad, x, y, positions);
                    seqs[(positions.ElementAt(x).Key, positions.ElementAt(y).Key)] = possibilities;
                }
            }

            return seqs;
        }

        private static List<string> GetAllPosibilities(string[][] keypad, int x, int y, Dictionary<string, (int, int)> positions)
        {
            var possibilities = new List<string>();
            var xValue = positions.ElementAt(x).Value;
            var yValue = positions.ElementAt(y).Value;
            double optimal = double.PositiveInfinity;
            var queue = new Queue<((int, int), string)>();
            queue.Enqueue((xValue, ""));
            while (queue.Count > 0)
            {
                ((int r, int c), string moves) = queue.Dequeue();
                var directionList = new List<(int nr, int nc, string nm)>
                {
                    (r - 1, c, "^"),
                    (r + 1, c, "v"),
                    (r, c - 1, "<"),
                    (r, c + 1, ">"),
                };

                foreach (var (nr, nc, nm) in directionList)
                {
                    if (nr < 0 || nc < 0 || nr >= keypad.Length || nc >= keypad[0].Length || keypad[nr][nc] == null)
                    {
                        continue;
                    }

                    if (keypad[nr][nc] == positions.ElementAt(y).Key)
                    {
                        if (optimal < moves.Length + 1) return possibilities;
                        optimal = moves.Length + 1;
                        possibilities.Add(moves + nm + "A");
                    }
                    else
                    {
                        var newPosition = (nr, nc);
                        queue.Enqueue((newPosition, moves + nm));
                    }
                }
            }
            return possibilities;
        }

        private static IEnumerable<IEnumerable<T>> GenerateCombinations<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
            foreach (var sequence in sequences)
            {
                result = from seq in result
                         from item in sequence
                         select seq.Concat(new[] { item });
            }
            return result;
        }

        internal override int Part2(List<string> input)
        {
            throw new NotImplementedException();
        }
    }
}
