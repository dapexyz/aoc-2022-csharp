using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp
{
    internal abstract class Day
    {
        public string input;

        public Day()
        {
            int dayNumber = int.Parse(this.GetType().Name[3..]);
            Console.WriteLine("Day " + dayNumber);
            input = File.ReadAllText($"Inputs/{dayNumber}.txt");
        }

        public abstract dynamic PartOne();
        public abstract dynamic PartTwo();
    }
}
