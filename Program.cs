using AdventOfCode2020_1.DayResolvers.Day9;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = 9;
            var task = 0;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day9Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 1));
        }
    }
}