namespace aoc_2022_csharp.Days
{
    internal class Day15 : Day
    {
        public override dynamic PartOne()
        {
            List<(int r, int c)> targetsBlocked = new();
            List<(int r, int c)> beacons = new();
            int targetC = 2000000;

            foreach (string line in input.Split("\n"))
            {
                (int r, int c) sensorPos = (int.Parse(line.Split("=")[1].Split(",")[0]), int.Parse(line.Split("=")[2].Split(":")[0]));
                (int r, int c) beaconPos = (int.Parse(line.Split("=")[3].Split(",")[0]), int.Parse(line.Split("=")[4]));

                int distanceToBeacon = Math.Abs(sensorPos.r - beaconPos.r) + Math.Abs(sensorPos.c - beaconPos.c);
                int differenceToTarget = Math.Abs(sensorPos.c - targetC);
                int difference = distanceToBeacon - differenceToTarget;

                for (int i = sensorPos.r - difference; i <= sensorPos.r + difference; i++)
                    targetsBlocked.Add((i, targetC));
                
                beacons.Add((beaconPos.r, beaconPos.c));
            }

            return targetsBlocked.Distinct().Count() - beacons.Distinct().Where(beacon => beacon.c == targetC).Count();
        }

        public override dynamic PartTwo()
        {
            Dictionary<int, List<int>> rowsToCheck = new();

            for(int targetC = 0; targetC <= 20; targetC++)
            {
                List<(int r, int c)> targetsBlocked = new();
                List<(int r, int c)> beacons = new();

                foreach (string line in input.Split("\n"))
                {
                    (int r, int c) sensorPos = (int.Parse(line.Split("=")[1].Split(",")[0]), int.Parse(line.Split("=")[2].Split(":")[0]));
                    (int r, int c) beaconPos = (int.Parse(line.Split("=")[3].Split(",")[0]), int.Parse(line.Split("=")[4]));

                    int distanceToBeacon = Math.Abs(sensorPos.r - beaconPos.r) + Math.Abs(sensorPos.c - beaconPos.c);
                    int differenceToTarget = Math.Abs(sensorPos.c - targetC);
                    int difference = distanceToBeacon - differenceToTarget;

                    for (int i = sensorPos.r - difference; i <= sensorPos.r + difference; i++)
                        targetsBlocked.Add((i, targetC));

                    beacons.Add((beaconPos.r, beaconPos.c));
                }

                foreach ((int r, int c) beacon in beacons.Distinct().Where(b => b.c == targetC))
                    targetsBlocked.Add(beacon);

                List<int> blocked = targetsBlocked.Distinct().Where(x => x.r <= 4_000_000).Select(x => x.r).ToList();
                blocked.Sort();

                rowsToCheck.Add(targetC, blocked);
            }

            for (int i = 0; i < rowsToCheck.Count; i++)
            {
                List<int> pair = rowsToCheck[i];
                foreach ((int c1, int c2) in Enumerable.Zip(pair, pair.ToArray()[1..].ToList()))
                {
                    if (c2 > c1 + 1)
                        return i + (c1 + 1) * 4000000;
                }
                //Console.WriteLine(pair.Key + ": " + string.Join(", ", pair.Value));
            }

            //TODO: works for example but not for custom input?
            return -1;
        }
    }
}
