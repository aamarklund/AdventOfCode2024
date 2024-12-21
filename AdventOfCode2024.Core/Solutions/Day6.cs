using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day6(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var grid = input.Select(x => x.ToCharArray().Select(y => y.ToString()).ToArray()).ToArray();
            var currentDir = (-1, 0);
            var currentPos = GetStartPos(grid);
            var width = grid[0].Length;
            var height = grid.Length;

            var visitedPositions = new HashSet<(int, int)>();

            while(true)
            {
                visitedPositions.Add(currentPos);
                var nextPosition = (currentPos.Item1 + currentDir.Item1, currentPos.Item2 + currentDir.Item2);
                if (!IsInGrid(height, width, nextPosition))
                {
                    break;
                }
                if (grid[nextPosition.Item1][nextPosition.Item2] == "#")
                {
                    currentDir = GetNextDir(currentDir);
                }
                currentPos = (currentPos.Item1 + currentDir.Item1, currentPos.Item2 + currentDir.Item2); 
            }

            return visitedPositions.Count;
        }


        internal override int Part2(List<string> input)
        {
            var grid = input.Select(x => x.ToCharArray().Select(y => y.ToString()).ToArray()).ToArray();
            var width = grid[0].Length;
            var height = grid.Length;
            var startPos = GetStartPos(grid);


            var sum = 0;
            for(int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    if (grid[r][c] != ".")
                    {
                        continue;
                    }
                    grid[r][c] = "#";
                    if (FoundLoop(grid, startPos.Item1, startPos.Item2))
                    {
                        sum++;
                    }
                    grid[r][c] = ".";
                }
            }

            return sum;
        }

        private static bool FoundLoop(string[][] grid, int r, int c)
        {
            var (dirR, dirC) = (-1, 0);
            var visited = new HashSet<(int r, int c, int dr, int dc)>();

            while (true)
            {
                visited.Add((r, c, dirR, dirC));
                if (!IsInGrid(grid.Length, grid[0].Length, (r + dirR, c + dirC)))
                {
                    return false;
                }
                if (grid[r + dirR][c + dirC] == "#")
                {
                    (dirR, dirC) = GetNextDir((dirR, dirC));
                }
                else
                {
                    r += dirR;
                    c += dirC;
                }
                if (visited.Contains((r, c, dirR, dirC)))
                {
                    return true;
                }
            }
        }

        private static (int, int) GetNextDir((int, int) currentDir)
        {
            return currentDir switch
            {
                (-1, 0) => (0, 1),  
                (0, 1) => (1, 0),  
                (1, 0) => (0, -1),
                (0, -1) => (-1, 0),
                _ => throw new ArgumentException("Invalid direction")
            };
        }

        private static bool IsInGrid(int height, int width, (int, int) position)
        {
            var (row, col) = position;
            return row >= 0 && row < height && col >= 0 && col < width;
        }

        private static (int, int) GetStartPos(string[][] grid)
        {

            for(int r  = 0; r < grid.Length; r++)
            {
                for(int c = 0;  c < grid[r].Length; c++)
                {
                    if (grid[r][c] == "^") return (r, c); 
                }
            }
            throw new Exception("NO START POS");
        }
    }
}
