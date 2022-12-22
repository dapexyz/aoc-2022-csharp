using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day18 : Day
    {

        public override dynamic PartOne()
        {
            return GetSurfaceArea(false);
        }

        public override dynamic PartTwo()
        {
            return GetSurfaceArea(true);
        }

        private int GetSurfaceArea(bool p2)
        {
            int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
            foreach (string line in input.Split("\n"))
            {
                (int x, int y, int z) = (int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1]), int.Parse(line.Split(",")[2]));

                if (x > maxX)
                    maxX = x;

                if (y > maxY)
                    maxY = y;

                if (z > maxZ)
                    maxZ = z;
            }

            int[,,] grid = new int[maxX + 1, maxY + 1, maxZ + 1];
            foreach (string line in input.Split("\n"))
            {
                (int x, int y, int z) = (int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1]), int.Parse(line.Split(",")[2]));

                grid[x, y, z] = 1;
            }

            int surfaceArea = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int z = 0; z < grid.GetLength(2); z++)
                    {
                        if (grid[x, y, z] == 0)
                            continue;

                        if (x == grid.GetLength(0) - 1 || grid[x + 1, y, z] == 0)
                            if (!p2 || isExterior(grid, (x + 1, y, z)))
                                surfaceArea++;

                        if (x == 0 || grid[x - 1, y, z] == 0)
                            if (!p2 || isExterior(grid, (x - 1, y, z)))
                                surfaceArea++;

                        if (y == grid.GetLength(1) - 1 || grid[x, y + 1, z] == 0)
                            if (!p2 || isExterior(grid, (x, y + 1, z)))
                                surfaceArea++;

                        if (y == 0 || grid[x, y - 1, z] == 0)
                            if (!p2 || isExterior(grid, (x, y - 1, z)))
                                surfaceArea++;

                        if (z == grid.GetLength(2) - 1 || grid[x, y, z + 1] == 0)
                            if (!p2 || isExterior(grid, (x, y, z + 1)))
                                surfaceArea++;

                        if (z == 0 || grid[x, y, z - 1] == 0)
                            if (!p2 || isExterior(grid, (x, y, z - 1)))
                                surfaceArea++;
                    }
                }
            }

            return surfaceArea;
        }

        private bool isExterior(int[,,] grid, (int x, int y, int z) startPoint) {
            Queue<(int x, int y, int z)> pointsToCheck = new();
            pointsToCheck.Enqueue(startPoint);
            List<(int x, int y, int z)> visited = new();

            while(pointsToCheck.TryDequeue(out var point))
            {
                if (visited.Contains(point))
                    continue;

                visited.Add(point);

                if (point.x == grid.GetLength(0) || point.x <= 0)
                    return true;

                if (point.y == grid.GetLength(1) || point.y <= 0)
                    return true;

                if (point.z == grid.GetLength(2) || point.z <= 0)
                    return true;



                if (!(point.x == grid.GetLength(0) - 1 || grid[point.x + 1, point.y, point.z] == 1))
                    pointsToCheck.Enqueue((point.x + 1, point.y, point.z));

                if (!(point.x == 0 || grid[point.x - 1, point.y, point.z] == 1))
                    pointsToCheck.Enqueue((point.x - 1, point.y, point.z));

                if (point.y == grid.GetLength(1) - 1 || grid[point.x, point.y + 1, point.z] == 0)
                    pointsToCheck.Enqueue((point.x, point.y + 1, point.z));

                if (!(point.y == 0 || grid[point.x, point.y - 1, point.z] == 1))
                    pointsToCheck.Enqueue((point.x, point.y - 1, point.z));

                if (!(point.z == grid.GetLength(2) - 1 || grid[point.x, point.y, point.z + 1] == 1))
                    pointsToCheck.Enqueue((point.x, point.y, point.z + 1));

                if (!(point.z == 0 || grid[point.x, point.y, point.z - 1] == 1))
                    pointsToCheck.Enqueue((point.x, point.y, point.z - 1));
            }

            return false;
        }
    }
}
