using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day10(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
                var topoMap = input;


                var dirs = new (int, int)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
                int total = 0;

                for (int i = 0; i < topoMap.Count; i++)
                {
                    var row = topoMap[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j] == '0')
                        {
                            var bfsNodes = new Queue<(int, int)>();
                            bfsNodes.Enqueue((i, j));
                            var visited = new HashSet<(int, int)>();

                            while (bfsNodes.Count > 0)
                            {
                                var (x, y) = bfsNodes.Dequeue();
                                if (visited.Contains((x, y)))
                                {
                                    continue;
                                }
                                visited.Add((x, y));

                                if (topoMap[x][y] == '9')
                                {
                                    total += 1;
                                    continue;
                                }

                                foreach (var (dx, dy) in dirs)
                                {
                                    int nx = x + dx;
                                    int ny = y + dy;

                                    if (nx >= 0 && nx < topoMap.Count && ny >= 0 && ny < row.Length)
                                    {
                                        if (int.Parse(topoMap[nx][ny].ToString()) - int.Parse(topoMap[x][y].ToString()) == 1)
                                        {
                                            bfsNodes.Enqueue((nx, ny));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return total;
        }

        internal override int Part2(List<string> input)
        {
                var topoMap = input;

                var dirs = new (int, int)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
                int total = 0;

                for (int i = 0; i < topoMap.Count; i++)
                {
                    var row = topoMap[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j] == '0')
                        {
                            var bfsNodes = new Queue<(int, int)>();
                            bfsNodes.Enqueue((i, j));
                            var visited = new Dictionary<(int, int), int> { { (i, j), 1 } };

                            while (bfsNodes.Count > 0)
                            {
                                var (x, y) = bfsNodes.Dequeue();

                                if (int.Parse(topoMap[x][y].ToString()) == 9)
                                {
                                    total += visited[(x, y)];
                                    continue;
                                }

                                foreach (var (dx, dy) in dirs)
                                {
                                    int nx = x + dx;
                                    int ny = y + dy;

                                    if (nx >= 0 && nx < topoMap.Count && ny >= 0 && ny < row.Length)
                                    {
                                        if (int.Parse(topoMap[nx][ny].ToString()) - int.Parse(topoMap[x][y].ToString()) == 1)
                                        {
                                            if (!visited.ContainsKey((nx, ny)))
                                            {
                                                visited[(nx, ny)] = 0;
                                                bfsNodes.Enqueue((nx, ny));
                                            }
                                            visited[(nx, ny)] += visited[(x, y)];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return total;
            }
        }

}
