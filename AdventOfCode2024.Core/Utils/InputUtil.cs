using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Utils
{
    internal class InputUtil
    {
        internal static List<string> GetFileContent(string fileName)
        {
            return [.. System.IO.File.ReadAllLines($"..//..//..//Inputs/{fileName}.txt")];
        }
    }
}
