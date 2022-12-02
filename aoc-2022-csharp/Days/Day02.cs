using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day02 : Day
    {
        public override dynamic PartOne() => (from result in input.Split(Environment.NewLine) select CalculateRound(result[0], result[2])).Sum();

        public override dynamic PartTwo() => (from result in input.Split(Environment.NewLine) select CalculateRound(result[0], GetMatchingResponse(result[0], result[2]))).Sum();

        private char GetMatchingResponse(char shapeOpponent, char outcome)
        {
            foreach (int i in Enumerable.Range(0, 3))
            {
                char response = (char)('X' + i);
                if (GetOutcome(shapeOpponent, response) / 3 == outcome - 'X')
                    return response;
            }

            Debug.Assert(false);
            return '\u0000';
        }

        private int GetOutcome(char shapeOpponent, char shapeResponse)
        {
            int response = shapeResponse - 'X';
            int opponent = shapeOpponent - 'A';

            if (opponent == response)
                return 3;
            else if (Math.Abs((opponent + 1) % 3) == response)
                return 6;
            else
                return 0;
        }

        private int CalculateRound(char shapeOpponent, char shapeResponse) => shapeResponse - 'X' + 1 + GetOutcome(shapeOpponent, shapeResponse);
    }
}
