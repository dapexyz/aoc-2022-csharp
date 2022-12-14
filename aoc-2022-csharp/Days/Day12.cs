using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day12 : Day
    {
        private int[,] heightMap;
        record Point(int row, int column, int distanceFromStart);
        Point startPoint, endPoint;

        public Day12()
        {
            heightMap = new int[input.Split(Environment.NewLine).Length, input.Split(Environment.NewLine)[0].Length];

            for(int row = 0; row < heightMap.GetLength(0); row++)
            {
                for(int column = 0; column < heightMap.GetLength(1); column++)
                {
                    char heightLetter = input.Split(Environment.NewLine)[row][column];
                    if (heightLetter == 'S')
                    {
                        heightMap[row, column] = 0;
                        startPoint = new(row, column, 0);
                    }
                    else if(heightLetter == 'E')
                    {
                        heightMap[row, column] = 'z' - 'a';
                        endPoint = new(row, column, 0);
                    }
                    else
                    {
                        heightMap[row, column] = heightLetter - 'a';
                    }
                }
            }
        }

        public override dynamic PartOne()
        {
            return GetDistance(startPoint, endPoint);
        }

        public override dynamic PartTwo() 
        {
            return GetDistance(null, endPoint);
        }

        private int GetDistance(Point? start, Point end)
        {
            int minDistance = int.MaxValue;
            bool[,] visited = new bool[heightMap.GetLength(0), heightMap.GetLength(1)];

            Queue<Point> pointsToCheck = new();
            
            if(start != null)
                pointsToCheck.Enqueue(start);
            else
                for(int row = 0; row < heightMap.GetLength(0); row++)
                    for(int column = 0; column < heightMap.GetLength(1); column++)
                        if (heightMap[row, column] == 0)
                            pointsToCheck.Enqueue(new Point(row, column, 0));

            while (pointsToCheck.TryDequeue(out var point))
            {
                if (point.row == end.row && point.column == end.column)
                {
                    minDistance = Math.Min(point.distanceFromStart, minDistance);
                    continue;
                }

                List<Point> neighbours = new() {
                    new Point(point.row - 1, point.column, point.distanceFromStart + 1),
                    new Point(point.row + 1, point.column, point.distanceFromStart + 1),
                    new Point(point.row, point.column + 1, point.distanceFromStart + 1),
                    new Point(point.row, point.column - 1, point.distanceFromStart + 1)
                };

                int currentHeight = heightMap[point.row, point.column];
                foreach (Point neighbour in neighbours)
                {
                    if (neighbour.row >= 0 && neighbour.row < heightMap.GetLength(0) &&
                       neighbour.column >= 0 && neighbour.column < heightMap.GetLength(1) &&
                       heightMap[neighbour.row, neighbour.column] <= currentHeight + 1 &&
                       !visited[neighbour.row, neighbour.column])
                    {
                        visited[neighbour.row, neighbour.column] = true;
                        pointsToCheck.Enqueue(neighbour);
                    }
                }
            }

            return minDistance;
        }
    }
}
