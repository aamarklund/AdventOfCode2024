using System.Runtime.InteropServices;

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

        private static Dictionary<(string, string), List<string>> ARROW_SEQS = GetSequenses(arrowKeypad);
        private static Dictionary<(string, string), int> ARROW_SEQS_LENGTH = ARROW_SEQS.ToDictionary(x => x.Key, x => x.Value.First().Length);
        private Dictionary<(string, string), List<string>> NUM_SEQS = GetSequenses(numKeypad);
        private static Dictionary<(string, string, int), long> LENGTH_CACHE = [];

        internal override int Part1(List<string> input)
        {
            long sum = 0;
            foreach (var line in input)
            {
                var inputs = Solve(line, NUM_SEQS);
                long optimal = long.MaxValue;
                foreach(var seq in inputs)
                {
                    long length = 0;
                    var asdf = Enumerable.Zip("A" + seq, seq, (a, b) => (a.ToString(), b.ToString()))
                                    .ToList();
                    foreach((string a, string b) in asdf)
                    {
                        length += GetLength(a, b);
                    }
                    optimal = Math.Min(optimal, length);
                }

                sum += optimal * int.Parse(string.Join("", line.Where(char.IsDigit).ToList()));
            }

            return (int)sum;
        }

        internal override int Part2(List<string> input)
        {
            long sum = 0;
            foreach (var line in input)
            {
                var inputs = Solve(line, NUM_SEQS);
                long optimal = long.MaxValue;
                foreach(var seq in inputs)
                {
                    long length = 0;
                    var asdf = Enumerable.Zip("A" + seq, seq, (a, b) => (a.ToString(), b.ToString()))
                                    .ToList();
                    foreach((string a, string b) in asdf)
                    {
                        length += GetLength(a, b, 25);
                    }
                    optimal = Math.Min(optimal, length);
                }

                sum += optimal * int.Parse(string.Join("", line.Where(char.IsDigit).ToList()));
            }

            return (int)sum; // DEBUG
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


        private static long GetLength(string x, string y, int depth = 2)
        {
            if (LENGTH_CACHE.TryGetValue((x, y, depth), out long cachedLength))
            {
                return cachedLength;
            }

            if (depth == 1)
            {

                return ARROW_SEQS_LENGTH[(x, y)];
            }

            long optimal = long.MaxValue;
            foreach (var seq in ARROW_SEQS[(x, y)])
            {
                long length = 0;

                var asdf = Enumerable.Zip("A" + seq, seq, (a, b) => (a.ToString(), b.ToString()))
                                .ToList();
                foreach ((var a, var b) in asdf)
                {
                    length += GetLength(a, b, depth - 1);
                }
                optimal = Math.Min(optimal, length);
            }

            long result = optimal;
            LENGTH_CACHE[(x, y, depth)] = result;
            return result;
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
                         select seq.Concat([item]);
            }
            return result;
        }

    }
}
