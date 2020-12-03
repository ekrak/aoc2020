using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day1
{
    public class Day1Resolver
    {
        public string Resolve(string input, int task)
        {
            List<int> numValues = GetNumbers(input);
            if (task == 1)
            {
                return Resolve1Internal(numValues);

            }

            return Resolve2Internal(numValues);

        }

        public string Resolve(StreamReader input, int task)
        {
            List<int> numValues = GetNumbers(input);
            if (task == 1)
            {
                return Resolve1Internal(numValues);

            }

            return Resolve2Internal(numValues);

        }

        private string Resolve2Internal(List<int> numValues)
        {
            for (int i = 0; i < numValues.Count - 2; i++)
            {
                for (int j = i + 1; j < numValues.Count - 1; j++)
                {
                    if (numValues[i] + numValues[j] <= 2020)
                    {
                        for (int k = j + 1; k < numValues.Count; k++)
                        {
                            if (numValues[i] + numValues[j] + numValues[k] == 2020)
                            {
                                return (numValues[i] * numValues[j] * numValues[k]).ToString();
                            }
                        }
                    }
                }
            }

            return "Not found";
        }

        private string Resolve1Internal(List<int> numValues)
        {
            for (int i = 0; i < numValues.Count; i++)
            {
                for (int j = numValues.Count - 1; j > i; j--)
                {
                    if (numValues[i] + numValues[j] == 2020)
                    {
                        return (numValues[i] * numValues[j]).ToString();
                    }
                }
            }

            return "Not found";
        }

        private List<int> GetNumbers(string input)
        {
            List<int> numValues = new List<int>();
            string[] valueStrings = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var valueString in valueStrings)
            {
                if(int.TryParse(valueString, out int resultValue) && resultValue <= 2020)
                {
                    numValues.Add(resultValue);
                }
            }
            numValues.Sort();
            return numValues;
        }

        private List<int> GetNumbers(StreamReader input)
        {
            List<int> numValues = new List<int>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                if (int.TryParse(line, out int resultValue) && resultValue <= 2020)
                {
                    numValues.Add(resultValue);
                }
            }

            numValues.Sort();
            return numValues;
        }
    }
}
