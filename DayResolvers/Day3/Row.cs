using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day3
{
    public class Row
    {
        public string Input { get; set; }

        public Row(string input)
        {
            Input = input;
        }

        public bool IsOpen(int position)
        {
            int modulo = position % Input.Length;
            return Input[modulo] == '.';
        }
    }
}
