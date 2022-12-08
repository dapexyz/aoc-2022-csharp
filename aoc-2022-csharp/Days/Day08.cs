using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day08 : Day
    {
        private readonly int[,] grid;

        public Day08()
        {
            string[] splitted = input.Split(Environment.NewLine);
            grid = new int[splitted[0].Length, splitted.Length];

            for (int i = 0; i < splitted.Length; i++)
                for (int j = 0; j < splitted[i].Length; j++)
                    grid[i, j] = int.Parse(splitted[i][j].ToString());
        }

        public override dynamic PartOne()
        {
            return (from i in Enumerable.Range(0, grid.GetLength(0))
                    from j in Enumerable.Range(0, grid.GetLength(1))
                    where CheckEdge(i, j) || CheckRow(i, j) || CheckColumn(i, j)
                    select 0).Count();
        }

        public override dynamic PartTwo()
        {
            return (from i in Enumerable.Range(0, grid.GetLength(0))
                    from j in Enumerable.Range(0, grid.GetLength(1))
                    select GetScenicScore(i, j)).Max();
        }

        private int GetScenicScore(int x, int y)
        {
            int viewLeft = 0;
            int viewRight = 0;
            int viewUp = 0;
            int viewDown = 0;

            for (int i = x - 1; i >= 0; i--)
            {
                viewLeft++;
                if (grid[i, y] >= grid[x, y])
                    break;
            }

            for(int i = x + 1; i < grid.GetLength(0); i++)
            {
                viewRight++;
                
                if (grid[i, y] >= grid[x, y])
                    break;
            }

            for(int i = y - 1; i >= 0; i--)
            {
                viewUp++;

                if (grid[x, i] >= grid[x, y])
                    break;
            }

            for(int i = y + 1; i < grid.GetLength(1); i++)
            {
                viewDown++;

                if (grid[x, i] >= grid[x, y])
                    break;
            }

            return viewUp * viewLeft * viewRight * viewDown;
        }

        private bool CheckEdge(int x, int y) => (x == 0 || x == grid.GetLength(0) - 1 || y == 0 || y == grid.GetLength(1) - 1);

        private bool CheckRow(int x, int y) => (GetRow(grid, x)[0..y].All(height => height < grid[x, y]) || GetRow(grid, x)[(y + 1)..].All(height => height < grid[x, y]));

        private bool CheckColumn(int x, int y) => (GetColumn(grid, y)[0..x].All(height => height < grid[x, y]) || GetColumn(grid, y)[(x + 1)..].All(height => height < grid[x, y]));

        private int[] GetRow(int[,] input, int x) => (from y in Enumerable.Range(0, input.GetLength(0)) select input[x, y]).ToArray();
        private int[] GetColumn(int[,] input, int y) => (from x in Enumerable.Range(0, input.GetLength(1)) select input[x, y]).ToArray();

    }
}
