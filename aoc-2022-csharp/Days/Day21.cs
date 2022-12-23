using AngouriMath;
using System.Diagnostics;
using System.Numerics;

namespace aoc_2022_csharp.Days
{
    internal class Day21 : Day
    {
        //TODO: 4+GB RAM 🤡
        public override dynamic PartOne()
        {
            return GetSolution(false);
        }

        public override dynamic PartTwo()
        {
            return GetSolution(true);
        }

        private string GetSolution(bool p2)
        {
            Dictionary<string, string> instructionMonkeys = new();

            foreach (string line in input.Split("\n"))
            {
                string monkeyName = line.Split(": ")[0];
                string content = line.Split(": ")[1];

                if (p2 && monkeyName == "humn")
                    content = "humn";

                instructionMonkeys.Add(monkeyName, content);
            }

            string calc = instructionMonkeys["root"];

            if (p2)
                calc = calc.Replace(" + ", " = ");
            else
                calc = "root = " + calc;

            instructionMonkeys.Remove("root");
            while (instructionMonkeys.Count > 0)
            {
                foreach (string monkey in calc.Split())
                {
                    if (instructionMonkeys.ContainsKey(monkey))
                    {
                        calc = calc.Replace(monkey, $"( {instructionMonkeys[monkey]} )");
                        instructionMonkeys.Remove(monkey);
                        break;
                    }
                }
            }

            Entity expr = calc;
            if(p2)
                return expr.Solve("humn").ToString().Split()[1];
            else
                return expr.Solve("root").ToString().Split()[1];
        }
    }
}
