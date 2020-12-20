using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day19
{
    public class Rule
    {
        public int Id { get; set; }

        public List<string> Pattern { get; set; } = new List<string>();

        public List<List<int>> Options { get; set; } = new List<List<int>>();

        public Rule(string line)
        {
            string[] idValue = line.Split(':');
            Id = int.Parse(idValue[0]);
            string val = idValue[1].Trim();
            if (val.Contains("\""))
            {
                Pattern.Add(val.Replace("\"", ""));
            }
            else
            {
                ParseOptions(val);
            }

        }

        public List<int> OccuringNumbers {
            get {
                List<int> numbers = new List<int>();
                Options.ForEach(numbers.AddRange);
                numbers.Remove(Id);
                return numbers;
            }
        }

        public bool IsEvaluated => Pattern.Any();

        public void Evaluate(Dictionary<int, Rule> evalRules)
        {
            foreach (var option in Options)
            {
                EvaluateOption(option, evalRules);
            }
        }

        private void EvaluateOption(List<int> option, Dictionary<int, Rule> evalRules)
        {
            List<List<string>> options = new List<List<string>>();
            foreach (var num in option)
            {
                var rule = evalRules[num];
                options.Add(rule.Pattern);
            }

            Pattern.AddRange(GetCombinations(options));
        }

        private List<string> GetCombinations(List<List<string>> options)
        {
            if (options.Count == 1)
            {
                return options[0];
            }

            if (options.Count == 2)
            {

                return GetCombinations(options[0], options[1]).ToList();
            }

            return GetCombinations(options[0],
                GetCombinations(options.GetRange(1, options.Count - 1))).ToList();
        }

        private List<string> GetCombinations(List<string> options1, List<string> options2)
        {
            List<string> combinations = new List<string>();
            foreach (var option1 in options1)
            {
                foreach (var option2 in options2)
                {
                    combinations.Add(option1 + option2);
                }
            }

            return combinations;
        }

        private void ParseOptions(string raw)
        {
            string[] pipes = raw.Split('|');
            foreach (var pipe in pipes)
            {
                ParsePipe(pipe.Trim());
            }
        }

        private void ParsePipe(string raw)
        {
            List<int> rules = new List<int>();
            string[] ids = raw.Split(' ');
            foreach (var id in ids)
            {
                rules.Add(int.Parse(id));
            }

            Options.Add(rules);
        }
}
}
