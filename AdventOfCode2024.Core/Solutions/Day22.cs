using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day22(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var lines = input.Select(long.Parse);
            long total = 0;
            foreach (var line in lines)
            {
                long num = line;
                for (int i = 0; i < 2000; i++)
                {
                    num = GetNextStep(num);
                }
                total += num;
            }
            return (int)total; // DEBUG
        }

        internal override int Part2(List<string> input)
        {
            var lines = input.Select(long.Parse);
            var seqTotal = new Dictionary<(long, long, long, long), long>();
            foreach (var line in lines)
            {
                long num = line;
                var buyer = new List<long>() { num % 10 };
                for (int i = 0; i < 2000; i++)
                {
                    num = GetNextStep(num);
                    buyer.Add(num % 10);
                }

                var seenSeqs = new HashSet<(long, long, long, long)>();
                for (int i = 0; i < buyer.Count - 4; i++)
                {
                    long a = buyer[i];
                    long b = buyer[i + 1];
                    long c = buyer[i + 2];
                    long d = buyer[i + 3];
                    long e = buyer[i + 4];

                    var seq = (b - a, c - b, d - c, e - d);
                    if(seenSeqs.Contains(seq))
                    {
                        continue;
                    }
                    seenSeqs.Add(seq);
                    if(!seqTotal.ContainsKey(seq))
                    {
                        seqTotal[seq] = 0;
                    }
                    seqTotal[seq] += e;

                }
            }

            return (int)seqTotal.Max(x => x.Value);
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
