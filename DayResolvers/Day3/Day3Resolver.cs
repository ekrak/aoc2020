using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day3
{
    public class Day3Resolver
    {
        List<Tuple<int,int>> task2Tuples = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(1,1),
            new Tuple<int, int>(3,1),
            new Tuple<int, int>(5,1),
            new Tuple<int, int>(7,1),
            new Tuple<int, int>(1,2)
        };

        public string Resolve(StreamReader input, int task)
        {
            if (task == 1)
            {
                return Resolve1Internal(input);

            }

            return Resolve2Internal(input);

        }

        private string Resolve2Internal(StreamReader input)
        {
            long result = 1;
            Map map1 = new Map(input, new Tuple<int, int>(0, 0), new Tuple<int, int>(1, 1));
            foreach (var task2Tuple in task2Tuples)
            {
                map1.Reset();
                map1.Increment = task2Tuple;
                long tempResult = (long)map1.Resolve1();
                result = result * tempResult;
            }

            return result.ToString();
        }

        private string Resolve1Internal(StreamReader input)
        {
            Map map = new Map(input, new Tuple<int, int>(0,0), new Tuple<int, int>(3, 1));
            return map
    ve1().ToString();
        }
    }
}
