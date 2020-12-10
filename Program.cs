using AdventOfCode2020_1.DayResolvers.Day11;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = 11;
            var task = 0;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day11Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 1));
        }
    }
}