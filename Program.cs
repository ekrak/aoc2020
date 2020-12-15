using AdventOfCode2020_1.DayResolvers.Day14;
using AdventOfCode2020_1.DayResolvers.Day15;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = 15;
            var task = 1;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day15Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 2));
        }
    }
}