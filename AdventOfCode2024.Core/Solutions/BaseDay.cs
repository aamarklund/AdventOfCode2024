using AdventOfCode2024.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal abstract class BaseDay(string inputFileName)
    {
        internal void Solution()
        {
            var input = InputUtil.GetFileContent(inputFileName).ToList();

            // ---- PART 1 ----
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var solutionPart1 = Part1(input);
            timer.Stop();
            PrintSolution(1, solutionPart1, timer.ElapsedMilliseconds);

            Console.WriteLine("\n" +
                              "-------------------------" +
                              "\n");

            // ---- PART 2 ----
            timer.Restart();
            var solutionPart2 = Part2(input);
            timer.Stop();
            PrintSolution(2, solutionPart2, timer.ElapsedMilliseconds);

        }

        private static void PrintSolution(int part, int answer, long executionTime)
        {
            Console.WriteLine($"Answer part {part}: {answer}");
            Console.WriteLine($"Execution Time: {executionTime} ms");
        }

        internal abstract int Part1(List<string> input);
        internal abstract int Part2(List<string> input);
    }
}
