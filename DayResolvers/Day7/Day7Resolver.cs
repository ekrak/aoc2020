using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day7
{
    public class Day7Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            var rules = LoadRules(input);

            if (task == 1) return Resolve1Internal(rules);

            return Resolve2Internal(rules);
        }

        private string Resolve2Internal(List<Rule> rules)
        {
            return (GetChildRulesCount(rules, "shiny", "gold") - 1).ToString();
        }

        private string Resolve1Internal(List<Rule> rules)
        {
            var returnRules =  GetRulesByBag(rules, "shiny", "gold").ToList();
            return returnRules.Count.ToString();
        }

        private List<Rule> GetRulesByBag(List<Rule> rules, string color1, string color2)
        {
            var returnRules = new List<Rule>();
            var filteredRules = rules.Where(rule => rule.Bags.Any(bag => bag.Color1 == color1 && bag.Color2 == color2)).ToList();
            returnRules.AddRange(filteredRules);
            filteredRules.ForEach(rule => returnRules.AddRange(GetRulesByBag(rules, rule.Bag.Color1, rule.Bag.Color2)));
            return returnRules.Distinct().ToList();
        }

        private int GetChildRulesCount(List<Rule> rules, string color1, string color2)
        {
            int count = 1;
            var filteredRules = rules.Where(rule => rule.Bag.Color1 == color1 && rule.Bag.Color2 == color2).ToList();
            filteredRules.ForEach(fRule =>
            {
                fRule.Bags.ForEach(bag =>
                {
                    count += bag.Amount * GetChildRulesCount(rules, bag.Color1, bag.Color2);
                });
            });

            return count;
        }

        private List<Rule> LoadRules(StreamReader input)
        {
            var rules = new List<Rule>();
            string line = input.ReadLine();
            while (line != null)
            {
                rules.Add(new Rule(line));
                line = input.ReadLine();
            } 


            return rules;
        }
    }
}