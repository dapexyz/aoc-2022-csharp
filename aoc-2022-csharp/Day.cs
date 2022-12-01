using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp
{
    internal abstract class Day
    {
        public string input = string.Empty;

        public abstract dynamic PartOne();
        public abstract dynamic PartTwo();

        public void SetInput(string input) => this.input = input;
    }
}
