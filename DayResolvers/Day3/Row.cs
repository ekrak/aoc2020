namespace AdventOfCode2020_1.DayResolvers.Day3
{
    public class Row
    {
        public Row(string input)
        {
            Input = input;
        }

        public string Input { get; set; }

        public bool IsOpen(int position)
        {
            var modulo = position % Input.Length;
            return Input[modulo] == '.';
        }
    }
}