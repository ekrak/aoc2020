using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day12
{
    public class Day12Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            FerryDistance dist = new FerryDistance(input);
            if (task == 1) return Resolve1Internal(dist);

            return Resolve2Internal(dist);
        }

        private string Resolve2Internal(FerryDistance dist)
        {
            return dist.GetManhattanDistance2().ToString();
        }

        private string Resolve1Internal(FerryDistance dist)
        {
            return dist.GetManhattanDistance().ToString();
        }
    }
}