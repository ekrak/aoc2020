namespace AdventOfCode2020_1.DayResolvers.Day5
{
    public class BoardingPass
    {
        public string Input { get; set; }

        public Row Row { get; set; }
        public Column Column { get; set; }


        public BoardingPass(string input)
        {
            Input = input;
            Row = new Row(input.Substring(0,7));
            Column = new Column(input.Substring(7, 3));
        }

        public int GetId()
        {
            return Row.Value * 8 + Column.Value;
        }
    }
}
