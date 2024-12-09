using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day5(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var result = 0;
            var (rules, updates) = ParseInput(input);

            var visited = new List<int>();
            foreach(var update in updates)
            {
                var upd = update.Split(",").Select(int.Parse).ToList();
                foreach(var seq in upd)
                {
                    if (rules.Any(x => x.Item1 == seq)) {
                        visited.Add(seq);
                    }
                    else if(visited.Contains(rules.Where(x => x.Item1 == seq).First().Item1))
                    {
                        visited.Add(seq);
                    }
                }
                if(visited.Count == upd.Count)
                {
                    result += GetMiddlePage(update.Split(","));
                } 

            }
            return result;
        }

        internal override int Part2(List<string> input)
        {
            return 0;
        }

        private static int GetMiddlePage(string[] nums)
        {
            return int.Parse(nums[nums.Length / 2]);
        }

        private static (List<(int, int)> rules, List<string> updates) ParseInput(List<string> input)
        {
            var longString = string.Join("\n", input);
            var orderingRules = longString.Split("\n\n")[0].Split("\n");
            var rules = new List<(int, int)>();

            foreach (var orderingRule in orderingRules)
            {
                var split = orderingRule.Split('|').Select(int.Parse).ToArray();
                rules.Add((split[0], split[1]));
            }
            var updates = longString.Split("\n\n")[1].Split("\n").ToList();

            return (rules, updates);
        }
    }
}
