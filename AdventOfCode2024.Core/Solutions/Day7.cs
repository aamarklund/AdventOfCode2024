using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day7(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var equations = input 
                       .Select(line => line.Split(": "))
                       .Select(parts => new List<long> { long.Parse(parts[0]) }
                                         .Concat(parts[1].Split(' ').Select(long.Parse)).ToList())
                       .ToList();
            var operations = new List<Func<long, long, long>>
            {
                (a, b) => a + b,
                (a, b) => a * b
            };

            long sum = equations.Where(equation => IsTrue(
                equation[0],
                equation[1],
                equation[2],
                equation.Skip(3).ToList(),
                operations
            )).Sum(equation => equation[0]);
            return -1; // DEBUG
        }

        internal override int Part2(List<string> input)
        {
            var equations = input 
                       .Select(line => line.Split(": "))
                       .Select(parts => new List<long> { long.Parse(parts[0]) }
                                         .Concat(parts[1].Split(' ').Select(long.Parse)).ToList())
                       .ToList();

            var operations = new List<Func<long, long, long>>
            {
                (a, b) => a + b,
                (a, b) => a * b,
                (a, b) => long.Parse(a.ToString() + b.ToString())
            };
            long sum = equations.Where(equation => IsTrue(
                equation[0],
                equation[1],
                equation[2],
                equation.Skip(3).ToList(),
                operations
            )).Sum(equation => equation[0]);
            return -1; // DEBUG
        }

        private static bool IsTrue(long expectedResult, long operand1, long operand2, List<long> remainingOperands, List<Func<long, long, long>> operations)
        {
            if (remainingOperands.Count == 0)
            {
                return operations.Any(operation => operation(operand1, operand2) == expectedResult);
            }
            else
            {
                return operations.Any(operation => IsTrue(
                    expectedResult,
                    operation(operand1, operand2),
                    remainingOperands[0],
                    remainingOperands.Skip(1).ToList(),
                    operations
                ));
            }
        }
    }
}
