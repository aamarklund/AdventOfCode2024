using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day20(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var grid = input.Select(x => x.ToCharArray().Select(y => y.ToString()).ToArray()).ToArray();

            (int r, int c) startPos = (-1, -1);
            (int r, int c) endPos = (-1, -1);
            var allWalls = new List<(int r, int c)>();

            for(int r = 0; r < grid.Length; r++)
            {
                for(int c = 0;  c < grid[r].Length; c++)
                {
                    if (grid[r][c] == "#")
                    {
                        allWalls.Add((r, c));
                    }
                    if (grid[r][c] == "S")
                    {
                        startPos = (r, c);
                    }
                    if(grid[r][c] == "E")
                    {
                        endPos = (r, c);
                    }
                }
            }

            var count = 0;
            var withoutRemoveWall = GetShortestPathFromStartToEnd(grid, startPos, endPos);
            foreach (var wall in allWalls)
            {
                grid[wall.r][wall.c] = ".";
                var path = GetShortestPathFromStartToEnd(grid, startPos, endPos);
                if(path <= withoutRemoveWall - 100)
                {
                    count++;
                }
                grid[wall.r][wall.c] = "#";
            }

            return count;
        }

        internal override int Part2(List<string> input)
        {
            return -1;
        }


        private int GetShortestPathFromStartToEnd(string[][] grid, (int r, int c) startPosition, (int r, int c) endPosition)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var queue = new Queue<((int r, int c) pos, int dist)>();
            var visited = new bool[rows, cols];

            queue.Enqueue((startPosition, 0));
            visited[startPosition.r, startPosition.c] = true;

            while (queue.Count > 0)
            {
                var (currentPos, currentDist) = queue.Dequeue();
                if (currentPos == endPosition)
                {
                    return currentDist;
                }

                foreach (var (dr, dc) in directions)
                {
                    int newRow = currentPos.r + dr;
                    int newCol = currentPos.c + dc;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && !visited[newRow, newCol] && grid[newRow][newCol] != "#")
                    {
                        queue.Enqueue(((newRow, newCol), currentDist + 1));
                        visited[newRow, newCol] = true;
                    }
                }
            }

            return -1; // Path not found
        }
    }
}
