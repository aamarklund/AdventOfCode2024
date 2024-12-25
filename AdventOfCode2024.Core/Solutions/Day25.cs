using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day25(string inputFileName) : BaseDay(inputFileName)
    {
        public readonly int HEIGHT = 5;

        internal override int Part1(List<string> input)
        {
            var lineInput = System.IO.File.ReadAllText($"..//..//..//Inputs/input.txt");

            var lockHeights = new List<(int,int,int,int,int)>();
            var keyHights = new List<(int,int,int,int,int)>();
            foreach (var block in lineInput.Split("\r\n\r\n"))
            {
                if (block.Split("\r\n")[0].All(x => x.Equals('#')))
                    lockHeights.Add(GetPinHeights(block));
                else
                    keyHights.Add(GetPinHeights(block));
            }


            return lockHeights.Sum(lo => keyHights.Count(key =>
                lo.Item1 + key.Item1 <= HEIGHT &&
                lo.Item2 + key.Item2 <= HEIGHT &&
                lo.Item3 + key.Item3 <= HEIGHT &&
                lo.Item4 + key.Item4 <= HEIGHT &&
                lo.Item5 + key.Item5 <= HEIGHT));
        }

        internal override int Part2(List<string> input)
        {
            return -1;
        }

        private static (int, int, int, int, int) GetPinHeights(string block)
        {
            var grid = block.Split("\n").Select(x => x.ToCharArray()).ToArray();

            var pinHeights = new List<int>();
            for (int c = 0; c < grid[0].Length - 1; c++)
            {
                pinHeights.Add(grid.Count(row => row[c] == '#') - 1);
            }

            return (pinHeights[0], pinHeights[1], pinHeights[2], pinHeights[3], pinHeights[4]);
        }
    }
}
