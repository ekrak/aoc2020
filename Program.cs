using AdventOfCode2020_1.DayResolvers.Day16;
using AdventOfCode2020_1.DayResolvers.Day17;
using AdventOfCode2020_1.DayResolvers.Day18;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = 17;
            var task = 1;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day17Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 2));
        }
    }
}