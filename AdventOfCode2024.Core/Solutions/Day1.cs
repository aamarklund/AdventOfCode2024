namespace AdventOfCode2024.Core.Solutions;
internal class Day1(string inputFileName) : BaseDay(inputFileName)
{
    internal override int Part1(List<string> input)
    {
        var column1 = GetColumn(input, 0);
        var column2 = GetColumn(input, 1);
        return Enumerable.Zip(column1, column2)
            .Select(col => Math.Abs(col.First - col.Second))
            .Sum();
    }

    internal override int Part2(List<string> input)
    {
        var column1 = GetColumn(input, 0);
        var column2 = GetColumn(input, 1);
        return column1
            .Select(col => 
                Math.Abs(col * column2.Count(x => x == col)))
            .Sum();
    }

    private static IEnumerable<int> GetColumn(List<string> input, int column)
    {
        return input.Select(line => 
            line.Split("   ")
            .Select(int.Parse)
            .ToArray()[column]
        ).Order();
    }
}
