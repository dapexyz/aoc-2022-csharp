using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day23 : Day
    {
        private List<Elf> elves = new();

        record Elf
        {
            public (int r, int c) pos;

            public Elf((int r, int c) pos)
            {
                this.pos = pos;
            }
        };

        private List<(int r, int c)> directions = new();

        //TODO: Optimize (+5min exec. time)
        public override dynamic PartOne()
        {
            Init();

            for(int round = 1; round <= 10; round++)
            {
                // first half
                Dictionary<Elf, (int r, int c)> newPositions = new();
                foreach (Elf elf in elves)
                {
                    bool isIsolated = true;
                    foreach ((int r, int c) dir in directions)
                    {
                        if (elves.Where(e => e.pos == (elf.pos.r + dir.r, elf.pos.c + dir.c)).Count() > 0)
                        {
                            isIsolated = false;
                            break;
                        }
                    }

                    if (isIsolated)
                        continue;

                    for(int i = 0; i < directions.Count; i+=3)
                    {
                        (int r, int c) dir1 = directions[i];
                        (int r, int c) dir2 = directions[i + 1];
                        (int r, int c) dir3 = directions[i + 2];

                        (int r, int c) pos1 = (elf.pos.r + dir1.r, elf.pos.c + dir1.c);
                        (int r, int c) pos2 = (elf.pos.r + dir2.r, elf.pos.c + dir2.c);
                        (int r, int c) pos3 = (elf.pos.r + dir3.r, elf.pos.c + dir3.c);

                        if (elves.Where(e => e.pos == pos1 || e.pos == pos2 || e.pos == pos3).Count() > 0)
                            continue;

                        newPositions.Add(elf, (elf.pos.r + dir1.r, elf.pos.c + dir1.c));
                        break;
                    }
                }

                //second half
                foreach (var pair in newPositions)
                {
                    if (newPositions.Values.Where(x => x == pair.Value).Count() > 1)
                        continue;

                    pair.Key.pos = pair.Value;
                }

                // end of round
                directions.Add(directions[0]);
                directions.Add(directions[1]);
                directions.Add(directions[2]);
                directions.RemoveAt(0);
                directions.RemoveAt(0);
                directions.RemoveAt(0);
            }

            int minR = elves.Min(x => x.pos.r);
            int maxR = elves.Max(x => x.pos.r);
            int minC = elves.Min(x => x.pos.c);
            int maxC = elves.Max(x => x.pos.c);
            int ret = 0;

            for (int r = minR; r <= maxR; r++)
                for (int c = minC; c <= maxC; c++)
                    if (!elves.Select(x => x.pos).Contains((r, c)))
                        ret++;

            return ret;
        }

        public override dynamic PartTwo()
        {
            Init();
            bool moving = true;

            int round = 0;
            while(moving)
            {
                round++;
                // first half
                Dictionary<Elf, (int r, int c)> newPositions = new();
                foreach (Elf elf in elves)
                {
                    bool isIsolated = true;
                    foreach ((int r, int c) dir in directions)
                    {
                        if (elves.Where(e => e.pos == (elf.pos.r + dir.r, elf.pos.c + dir.c)).Count() > 0)
                        {
                            isIsolated = false;
                            break;
                        }
                    }

                    if (isIsolated)
                        continue;

                    for (int i = 0; i < directions.Count; i += 3)
                    {
                        (int r, int c) dir1 = directions[i];
                        (int r, int c) dir2 = directions[i + 1];
                        (int r, int c) dir3 = directions[i + 2];

                        (int r, int c) pos1 = (elf.pos.r + dir1.r, elf.pos.c + dir1.c);
                        (int r, int c) pos2 = (elf.pos.r + dir2.r, elf.pos.c + dir2.c);
                        (int r, int c) pos3 = (elf.pos.r + dir3.r, elf.pos.c + dir3.c);

                        if (elves.Where(e => e.pos == pos1 || e.pos == pos2 || e.pos == pos3).Count() > 0)
                            continue;

                        newPositions.Add(elf, (elf.pos.r + dir1.r, elf.pos.c + dir1.c));
                        break;
                    }
                }

                //second half
                moving = false;

                foreach (var pair in newPositions)
                {
                    if (newPositions.Values.Where(x => x == pair.Value).Count() > 1)
                        continue;

                    pair.Key.pos = pair.Value;
                    moving = true;
                }

                // end of round
                directions.Add(directions[0]);
                directions.Add(directions[1]);
                directions.Add(directions[2]);
                directions.RemoveAt(0);
                directions.RemoveAt(0);
                directions.RemoveAt(0);
            }

            return round;
        }

        private void Init()
        {
            elves.Clear();
            for (int r = 0; r < input.Split("\n").Length; r++)
            {
                string line = input.Split("\n")[r];
                for (int c = 0; c < line.Length; c++)
                    if (line[c] == '#')
                        elves.Add(new((r, c)));
            }

            directions = new()
            {
                // north
                { (-1, 0) },
                { (-1, -1) },
                { (-1, 1) },

                // south
                { (1, 0) },
                { (1, -1) },
                { (1, 1) },

                // west
                { (0, -1) },
                { (1, -1) },
                { (-1, -1) },

                // east
                { (0, 1) },
                { (1, 1) },
                { (-1, 1) },
            };
        }
    }
}
