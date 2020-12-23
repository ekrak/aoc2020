using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020_1.DayResolvers.Day22
{
    public class Player
    {
        public int Id { get; set; }
        public Queue<int> Queue = new Queue<int>();

        public Player(List<string> lines)
        {
            var match = new Regex(@"Player\s([0-9]+):").Match(lines[0]);
            Id = int.Parse(match.Groups[1].Value);
            for (int i = 1; i < lines.Count; i++)
            {
                Queue.Enqueue(int.Parse(lines[i].Trim()));
            }
        }
    }
}
