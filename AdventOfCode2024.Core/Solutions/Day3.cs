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
        [GeneratedRegex("mul\\(\\d{1,3},\\d{1,3}\\)")]
        private static partial Regex MulRegex();

        [GeneratedRegex("do\\(\\)")]
        private static partial Regex DoRegex();

        [GeneratedRegex("don't\\(\\)")]

        private static partial Regex DontRegex();
        internal override int Part1(List<string> input)
        {
            var line = string.Join("", input);
            var mul = FindRegex(MulRegex().Matches(line));

            return MultiplyMulValues(
                mul
                .Select(x => x.value)
                .ToList()
            );
        }


        internal override int Part2(List<string> input)
        {
            var line = string.Join("", input);

            var muls = FindRegex(MulRegex().Matches(line));

            var switches = FindRegex(DontRegex().Matches(line));
            switches.AddRange(FindRegex(DoRegex().Matches(line)));

            return MultiplyMulValues(
                muls
                .Where(mul => IsMulDo(mul, switches))
                .Select(x => x.value)
                .ToList()
            );
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

        private static List<(string value, int index)> FindRegex(MatchCollection matches)
        {
            return matches.Select(x => (x.Value, x.Index)).ToList();
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
    }
}
