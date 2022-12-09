using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day09 : Day
    {
        public override dynamic PartOne()
        {
            return GetTailVisitedPositions(2);
        }

        public override dynamic PartTwo()
        {
            return GetTailVisitedPositions(10);
        }

        private int GetTailVisitedPositions(int knots)
        {
            List<(int x, int y)> tailVisitedPositions = new();

            List<(int x, int y)> knotPositions = new();
            for (int i = 0; i < knots; i++)
                knotPositions.Add((0, 0));

            foreach ((char direction, int amount) instruction in (from e in input.Split(Environment.NewLine) select (e.Split(" ")[0][0], int.Parse(e.Split(" ")[1]))))
            {
                for(int i = 0; i < instruction.amount; i++)
                {
                    switch (instruction.direction)
                    {
                        case 'L':
                            knotPositions[0] = (knotPositions[0].x - 1, knotPositions[0].y);
                            break;
                        case 'R':
                            knotPositions[0] = (knotPositions[0].x + 1, knotPositions[0].y);
                            break;
                        case 'U':
                            knotPositions[0] = (knotPositions[0].x, knotPositions[0].y + 1);
                            break;
                        case 'D':
                            knotPositions[0] = (knotPositions[0].x, knotPositions[0].y - 1);
                            break;
                    }

                    for (int j = 1; j < knotPositions.Count; j++)
                    {
                        if (Math.Abs(Math.Max(knotPositions[j].x, knotPositions[j - 1].x) - Math.Min(knotPositions[j].x, knotPositions[j - 1].x)) > 1 ||
                            Math.Abs(Math.Max(knotPositions[j].y, knotPositions[j - 1].y) - Math.Min(knotPositions[j].y, knotPositions[j - 1].y)) > 1)
                        {
                            (int x, int y) newPosition = knotPositions[j];

                            if (knotPositions[j - 1].x - knotPositions[j].x > 0)
                                newPosition.x++;

                            if (knotPositions[j - 1].x - knotPositions[j].x < 0)
                                newPosition.x--;

                            if (knotPositions[j - 1].y - knotPositions[j].y > 0)
                                newPosition.y++;

                            if (knotPositions[j - 1].y - knotPositions[j].y < 0)
                                newPosition.y--;

                            knotPositions[j] = newPosition;
                        }
                    }

                    tailVisitedPositions.Add(knotPositions[knots - 1]);
                }
            }
            return tailVisitedPositions.Distinct().Count();
        }

    }

}
