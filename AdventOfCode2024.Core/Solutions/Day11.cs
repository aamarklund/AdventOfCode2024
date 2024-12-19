using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day11(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var stones = input.First().Split(" ").Select(long.Parse);

            long result = 0;
            foreach (var stone in stones)
            {
                result += CalculateStones(stone, 25);
            }
            return (int)result;
        }

        internal override int Part2(List<string> input)
        {
            var stones = input.First().Split(" ").Select(long.Parse);

            long result = 0;
            foreach (var stone in stones)
            {
                result += CalculateStones(stone, 75);
            }
            return (int)result;
        }

        private static Dictionary<(long, int), long> cache = new();

        private static long CalculateStones(long stone, int steps)
        {
            if (cache.TryGetValue((stone, steps), out var cachedResult))
                return cachedResult;

            long result;
            if (steps == 0)
                result = 1;
            else if (stone == 0)
                result = CalculateStones(1, steps - 1);
            else
            {
                var asString = stone.ToString();
                if (asString.Length % 2 == 0)
                    result = CalculateStones(long.Parse(asString[(asString.Length / 2)..]), steps - 1)
                             + CalculateStones(long.Parse(asString[..(asString.Length / 2)]), steps - 1);
                else
                    result = CalculateStones(stone * 2024, steps - 1);
            }

            cache[(stone, steps)] = result;
            return result;
        }
    }
}
