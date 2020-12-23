using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day22
{
    public class Day22Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            var game = new Game(input);
            if (task == 1) return Resolve1Internal(game);

            return Resolve2Internal(game);
        }

        private string Resolve2Internal(Game input)
        {
            return null;
        }

        private string Resolve1Internal(Game input)
        {
            return input.Play().ToString();
        }
    }
}