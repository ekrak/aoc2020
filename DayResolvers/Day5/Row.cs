using System;

namespace AdventOfCode2020_1.DayResolvers.Day5
{
    public class Row : BaseRow
    {
        public Row(string input) : base(input) { }

        public override Tuple<int, int> InitialRange { get; set; } = new Tuple<int, int>(0,127);
        public override char LowerLetter { get; set; } = 'F';
        public override char UpperLetter { get; set; } = 'B';
    }
}
