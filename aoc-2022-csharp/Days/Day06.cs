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
            for (int i = 0; i < input.Length - amount - 1; i++)
            {
                string abschnitt = "";
                for (int j = i; j < i + amount; j++)
                    abschnitt += input[j];

                if (abschnitt.All(x => abschnitt.IndexOf(x) == abschnitt.LastIndexOf(x)))
                {
                    return i + amount;
                }
            }

            Debug.Assert(false);
            return 0;
        }

    }
}
