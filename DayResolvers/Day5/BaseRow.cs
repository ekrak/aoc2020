using System;

namespace AdventOfCode2020_1.DayResolvers.Day5
{
    public abstract class BaseRow
    {

        public string Input { get; set; }

        public abstract Tuple<int,int> InitialRange { get; set; }

        public abstract char LowerLetter { get; set; }

        public abstract char UpperLetter { get; set; }

        private int? _value = null;

        public int Value
        {
            get
            {
                if (_value == null) _value = GetRow();
                return (int)_value;
            }
        }

        public BaseRow(string input)
        {
            Input = input;
        }

        private int GetRow()
        {
            Tuple<int,int> currentRange = new Tuple<int, int>(InitialRange.Item1, InitialRange.Item2);
            for (int i = 0; i < Input.Length; i++)
            {
                currentRange = GetRange(currentRange, Input[i]);
            }

            return currentRange.Item1;
        }

        private Tuple<int, int> GetRange(Tuple<int, int> range, char letter)
        {
            int lowerMiddlePoint = ((range.Item2 - range.Item1) / 2) + range.Item1;

            if (letter == UpperLetter)
            {
                return new Tuple<int, int>(lowerMiddlePoint+1, range.Item2);
            }

            if (letter == LowerLetter)
            {
                return new Tuple<int, int>(range.Item1, lowerMiddlePoint);
            }

            return null;

        }
    }
}
