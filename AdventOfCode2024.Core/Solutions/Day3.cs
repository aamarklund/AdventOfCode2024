using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal partial class Day3(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var result = 0;
            foreach(var line in input)
            {
                var mul = FindMul(line);
                result += MultiplyMulValues(mul.Select(x => x.value).ToList());
            }
            return result;
        }


        internal override int Part2(List<string> input)
        {
            // join all the strings from "input" to one string
            var result = 0;
            var line = string.Join("", input);
            var muls = FindMul(line);
            var donts = FindDoAndDonts(line);
            result += MultiplyMulValues(muls.Where(mul => IsMulDo(mul, donts)).Select(x => x.value).ToList());
            return result;
        }

        private static bool IsMulDo((string value, int index) mul, List<(string value, int index)> donts)
        {
            return !donts
                .Where(x => x.index < mul.index)
                .OrderByDescending(x => x.index)
                .Select(x => x.value)
                .ToList()
                .FirstOrDefault()?
                .Contains("don't")??true;
        }

        private static List<(string value, int index)> FindMul(string input)
        {
            var list = new List<(string, int)>();
            var matches = MulRegex().Matches(input);
            foreach (Match match in matches)
            {
                list.Add((match.Value, match.Index));
            }
            return list;
        }

        private static int MultiplyMulValues(List<string> multipliers)
        {
            var result = 0;
            foreach (var mul in multipliers)
            {
                var split = mul.Split(",");
                var value1 = int.Parse(new string(split[0].Where(char.IsDigit).ToArray()));
                var value2 = int.Parse(new string(split[1].Where(char.IsDigit).ToArray()));

                result += (value1 * value2);
            }

            return result;
        }
        private static List<(string value, int index)> FindDoAndDonts(string input)
        {
            var list = new List<(string, int)>();
            var dos = DoRegex().Matches(input);
            var donts = DontRegex().Matches(input);
            foreach (Match match in dos)
            {
                list.Add((match.Value, match.Index));
            }

            foreach (Match match in donts)
            {
                list.Add((match.Value, match.Index));
            }
            return list;
        }

        [GeneratedRegex("mul\\(\\d{1,3},\\d{1,3}\\)")]
        private static partial Regex MulRegex();
        

        [GeneratedRegex("do\\(\\)")]
        private static partial Regex DoRegex();

        [GeneratedRegex("don't\\(\\)")]
        private static partial Regex DontRegex();
    }
}
