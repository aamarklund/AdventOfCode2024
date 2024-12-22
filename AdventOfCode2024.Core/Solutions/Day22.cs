using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day22(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            long total = 0;
            foreach (var line in input)
            {
                long num = long.Parse(line);
                for (int i = 0; i < 2000; i++)
                {
                    num = GetNextStep(num);
                }
                total += num;
            }
            return (int)total;
        }

        internal override int Part2(List<string> input)
        {
            return -1;
        }

        private static long GetNextStep(long number)
        {
            number = (number ^ (number * 64)) % 16777216;
            number = (number ^ (number / 32)) % 16777216;
            number = (number ^ (number * 2048)) % 16777216;
            return number;
        }
    }
}
