﻿using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day9
{
    public class Day9Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            if (task == 1) return Resolve1Internal(input);

            return Resolve2Internal(input);
        }

        private string Resolve2Internal(StreamReader input)
        {
            Code code = new Code(input, 25);
            var ew =  code.GetEncryptionWeakness();
            return (ew.Min() + ew.Max()).ToString();
        }

        private string Resolve1Internal(StreamReader input)
        {
            Code code = new Code(input, 25);
            return code.GetFirstWrong().ToString();
        }
    }
}