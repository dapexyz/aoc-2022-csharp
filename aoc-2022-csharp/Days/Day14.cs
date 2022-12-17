using System.Numerics;

namespace aoc_2022_csharp.Days
{
    internal class Day14 : Day
    {
        private List<(int r, int c)> wallList = new(), sandList = new();
        private (int r, int c) startPoint = (0, 500);
        private int maxR = -1;

        //TODO: Optimize (+3min exec. time 🤡)

        public override dynamic PartOne()
        {
            InitWalls();

            bool canPlaceSand = true;
            while (canPlaceSand)
            {
                Stack<(int r, int c)> pointsToCheck = new();
                pointsToCheck.Push(startPoint);

                while (pointsToCheck.TryPop(out (int r, int c) point))
                {
                    if (IsInVoid(point))
                    {
                        canPlaceSand = false;
                        break;
                    }

                    pointsToCheck.Clear();

                    (int r, int c) left = (point.r + 1, point.c - 1);
                    (int r, int c) down = (point.r + 1, point.c);
                    (int r, int c) right = (point.r + 1, point.c + 1);

                    if (!wallList.Contains(down) && !sandList.Contains(down))
                        pointsToCheck.Push(down);
                    else if (!wallList.Contains(left) && !sandList.Contains(left))
                        pointsToCheck.Push(left);
                    else if (!wallList.Contains(right) && !sandList.Contains(right))
                        pointsToCheck.Push(right);
                    else
                        sandList.Add(point);
                }
            }

            return sandList.Count;
        }

        public override dynamic PartTwo()
        {
            InitWalls();

            bool canPlaceSand = true;
            while (canPlaceSand)
            {
                Stack<(int r, int c)> pointsToCheck = new();
                pointsToCheck.Push(startPoint);

                while (pointsToCheck.TryPop(out (int r, int c) point))
                {
                    pointsToCheck.Clear();

                    if (point.r >= maxR + 1)
                    {
                        sandList.Add(point);
                        continue;
                    }

                    (int r, int c) left = (point.r + 1, point.c - 1);
                    (int r, int c) down = (point.r + 1, point.c);
                    (int r, int c) right = (point.r + 1, point.c + 1);

                    if (!wallList.Contains(down) && !sandList.Contains(down))
                        pointsToCheck.Push(down);
                    else if (!wallList.Contains(left) && !sandList.Contains(left))
                        pointsToCheck.Push(left);
                    else if (!wallList.Contains(right) && !sandList.Contains(right))
                        pointsToCheck.Push(right);
                    else
                    {
                        sandList.Add(point);
                        if (point == startPoint)
                            canPlaceSand = false;
                    }
                }
            }

            return sandList.Count;
        }


        private bool IsInVoid((int r, int c) p)
        {
            if (wallList.Where(x => x.r > p.r).Count() > 0)
                return false;

            if (sandList.Where(x => x.r > p.r).Count() > 0)
                return false;

            return true;
        }

        private void InitWalls()
        {
            wallList.Clear();
            sandList.Clear();

            foreach (string line in input.Split("\n"))
            {
                string[] splitted = line.Split(" -> ");
                foreach ((string from, string to) in Enumerable.Zip(splitted, splitted[1..]))
                {
                    (int r, int c) first = (int.Parse(from.Split(",")[1]), int.Parse(from.Split(",")[0]));
                    (int r, int c) second = (int.Parse(to.Split(",")[1]), int.Parse(to.Split(",")[0]));

                    for (int r = Math.Min(first.r, second.r); r <= Math.Max(first.r, second.r); r++)
                        for (int c = Math.Min(first.c, second.c); c <= Math.Max(first.c, second.c); c++)
                            wallList.Add((r, c));
                }
            }

            maxR = wallList.Max(x => x.r);
        }

    }
}
