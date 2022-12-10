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
            return 20 * GetSignalAfterCycle(20).signal + 60 * GetSignalAfterCycle(60).signal + 100 * GetSignalAfterCycle(100).signal + 140 * GetSignalAfterCycle(140).signal + 180 * GetSignalAfterCycle(180).signal + 220 * GetSignalAfterCycle(220).signal;
        }

        public override dynamic PartTwo()
        {
            string drawing = Environment.NewLine;
            bool lastInstruction = false;

            int i = 0;
            while (!lastInstruction)
            {
                (int signal, bool lastInstruction) signal = GetSignalAfterCycle(i);
                int position = (i - 1) % 40;


                if (Math.Abs(signal.signal - position) < 2)
                    drawing += "#";
                else
                    drawing += ".";

                if (position == 39)
                {
                    drawing += Environment.NewLine;
                }
                i++;
                lastInstruction = signal.lastInstruction;
            };

            return drawing;
        }

        private (int signal, bool lastInstruction) GetSignalAfterCycle(int cycle)
        {
            int regValue = 1;
            int currentCycle = 0;

            for(int i = 0; i < input.Split(Environment.NewLine).Length; i++)
            {
                string line = input.Split(Environment.NewLine)[i];
                string[] instruction = line.Split(" ");

                if (instruction[0] == "noop")
                {
                    if (++currentCycle == cycle)
                        return (regValue, (i == input.Split(Environment.NewLine).Length - 1));
                }
                else
                {
                    if (++currentCycle == cycle)
                        return (regValue, (i == input.Split(Environment.NewLine).Length - 1));

                    if (++currentCycle == cycle)
                        return (regValue, (i == input.Split(Environment.NewLine).Length - 1));

                    regValue += int.Parse(instruction[1]);
                }
            }

            return (regValue, false);
        }

    }
}
