using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class Day17Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            CubePocket cp = new CubePocket(input);
            CubePocket4 cp2 = new CubePocket4(cp);
            if (task == 1) return Resolve1Internal(cp);

            return Resolve2Internal(cp2);
        }

        private string Resolve2Internal(CubePocket4 input)
        {
            return input.GetActiveCubes(6);
        }

        private string Resolve1Internal(CubePocket input)
        {
            return input.GetActiveCubes(6);
        }
    }
}