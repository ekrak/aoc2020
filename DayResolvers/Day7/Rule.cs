using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day7
{
    public class Rule
    {
        public string Line { get; set; }
        public Bag Bag { get; set; }
        public List<Bag> Bags { get; set; } = new List<Bag>();

        public Rule(string line)
        {
            Line = line;
            Parse();
        }

        private void Parse()
        {
            var splits = Line.Split(new [] {"contain"}, StringSplitOptions.None);
            Bag = new Bag(splits[0].Trim());
            var splits2 = splits[1].Split(',').ToList();
            splits2.ForEach(spl => Bags.Add(new Bag(spl.Trim())));
        }
    }
}
