using AdventOfCode2020_1.DayResolvers.Day16;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = 16;
            var task = 1;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day16Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 2));
        }
    }
}