using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day15
{
    public class MemoryGame
    {
        public List<int> Sequence { get; set; } = new List<int>();

        private List<int> calculatedSequence = new List<int>();
        Dictionary<int, Tuple<int,int>> lastOccurences = new Dictionary<int, Tuple<int, int>>();

        public MemoryGame(StreamReader input)
        {
            string line = input.ReadLine();
            string[] splits = line.Split(',');
            int index = 1;
            foreach (var split in splits)
            {
                int splitNum = int.Parse(split);
                Sequence.Add(splitNum);
                AddToSequences(splitNum, index);
                index++;
            }
        }



        public int Get2020() => SolveLastNumber(2020);

        public int Get30000000() => SolveLastNumber(30000000);

        //Not the fastest but solves under 10sec -> good enough
        //Next idea: check if any loops are happening
        private int SolveLastNumber(int round)
        {
            int count = Sequence.Count;
            int lastNum = Sequence.Last();

            while (count != round)
            {
                count++;
                var val = lastOccurences[lastNum];
                lastNum = val.Item1 == -1 ? 0 : val.Item2 - val.Item1;
                AddToSequences(lastNum, count);
            }

            return lastNum;
        }

        private void AddToSequences(int num, int index)
        {
            calculatedSequence.Add(num);
            if (lastOccurences.ContainsKey(num))
            {
                var val = lastOccurences[num];
                int item1 = val.Item2;
                Tuple<int,int> newTuple = new Tuple<int, int>(item1, index);
                lastOccurences[num] = newTuple;
            }
            else
            {
                lastOccurences.Add(num, new Tuple<int, int>(-1, index));
            }
        }
    }
}
