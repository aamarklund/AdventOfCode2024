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
            return multipliers.Sum(mul =>
            {
                var values = mul.Split(',')
                                .Select(part => int.Parse(new string(part.Where(char.IsDigit).ToArray())))
                                .ToArray();
                return values[0] * values[1];
            });
        }
    }
}
