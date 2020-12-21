using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day21
{
    public class Food
    {
        public List<string> Foods { get; set; } = new List<string>();
        public List<string> Allergens { get; set; } = new List<string>();

        public Food(string line)
        {
            int indexOfParenthesis = line.IndexOf('(');
            ParseFoods(line.Substring(0, indexOfParenthesis));
            ParseAllergnes(line.Substring(indexOfParenthesis, line.Length - indexOfParenthesis));
        }

        private void ParseAllergnes(string substring)
        {
            string withoutLeft = substring.Trim().Replace("(contains ", "");
            string withoutRight = withoutLeft.Replace(")", "");
            string withoutSplit = withoutRight.Replace(",", "");
            string[] splits = withoutSplit.Split(' ');
            foreach (var split in splits)
            {
                Allergens.Add(split.Trim());
            }
        }

        private void ParseFoods(string substring)
        {
            Foods = substring.Trim().Split(' ').ToList();
        }
    }
}
