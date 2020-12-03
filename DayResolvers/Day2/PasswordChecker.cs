using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day2
{
    public class PasswordChecker
    {
        
        public PasswordChecker(string input)
        {
            Input = input;
            Parse();
        }

        public bool Validate()
        {
            int count = Password.Count(f => f == RuleLetter);
            return count >= RuleInterval.Item1 && count <= RuleInterval.Item2;
        }

        public bool Validate2()
        {
            char char1 = Password[RuleInterval.Item1 - 1];
            char char2 = Password[RuleInterval.Item2 - 1];
            return (char1 == RuleLetter || char2 == RuleLetter) && char1 != char2;
        }

        private void Parse()
        {
            string[] splits = Input.Split(' ');
            RuleInterval = ParseInterval(splits[0]);
            RuleLetter = ParseLetter(splits[1]);
            Password = splits[2];
        }

        private char ParseLetter(string input) => input[0];

        private Tuple<int, int> ParseInterval(string input)
        {
            string[] splits = input.Split('-');
            int number1 = int.Parse(splits[0]);
            int number2 = int.Parse(splits[1]);
            return new Tuple<int, int>(number1, number2);
        }

        public Tuple<int, int> RuleInterval { get; set; }
        public char RuleLetter { get; set; }
        public string Password { get; set; }
        public string Input { get; set; }
    }
}
