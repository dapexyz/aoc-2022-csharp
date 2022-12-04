namespace aoc_2022_csharp.Days
{
    internal class Day04 : Day
    {
        private int[] BuildRangeFromString(string range) =>
            Enumerable.Range(int.Parse(range.Split('-')[0]), int.Parse(range.Split('-')[1]) - int.Parse(range.Split('-')[0]) + 1).ToArray();

        public override dynamic PartOne()
        {
            return (from line in input.Split(Environment.NewLine)
                    let firstRange = BuildRangeFromString(line.Split(',')[0])
                    let secondRange = BuildRangeFromString(line.Split(',')[1])
                    where firstRange.All(x => secondRange.Contains(x) || secondRange.All(x => firstRange.Contains(x)))
                    select line).Count();
        }

        public override dynamic PartTwo()
        {
            return (from line in input.Split(Environment.NewLine)
                    let firstRange = BuildRangeFromString(line.Split(',')[0])
                    let secondRange = BuildRangeFromString(line.Split(',')[1])
                    where firstRange.Any(x => secondRange.Contains(x) || secondRange.All(x => firstRange.Contains(x)))
                    select line).Count();
        }
    }
}
