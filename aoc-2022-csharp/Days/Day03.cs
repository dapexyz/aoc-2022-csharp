using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day03 : Day
    {
        private string alphabet = string.Empty;

        public Day03()
        {
            for (char letter = 'a'; letter <= 'z'; letter++)
                alphabet += letter;

            for (char letter = 'A'; letter <= 'Z'; letter++)
                alphabet += letter;
        }

        private int GetPriority(char input) => alphabet.IndexOf(input) + 1;

        public override dynamic PartOne()
        {
            int sum = 0;

            foreach (string rucksack in input.Split(Environment.NewLine))
                sum += GetPriority(rucksack.Where(x => rucksack[..(rucksack.Length / 2)].Contains(x) && rucksack[(rucksack.Length / 2)..].Contains(x)).First());

            return sum;
        }

        public override dynamic PartTwo()
        {
            int sum = 0;

            string[] split = input.Split(Environment.NewLine);
            while(split.Length > 0)
            {
                sum += GetPriority(split[0].Where(x => split[1].Contains(x) && split[2].Contains(x)).First());
                split = split.Skip(3).ToArray();
            }

            return sum;
        }

    }
}
