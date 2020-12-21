using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day20
{
    public class Day20Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            var squareImage = new SquareImage(input);
            if (task == 1) return Resolve1Internal(squareImage);

            return Resolve2Internal(squareImage);
        }

        private string Resolve2Internal(SquareImage input)
        {
            return null;
        }

        private string Resolve1Internal(SquareImage input)
        {
            return null;
        }
    }
}