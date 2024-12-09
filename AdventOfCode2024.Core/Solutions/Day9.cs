using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Core.Solutions
{
    internal class Day9(string inputFileName) : BaseDay(inputFileName)
    {
        internal override int Part1(List<string> input)
        {
            var arr = input[0].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            var finalList = new List<int>();
            var index = 0;
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int j = 0; j < arr[i]; j++)
                {
                    if(i%2 == 0)
                        finalList.Add(index);
                    else
                        finalList.Add(-1);
                }
                if(arr[i] >= 0 && i%2 == 0)
                {
                    index++;
                }
            }



            for(int i = 0; i < finalList.Count; i++)
            {
                while (finalList[^1] == -1)
                {
                    finalList.RemoveAt(finalList.Count - 1);
                }
                if (finalList[i] == -1)
                {
                    finalList[i] = finalList[^1];
                    finalList.RemoveAt(finalList.Count -1 );
                }
            }

            finalList = finalList.Where(x => x != -1).ToList();

            BigInteger sum = 0;
            for(int i =0; i < finalList.Count; i++)
            {
                sum += i * finalList[i];
            }
            return 0;            
        }

        internal override int Part2(List<string> input)
        {
            throw new NotImplementedException();
        }
    }
}
