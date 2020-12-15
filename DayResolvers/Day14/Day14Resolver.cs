using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day14
{
    public class Day14Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            DockingProgram dp = new DockingProgram(input);
            if (task == 1) return Resolve1Internal(dp);

            return Resolve2Internal(dp);
        }

        private string Resolve2Internal(DockingProgram input)
        {
            return input.GetFinalSum2().ToString();
        }

        private string Resolve1Internal(DockingProgram input)
        {
            return input.GetFinalSum().ToString();
        }
    }
}