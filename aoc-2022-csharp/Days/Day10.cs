using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day10 : Day
    {

        public override dynamic PartOne()
        {
            int sum = 0;
            foreach ((int cycle, int signal) in GetSignals())
                if (new int[]{20, 60, 100, 140, 180, 220}.Contains(cycle))
                    sum += cycle * signal;

            return sum;
        }

        public override dynamic PartTwo()
        {
            string drawing = Environment.NewLine;

            foreach((int cycle, int signal) in GetSignals())
            {
                int position = (cycle - 1) % 40;

                if (Math.Abs(signal - position) < 2)
                    drawing += "#";
                else
                    drawing += ".";

                if (position == 39)
                {
                    drawing += Environment.NewLine;
                }
            };

            return drawing;
        }

        private IEnumerable<(int cycle, int signal)> GetSignals()
        {
            int regValue = 1;
            int currentCycle = 0;

            for(int i = 0; i < input.Split(Environment.NewLine).Length; i++)
            {
                string line = input.Split(Environment.NewLine)[i];
                string[] instruction = line.Split(" ");
                
                if (instruction[0] == "noop")
                {
                    currentCycle++;
                    yield return (currentCycle, regValue);
                }
                else
                {
                    currentCycle++;
                    yield return (currentCycle, regValue);

                    currentCycle++;
                    yield return (currentCycle, regValue);

                    regValue += int.Parse(instruction[1]);
                }
            }
        }

    }
}
