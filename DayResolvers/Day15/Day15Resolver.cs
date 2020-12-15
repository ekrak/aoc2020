using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day15
{
    public class Day15Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            MemoryGame dp = new MemoryGame(input);
            if (task == 1) return Resolve1Internal(dp);

            return Resolve2Internal(dp);
        }

        private string Resolve2Internal(MemoryGame input)
        {
            return input.Get30000000().ToString();
        }

        private string Resolve1Internal(MemoryGame input)
        {
            return input.Get2020().ToString();
        }
    }
}