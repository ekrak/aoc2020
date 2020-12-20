using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day19
{
    public class Day19Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            Validator dp = new Validator(input);
            if (task == 1) return Resolve1Internal(dp);

            return Resolve2Internal(dp);
        }

        private string Resolve2Internal(Validator input)
        {
            return input.GetValidLines().ToString();
        }

        private string Resolve1Internal(Validator input)
        {
            return input.GetValidLines().ToString();
        }
    }
}