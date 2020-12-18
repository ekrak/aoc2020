using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day18
{
    public class Day18Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            List<MathGroup> mathGroups = new List<MathGroup>();
            string line = input.ReadLine();
            while (line != null)
            {
                mathGroups.Add(new MathGroup(line, out int i));
                line = input.ReadLine();
            }

            if (task == 1) return Resolve1Internal(mathGroups);

            return Resolve2Internal(mathGroups);
        }

        private string Resolve2Internal(List<MathGroup> mathGroups)
        {
            long sum = 0;
            mathGroups.ForEach(mg => sum += mg.Evaluate2());
            return sum.ToString();
        }

        private string Resolve1Internal(List<MathGroup> mathGroups)
        {
            long sum = 0;
            mathGroups.ForEach(mg => sum += mg.Evaluate());
            return sum.ToString();
        }
    }
}