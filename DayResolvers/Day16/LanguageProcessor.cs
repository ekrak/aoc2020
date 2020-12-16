using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day16
{
    public class LanguageProcessor
    {
        private Dictionary<string, List<Tuple<int,int>>> wordRanges = new Dictionary<string, List<Tuple<int, int>>>();
        private List<int> myTicket = new List<int>();
        private List<List<int>> tickets = new List<List<int>>();
        private List<int> invalidNumbers = new List<int>();

        private Dictionary<int, List<string>> validWordsForIndex = new Dictionary<int, List<string>>();

        private readonly bool parseDefinition = true;
        private readonly bool parseMyTicket = false;
        private readonly bool parseTickets = false;

        private readonly List<int> invalidTickets = new List<int>();
        private int maxDefinition = 0;
        private int minDefinition = -1;

        public LanguageProcessor(StreamReader input)
        {
            string line = input.ReadLine();
            while (line!=null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (parseDefinition)
                    {
                        EvaluateInvalid();
                    }

                    parseDefinition = false;
                    parseMyTicket = false;
                    parseTickets = false;
                } else if (parseDefinition)
                {
                    ParseDefintion(line);
                } else if (parseMyTicket)
                {
                    myTicket = ParseTicket(line, false);
                } else if (parseTickets) {
                    tickets.Add(ParseTicket(line, true));
                }  else
                {
                    parseMyTicket = line == "your ticket:";
                    parseTickets = !parseMyTicket;
                }


                line = input.ReadLine();
            }
        }

        public double GetSumOfInvalidTickets() => invalidTickets.Sum();

        public double GetDeparturesMultiplied(string filter)
        {
            ReevaluateValidWords();
            double sum = 1;
            int keysMin = validWordsForIndex.Keys.Min();
            int keysMax = validWordsForIndex.Keys.Max();

            for (int i = keysMin; i <= keysMax; i++)
            {
                var word = validWordsForIndex[i].Single();
                if (word.StartsWith(filter))
                {
                    sum = sum * myTicket[i];
                }
            }

            return sum;
        }

        private void ReevaluateValidWords()
        {
            var allCount = validWordsForIndex.Count;
            var evaluated = validWordsForIndex.Values.Where(values => values.Count == 1).ToArray();
            var evalCount = evaluated.Count();

            while (allCount != evalCount)
            {
                var uniqueWords = evaluated.Select(x => x.First()).ToArray();
                foreach (var key in validWordsForIndex.Keys)
                {
                    if (validWordsForIndex[key].Count > 1)
                        validWordsForIndex[key].RemoveAll(val => uniqueWords.Contains(val));
                }

                evaluated = validWordsForIndex.Values.Where(values => values.Count == 1).ToArray();
                evalCount = evaluated.Count();
            }
        }

        private void EvaluateInvalid()
        {
            invalidNumbers = Enumerable.Range(minDefinition, maxDefinition - minDefinition + 1).ToList();
            foreach (var ranges in wordRanges.Values)
            {
                foreach (var range in ranges)
                {
                    HashSet<int> exclusionSet = Enumerable.Range(range.Item1, range.Item2 - range.Item1 + 1).ToHashSet();
                    invalidNumbers.RemoveAll(i => exclusionSet.Contains(i));
                }
            }
        }

        private List<int> ParseTicket(string line, bool considerAsInvalid)
        {
            bool isInvalid = false;
            List<int> ticket = new List<int>();
            var splits = line.Split(',');
            foreach (var split in splits)
            {
                var num = int.Parse(split);
                if (considerAsInvalid && (invalidNumbers.Contains(num) || num < minDefinition || num > maxDefinition))
                {
                    isInvalid = true;
                    invalidTickets.Add(num);
                }
                else
                {
                    ticket.Add(num);
                }
            }

            if (!isInvalid)
            {
                for (int i = 0; i < ticket.Count; i++)
                {
                    AddValidWordsForIndex(i, ticket[i]);
                }
            }

            return ticket;
        }

        private void ParseDefintion(string line)
        {
            var wordRangesSplit = line.Split(':');
            string key = wordRangesSplit[0].Trim();
            var rangesSplit = wordRangesSplit[1].Trim().Split(new[] {" or "}, StringSplitOptions.RemoveEmptyEntries);
            List<Tuple<int, int>> value = new List<Tuple<int, int>>();
            foreach (var rangeSplit in rangesSplit)
            {
                var numSplit = rangeSplit.Split('-');
                Tuple<int,int> range = new Tuple<int, int>(int.Parse(numSplit[0]), int.Parse(numSplit[1]));
                if (minDefinition < 0 || range.Item1 < minDefinition) minDefinition = range.Item1;
                if (range.Item2 > maxDefinition) maxDefinition = range.Item2;
                value.Add(range);
            }

            wordRanges.Add(key, value);
        }

        private void AddValidWordsForIndex(int index, int num)
        {

            var words = new List<string>();
            if (validWordsForIndex.ContainsKey(index))
            {
                var oldWords = validWordsForIndex[index];
                foreach (var oldWord in oldWords)
                {
                    if(IsValidWord(wordRanges[oldWord], num)) words.Add(oldWord);
                }

                validWordsForIndex[index] = words;
            }
            else
            {
                foreach (var wordRange in wordRanges)
                {
                    if(IsValidWord(wordRange.Value, num)) words.Add(wordRange.Key);
                }

                validWordsForIndex.Add(index, words);
            }
        }

        private bool IsValidWord(List<Tuple<int, int>> values, int num)
        {
            foreach (var value in values)
            {
                if (num >= value.Item1 && num <= value.Item2) return true;
            }

            return false;
        }
    }
}
