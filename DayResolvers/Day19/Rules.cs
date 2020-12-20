using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day19
{
    public class Rules
    {
        public List<Rule> AllRules = new List<Rule>();
        private List<Rule> unevaluated = new List<Rule>();
        private Dictionary<int, Rule> RulesById { get; set; } = new Dictionary<int, Rule>();

        public Rules(List<string> lines)
        {
            foreach (var line in lines)
            {
                Rule newRule = new Rule(line);
                AllRules.Add(newRule);
                if (newRule.IsEvaluated)
                {
                    RulesById.Add(newRule.Id, newRule);
                }
                else
                {
                    unevaluated.Add(newRule);
                }
            }

            EvaluatePatterns();
        }

        private void EvaluatePatterns()
        {
            while (unevaluated.Any())
            {
                List<Rule> rulesToRemove = new List<Rule>();
                foreach (var urule in unevaluated)
                {
                    if (!urule.OccuringNumbers.Except(RulesById.Keys.ToList()).Any())
                    {
                        urule.Evaluate(RulesById);
                        rulesToRemove.Add(urule);
                        RulesById.Add(urule.Id, urule);
                    }
                }

                rulesToRemove.ForEach(r => unevaluated.Remove(r));
                rulesToRemove.Clear();
            }
        }

        public bool IsLineValid(string line)
        {
            Rule zeroRule = RulesById[0];
            return zeroRule.Pattern.Contains(line);
        }
    }
}
