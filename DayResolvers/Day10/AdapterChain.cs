using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day10
{
    public class AdapterChain
    {
        private List<int> Adapters { get; } = new List<int>();

        public AdapterChain(StreamReader input)
        {
            string line = input.ReadLine();
            while (line != null)
            {
                Adapters.Add(int.Parse(line.Trim()));
                line = input.ReadLine();
            }

            Adapters.Sort();
            Adapters.Add(Adapters.Max() + 3);
        }

        public Tuple<int, int, int> GetDifferences()
        {
            int difference1 = 0;
            int difference2 = 0;
            int difference3 = 0;
            int previous = 0;
            for (int i = 0; i < Adapters.Count; i++)
            {
                int difference = Adapters[i] - previous;
                switch (difference)
                {
                    case 1:
                        difference1++;
                        break;
                    case 2:
                        difference2++;
                        break;
                    case 3:
                        difference3++;
                        break;
                    default:
                        throw new ArgumentException();
                }

                previous = Adapters[i];
            }

            return new Tuple<int, int, int>(difference1, difference2, difference3);
        }

        public long GetCombinations()
        {
            List<int> combinations = new List<int>() { 0 };
            List<double> sumCombinations = new List<double>() { };
            for (int i = 0; i < Adapters.Count; i++)
            {
                if((Adapters[i] - combinations.Last()) == 3)
                {
                    if (combinations.Count > 2)
                    {
                        sumCombinations.Add(GetCalculatedCombinations(combinations.Count - 2));
                    }

                    combinations.Clear();
                    combinations.Add(Adapters[i]);
                }
                else
                {
                    combinations.Add(Adapters[i]);
                }
            }

            long sum = 1;

            foreach (var sumCombination in sumCombinations)
            {
                sum = (long) sumCombination * sum;
            }

            return sum;

        }

        // Figured out this formula 
        //
        // comb(x) {
        //      if(x==0) return 1;
        //      if(x==1) return 2;
        //      if(x==2) return 4;
        //      return 2^x - 3^(x-3)
        // }
        //
        // 1 => 2   --- 2^1 - 0
        // 2 => 4   --- 2^2 - 0
        // 3 => 7   --- 2^3 - 1 = 3^(3-3)
        // 4 => 13  --- 2^4 - 3 = 3^(4-3)
        // 5 => 23  --- 2^5 - 9 = 3^(5-3)

        private double GetCalculatedCombinations(int count)
        {
            if (count == 0) return 1;
            if (count == 1) return 2;
            return Math.Pow(2, count) - GetPow3(count);
        }

        private double GetPow3(int count)
        {
            if (count == 1 || count == 2) return 0;
            return Math.Pow(3, count - 3);
        }

    }
}
