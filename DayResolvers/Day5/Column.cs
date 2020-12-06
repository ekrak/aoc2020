using System;

namespace AdventOfCode2020_1.DayResolvers.Day5
{
    public class Column : BaseRow
    {
        public Column(string input) : base(input)
        {
        }

        public override Tuple<int, int> InitialRange { get; set; } = new Tuple<int, int>(0, 7);
        public override char LowerLetter { get; set; } = 'L';
        public override char UpperLetter { get; set; } = 'R';
    }
}