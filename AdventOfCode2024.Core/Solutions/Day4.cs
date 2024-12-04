using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal partial class Day4(string inputFileName) : BaseDay(inputFileName)
    {
        [GeneratedRegex("XMAS")]
        private static partial Regex XmasRegex();

        [GeneratedRegex("SAMX")]
        private static partial Regex XmasReverseRegex();

        internal override int Part1(List<string> input)
        {
            return CountHorizontal(input) + CountVertical(input);
        }

        internal override int Part2(List<string> input)
        {
            return CountCrossMas(input);
        }

        private static int CountHorizontal(List<string> input)
        {
            var xmasCount = 0;
            int rows = input.Count;
            int cols = input[0].Length;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (j + 3 < cols)
                    {
                        string horizontal = input[i].Substring(j, 4);
                        if (XmasRegex().IsMatch(horizontal) || XmasReverseRegex().IsMatch(horizontal))
                            xmasCount++;
                    }
                    if (i + 3 < rows)
                    {
                        string vertical = new string(new char[] { input[i][j], input[i + 1][j], input[i + 2][j], input[i + 3][j] });
                        if (XmasRegex().IsMatch(vertical) || XmasReverseRegex().IsMatch(vertical))
                            xmasCount++;
                    }
                }
            }
            return xmasCount;
        }

        private static int CountVertical(List<string> input)
        {
            var xmasCount = 0;
            int rows = input.Count;
            int cols = input[0].Length;

            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < cols - 3; j++)
                {
                    string diagonal1 = new string(new char[] { input[i][j], input[i + 1][j + 1], input[i + 2][j + 2], input[i + 3][j + 3] });
                    string diagonal2 = new string(new char[] { input[i][j + 3], input[i + 1][j + 2], input[i + 2][j + 1], input[i + 3][j] });
                    if (XmasRegex().IsMatch(diagonal1) || XmasReverseRegex().IsMatch(diagonal1))
                        xmasCount++;
                    if (XmasRegex().IsMatch(diagonal2) || XmasReverseRegex().IsMatch(diagonal2))
                        xmasCount++;
                }
            }
            return xmasCount;
        }

        private static int CountCrossMas(List<string> input)
        {
            var xmasCount = 0;

            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (IsCrossMas(input, y, x))
                    {
                        xmasCount++;
                    }
                }
            }
            return xmasCount;
        }

        private static bool IsCrossMas(List<string> map, int y, int x)
        {
            return (map[y][x] == 'A' && y - 1 >= 0 && x - 1 >= 0 && y + 1 < map.Count && x + 1 < map[0].Length &&
                    ((map[y - 1][x - 1] == 'M' && map[y + 1][x + 1] == 'S' && map[y + 1][x - 1] == 'M' && map[y - 1][x + 1] == 'S') ||
                     (map[y - 1][x - 1] == 'M' && map[y + 1][x + 1] == 'S' && map[y + 1][x - 1] == 'S' && map[y - 1][x + 1] == 'M') ||
                     (map[y - 1][x - 1] == 'S' && map[y + 1][x + 1] == 'M' && map[y + 1][x - 1] == 'M' && map[y - 1][x + 1] == 'S') ||
                     (map[y - 1][x - 1] == 'S' && map[y + 1][x + 1] == 'M' && map[y + 1][x - 1] == 'S' && map[y - 1][x + 1] == 'M')));
        }
        }
    }
