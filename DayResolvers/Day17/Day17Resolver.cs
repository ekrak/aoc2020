using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class Day17Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            CubePocket dp = new CubePocket(input);
            if (task == 1) return Resolve1Internal(dp);

            return Resolve2Internal(dp);
        }

        private string Resolve2Internal(CubePocket input)
        {
            return null;
        }

        private string Resolve1Internal(CubePocket input)
        {
            return input.GetActiveCubes(6);
        }
    }
}