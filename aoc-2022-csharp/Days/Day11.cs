using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day11 : Day
    {
        record Monkey(List<int> items, string operation, int divisableTest, int trueTarget, int falseTarget, long amountOfInspections);
        private List<Monkey> monkeys = new();

        private void InitMonkeys()
        {
            monkeys.Clear();
            foreach (string monkeyText in input.Split(Environment.NewLine + Environment.NewLine))
            {
                string[] monkeySplit = monkeyText.Split(Environment.NewLine);

                List<int> startingItems = (from e in monkeySplit[1].Trim()["Starting items: ".Length..].Split(", ") select int.Parse(e)).ToList();
                string operation = monkeySplit[2].Trim()["Operation: new = ".Length..];
                int divisableTest = int.Parse(monkeySplit[3].Trim()["Test: divisible by ".Length..]);
                int trueTarget = int.Parse(monkeySplit[4].Trim()["If true: throw to monkey ".Length..]);
                int falseTarget = int.Parse(monkeySplit[5].Trim()["If false: throw to monkey ".Length..]);
                monkeys.Add(new Monkey(startingItems, operation, divisableTest, trueTarget, falseTarget, 0));
            }
        }

        private int executeOperation(int oldValue, string operation)
        {
            operation = operation.Replace("old", oldValue.ToString());
            int x1 = int.Parse(operation.Split(" ")[0]);
            int x2 = int.Parse(operation.Split(" ")[2]);
            char operatorChar = operation.Split(" ")[1][0];

            switch(operatorChar)
            {
                case '-':
                    return x1 - x2;
                case '+':
                    return x1 + x2;
                case '*':
                    return x1 * x2;
                case '/':
                    return x1 / x2;
            }

            return -1;
        }

        public override dynamic PartOne()
        {
            InitMonkeys();
            return GetMonkeyBusinessAfterRounds(20);
        }

        public override dynamic PartTwo()
        {
            return 0;
        }

        private long GetMonkeyBusinessAfterRounds(int rounds)
        {
            for (int round = 0; round < rounds; round++)
            {
                for (int i = 0; i < monkeys.Count; i++)
                {
                    Monkey monkey = monkeys[i];
                    foreach (int item in monkey.items)
                    {
                        int worryLevel = item;
                        worryLevel = executeOperation(worryLevel, monkey.operation);

                        worryLevel /= 3;

                        if (worryLevel % monkey.divisableTest == 0)
                            monkeys[monkey.trueTarget].items.Add(worryLevel);
                        else
                            monkeys[monkey.falseTarget].items.Add(worryLevel);
                    }

                    long amountOfInspections = monkey.amountOfInspections + monkey.items.Count();
                    monkeys[i] = new(new(), monkey.operation, monkey.divisableTest, monkey.trueTarget, monkey.falseTarget, amountOfInspections);
                }
            }

            return (from monkey in monkeys orderby monkey.amountOfInspections descending select monkey.amountOfInspections).Take(2).Aggregate((x, y) => x * y);
        }

    }
}
