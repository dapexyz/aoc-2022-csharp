using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace aoc_2022_csharp.Days
{
    internal class Day15 : Day
    {
        public override dynamic PartOne()
        {
            HashSet<int> targetsBlocked = new(), beacons = new();
            int targetC = 2_000_000;

            foreach (string line in input.Split("\n"))
            {
                (int r, int c) sensorPos = (int.Parse(line.Split("=")[1].Split(",")[0]), int.Parse(line.Split("=")[2].Split(":")[0]));
                (int r, int c) beaconPos = (int.Parse(line.Split("=")[3].Split(",")[0]), int.Parse(line.Split("=")[4]));

                int distanceToBeacon = Math.Abs(sensorPos.r - beaconPos.r) + Math.Abs(sensorPos.c - beaconPos.c);
                int differenceToTarget = Math.Abs(sensorPos.c - targetC);
                int difference = distanceToBeacon - differenceToTarget;

                for (int i = sensorPos.r - difference; i <= sensorPos.r + difference; i++)
                    targetsBlocked.Add(i);
                
                if(beaconPos.c == targetC)
                    beacons.Add(beaconPos.r);
            }

            return targetsBlocked.Count() - beacons.Count();
        }

        public override dynamic PartTwo()
        {
            int maxC = 4_000_000;
            //maxC = 20;

            for (int targetC = 0; targetC <= maxC; targetC++)
            {
                List<(int from, int to)> intervals = new();

                foreach (string line in input.Split("\n"))
                {
                    (int r, int c) sensorPos = (int.Parse(line.Split("=")[1].Split(",")[0]), int.Parse(line.Split("=")[2].Split(":")[0]));
                    (int r, int c) beaconPos = (int.Parse(line.Split("=")[3].Split(",")[0]), int.Parse(line.Split("=")[4]));

                    int distanceToBeacon = Math.Abs(sensorPos.r - beaconPos.r) + Math.Abs(sensorPos.c - beaconPos.c);
                    int differenceToTarget = Math.Abs(sensorPos.c - targetC);
                    int difference = distanceToBeacon - differenceToTarget;

                    if (difference >= 0)
                        intervals.Add((sensorPos.r - difference, sensorPos.r + difference));
                }

                intervals.Sort();

                (int from, int to) completeInterval = (int.MinValue, int.MinValue);
                foreach((int from, int to) in intervals)
                    if (completeInterval == (int.MinValue, int.MinValue))
                        completeInterval = (from, to);
                    else if (from <= completeInterval.to + 1)
                        completeInterval = (completeInterval.from, Math.Max(completeInterval.to, to));
                    else
                        return (BigInteger)(completeInterval.to + 1) * 4_000_000 + targetC;
            }

            Debug.Assert(false);
            return -1;
        }
    }
}
