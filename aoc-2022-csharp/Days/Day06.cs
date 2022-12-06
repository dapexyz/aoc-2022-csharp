using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day06 : Day
    {
        public override dynamic PartOne() => CalculateFirstDistinctPosition(input, 4);

        public override dynamic PartTwo() => CalculateFirstDistinctPosition(input, 14);


        private int CalculateFirstDistinctPosition(string input, int amount)
        {
            foreach(int i in Enumerable.Range(0, input.Length))
                if (input[i..(i + amount)].ToCharArray().Distinct().Count() == amount)
                    return i + amount;

            return 0;
        }

    }
}
