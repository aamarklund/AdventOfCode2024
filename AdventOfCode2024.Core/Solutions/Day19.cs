using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day19(string inputFileName) : BaseDay(inputFileName)
    {
        private Dictionary<string, bool> _cache = new Dictionary<string, bool>();
        private Dictionary<string, long> _cacheTwo = new Dictionary<string, long>();

        internal override int Part1(List<string> input)
        {
            var patterns = input.First().Split(", ").ToList();
            var designs = input.Skip(2).ToList();
            var maxPatternLength = patterns.Max(pattern => pattern.Length);
            var minPatternLength = patterns.Min(pattern => pattern.Length);

            return designs.Count(x => IsPossible(patterns, x, maxPatternLength, minPatternLength));
        }

        internal override int Part2(List<string> input)
        {
            var patterns = input.First().Split(", ").ToList();
            var designs = input.Skip(2).ToList();
            var maxPatternLength = patterns.Max(pattern => pattern.Length);
            var minPatternLength = patterns.Min(pattern => pattern.Length);

            long count = designs.Sum(design => CountPossibilities(patterns, design, maxPatternLength, minPatternLength));
            return (int)count; // DEBUG
        }

        private bool IsPossible(List<string> patterns, string design, int maxPatternLength, int minPatternLength)
        {
            if (_cache.ContainsKey(design)) return _cache[design];
            if (design == "") return true;
            for (int i = minPatternLength; i < design.Length + 1; i++)
            {
                if (patterns.Contains(design.Substring(0, i)) && IsPossible(patterns, design.Substring(i), maxPatternLength, minPatternLength))
                {
                    _cache[design] = true;
                    return true;
                }
            }
            _cache[design] = false;
            return false;
        }

        private long CountPossibilities(List<string> patterns, string design, int maxPatternLength, int minPatternLength)
        {
            if (_cacheTwo.ContainsKey(design)) return _cacheTwo[design];
            if (design == "") return 1;
            long count = 0;
            for (int i = minPatternLength; i < design.Length + 1; i++)
            {
                if (patterns.Contains(design[..i]))
                {
                    count += CountPossibilities(patterns, design[i..], maxPatternLength, minPatternLength);
                }
            }
            _cacheTwo[design] = count;
            return count;
        }
    }
}
