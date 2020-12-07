using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day7
{
    public class Bag
    {
        public string Raw { get; set; }
        public string Color => $"{Color1} {Color2}";
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public int Amount { get; set; }

        public Bag(string raw)
        {
            Raw = raw;
            Parse();
        }

        private void Parse()
        {
            var spacesSplit = Raw.Trim().Split(' ');
            if (spacesSplit.Length == 3)
            {
                Color1 = spacesSplit[0].Trim();
                Color2 = spacesSplit[1].Trim();
                Amount = Color2 == "other" ? 0: 1;
            } else if (spacesSplit.Length == 4)
            {
                Amount = int.Parse(spacesSplit[0]);
                Color1 = spacesSplit[1].Trim();
                Color2 = spacesSplit[2].Trim();
            }


        }
    }
}
