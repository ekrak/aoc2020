using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day9
{
    public class Code
    {
        public List<long> Preamble { get; set; } = new List<long>();

        public List<long> Current { get; set; } = new List<long>();

        public List<long> AfterPreamble { get; set; } = new List<long>();

        public List<long> All { get; set; } = new List<long>();

        public Code(StreamReader input, int preambleLength)
        {
            string line = input.ReadLine();
            while (line != null)
            {
                long number = long.Parse(line.Trim());
                if (Preamble.Count < preambleLength)
                {
                    Preamble.Add(number);
                }
                else
                {
                    AfterPreamble.Add(number);
                }

                All.Add(number);

                line = input.ReadLine();
            }
        }

        public List<long> GetEncryptionWeakness()
        {
            long result = GetFirstWrong();
            List<long> contiguousSet = new List<long>();

            for (int i = 0; i < All.Count; i++)
            {
                contiguousSet.Add(All[i]);
                while (contiguousSet.Sum() > result)
                {
                    contiguousSet.RemoveAt(0);
                }

                if (contiguousSet.Count > 1 && contiguousSet.Sum() == result) 
                    return contiguousSet;
            }

            return null;

        }


        public long GetFirstWrong()
        {
            Current.Clear();
            Current.AddRange(Preamble);

            for (int i = 0; i < AfterPreamble.Count; i++)
            {
                if (!CanSum(Current, AfterPreamble[i]))
                     return AfterPreamble[i];

                Current.RemoveAt(0);
                Current.Add(AfterPreamble[i]);
            }

            return -1;
        }

        private bool CanSum(List<long> list, long number)
        {
            var smallerList = list.Where(num => num < number).ToList();
            for (int i = smallerList.Count - 1; i >= 0; i--)
            {
                if (smallerList.Contains(number - smallerList[i])) return true;
            }

            return false;
        }
    }
}
