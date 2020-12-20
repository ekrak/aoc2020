using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day19
{
    public class Validator
    {
        private List<string> rules = new List<string>();
        private List<string> lines = new List<string>();

        public Rules Rules { get; set; }

        public Validator(StreamReader input)
        {
            string line = input.ReadLine();
            bool areRules = true;

            while (line!=null) 
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    areRules = false;
                    line = input.ReadLine();
                    continue;
                }

                if (areRules)
                {
                    rules.Add(line.Trim());
                }
                else
                {
                    lines.Add(line.Trim());
                }



                line = input.ReadLine();
            }

            Rules = new Rules(rules);
        }


        public int GetValidLines()
        {
            return lines.Count(Rules.IsLineValid);
        }
    }
}
