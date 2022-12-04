using aoc_2022_csharp.Days;
using aoc_2022_csharp.Properties;
using System.Reflection;

namespace aoc_2022_csharp
{
    internal class Program
    {
        private 

        static void Main1()
        {
            RunDay(new Day04());
        }

        static void Main()
        {
            var days = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.FullName!.StartsWith(typeof(Program).Namespace + ".Days.Day") && t.DeclaringType == null);

            foreach (var day in days)
                Console.WriteLine(day.FullName);

            if(!Convert.ToBoolean(Resources.RunAll))
                RunDay((Day)Activator.CreateInstance(days.Where(x => int.Parse(x.Name[3..]) == int.Parse(Resources.DayToRun)).First())!);
            else
                foreach (var day in days)
                    RunDay((Day)Activator.CreateInstance(day)!);
        }

        static void RunDay(Day day)
        {
            int dayNumber = int.Parse(day.GetType().Name[3..]);
            day.SetInput(File.ReadAllText($"Inputs/{dayNumber}.txt"));
            Console.WriteLine("Day " + dayNumber);
            Console.WriteLine("Part 1: " + day.PartOne());
            Console.WriteLine("Part 2: " + day.PartTwo());
            Console.WriteLine();
        }
    }
}
