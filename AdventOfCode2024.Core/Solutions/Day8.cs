using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day8(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var (antennas, rows, cols) = BuildAntennaMap([.. input]);
            var antinodes = FindAntinodes(antennas, rows, cols);
            return antinodes.Count;
        }

        internal override int Part2(List<string> input)
        {
            var (antennas, rows, cols) = BuildAntennaMap([.. input]);
            var antinodes = FindAntinodes(antennas, rows, cols, true);
            return antinodes.Count;
        }

        static (Dictionary<char, List<(int, int)>>, int, int) BuildAntennaMap(string[] data)
        {
            var map = data.Select(line => line.ToCharArray()).ToArray();
            var antennas = new Dictionary<char, List<(int, int)>>();
            int rows = map.Length;
            int cols = map[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (map[row][col] != '.')
                    {
                        if (!antennas.TryGetValue(map[row][col], out List<(int, int)>? value))
                        {
                            value = [];
                            antennas[map[row][col]] = value;
                        }

                        value.Add((row, col));
                    }
                }
            }

            return (antennas, rows, cols);
        }

        private static HashSet<(int, int)> FindAntinodes(Dictionary<char, List<(int, int)>> antennas, int rows, int cols, bool part2 = false)
        {
            var antinodes = new HashSet<(int, int)>();

            foreach (var antenna in antennas)
            {
                var coords = antenna.Value;
                for (int i = 0; i < coords.Count; i++)
                {
                    for (int j = i + 1; j < coords.Count; j++)
                    {
                        var diff = (coords[j].Item1 - coords[i].Item1, coords[j].Item2 - coords[i].Item2);

                        AddAntinodes(antinodes, coords, diff, rows, cols, part2, i, -1);
                        AddAntinodes(antinodes, coords, diff, rows, cols, part2, j, 1);
                    }
                }
            }

            return antinodes;
        }

        private static void AddAntinodes(HashSet<(int, int)> antinodes, List<(int, int)> coords, (int, int) diff, int rows, int cols, bool part2, int index, int direction)
        {
            if (part2)
            {
                var pos = coords[index];
                while (pos.Item1 >= 0 && pos.Item1 < rows && pos.Item2 >= 0 && pos.Item2 < cols)
                {
                    antinodes.Add(pos);
                    pos = (pos.Item1 + diff.Item1 * direction, pos.Item2 + diff.Item2 * direction);
                }
            }
            else
            {
                var pos = (coords[index].Item1 + diff.Item1 * direction, coords[index].Item2 + diff.Item2 * direction);
                if (pos.Item1 >= 0 && pos.Item1 < rows && pos.Item2 >= 0 && pos.Item2 < cols)
                {
                    antinodes.Add(pos);
                }
            }
        }


    }

    internal record Position(char Value, (int row, int col) Cord); 
}
