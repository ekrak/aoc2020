using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day13
{
    public class Day13Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            BusSchedule bs = new BusSchedule(input);
            if (task == 1) return Resolve1Internal(bs);

            return Resolve2Internal(bs);
        }

        private string Resolve2Internal(BusSchedule input)
        {
            //return input.BruteForce().ToString();
            return input.FindTimestamp().ToString();
        }

        private string Resolve1Internal(BusSchedule input)
        {
            return input.GetClosestBus().ToString();
        }
    }
}