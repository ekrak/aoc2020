using AdventOfCode2020_1.DayResolvers.Day12;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = 12;
            var task = 0;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day12Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 1));
        }
    }
}