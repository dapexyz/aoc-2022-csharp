using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day05 : Day
    {
        private Dictionary<int, Stack<string>> boxes = new();
        private List<(int amount, int from, int to)> instructions = new();

        private void InitInstructions()
        {
            instructions = new();

            foreach (string line in input.Split(Environment.NewLine))
            {
                Regex regex = new("move (\\d+) from (\\d+) to (\\d+)");

                if (!regex.IsMatch(line))
                    continue;

                GroupCollection groups = regex.Matches(line)[0].Groups;

                instructions.Add((int.Parse(groups[1].ToString()), int.Parse(groups[2].ToString()), int.Parse(groups[3].ToString())));
            }

        }

        private void InitBoxes()
        {
            Regex boxMatcher = new("( ?[A-Z] ?)|( ?   )");
            Dictionary<int, List<string>> initialBoxes = new();
            foreach (string line in input.Split(Environment.NewLine))
            {
                if (line.Any(char.IsDigit))
                    break;

                MatchCollection matches = boxMatcher.Matches(line);
                for (int i = 0; i < matches.Count; i++)
                {
                    List<string> list = initialBoxes.GetValueOrDefault(i, new List<string>());
                    list.Add(matches[i].ToString().Trim());
                    initialBoxes[i] = list;
                }
            }

            boxes = new();
            for (int i = 0; i < initialBoxes.Count; i++)
            {
                if (!boxes.ContainsKey(i))
                    boxes.Add(i, new());

                initialBoxes[i].Reverse();

                foreach (string box in initialBoxes[i])
                    if (box != "")
                        boxes[i].Push(box);
            }

            InitInstructions();

        }

        public override dynamic PartOne()
        {
            InitBoxes();

            foreach ((int amount, int from, int to) in instructions)
                for (int i = 0; i < amount; i++)
                    boxes[to - 1].Push(boxes[from - 1].Pop());

            return string.Join("", from e in boxes select e.Value.Peek());
        }

        public override dynamic PartTwo()
        {
            InitBoxes();

            foreach ((int amount, int from, int to) in instructions)
            {
                List<string> boxesToMove = new();
                for (int i = 0; i < amount; i++)
                    boxesToMove.Add(boxes[from - 1].Pop());
                boxesToMove.Reverse();

                Array.ForEach(boxesToMove.ToArray(), box => boxes[to - 1].Push(box));
            }

            return string.Join("", from e in boxes select e.Value.Peek());
        }
    }
}
