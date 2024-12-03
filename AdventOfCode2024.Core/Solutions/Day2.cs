using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day2(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var safeReports = 0;
            foreach (var line in input) 
            { 
                var report = line.Split(" ").Select(int.Parse);
                if (IsSafe(report))
                    safeReports++;

            }
            return safeReports;
        }

        internal override int Part2(List<string> input)
        {
            var safeReports = 0;
            foreach (var line in input)
            {
                var report = line.Split(" ").Select(int.Parse);

                if (IsSafe(report))
                    safeReports++;
                else
                {
                    var reportList = report.ToList();
                    for (int i = 0; i < reportList.Count; i++)
                    {
                        var reportLessOne = reportList.ToList();
                        reportLessOne.RemoveAt(i);
                        if (IsSafe(reportLessOne))
                        {
                            safeReports++;
                            break;
                        }
                    }
                }

            }
            return safeReports;
        }

        private static bool IsSafe(IEnumerable<int> report)
        {
            var difPairsLessOne =report 
                .Zip(report.Skip(1)).ToList()
                .Select(a => (a.Second - a.First, a.First - a.Second));
            return 
                difPairsLessOne.All(x => x.Item1 <= 3 && 1 <= x.Item1) ||
                difPairsLessOne.All(x => x.Item2 <= 3 && 1 <= x.Item2);
        }
    }
}
