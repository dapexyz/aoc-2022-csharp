using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day01 : Day
    {
        private IEnumerable<int> Calories() =>
            from block
            in input.Split(Environment.NewLine + Environment.NewLine)
            select block.Split(Environment.NewLine).Select(int.Parse).Sum();

        public override dynamic PartOne()
        {
            return Calories().Max();
        }

        public override dynamic PartTwo()
        {
            return Calories().OrderByDescending(x => x).Take(3).Sum();
        }
    }
}
