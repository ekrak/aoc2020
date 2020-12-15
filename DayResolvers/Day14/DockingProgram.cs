using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020_1.DayResolvers.Day14
{
    public class DockingProgram
    {
        public char[] Mask { get; set; } = new char[36];
        List<string> lines = new List<string>();
        Dictionary<double,double> MemoryValues = new Dictionary<double, double>();

        public DockingProgram(StreamReader input)
        {
            string line = input.ReadLine();
            while (line != null)
            {
                lines.Add(line);
                line = input.ReadLine();
            }
        }

        private void ParseMask(string line)
        {
            string[] splits = line.Split('=');
            string mask = splits[1].Trim();
            for (int i = 0; i < mask.Length; i++)
            {
                Mask[i] = mask[i];
            }
        }

        private Tuple<int,int> GetMemoryInput(string line)
        {
            var match = new Regex(@"\[(.*?)\]\s\=\s([0-9]+)").Match(line);
            string key = match.Groups[1].Value;
            string val = match.Groups[2].Value;

            return new Tuple<int, int>(int.Parse(key), int.Parse(val));

        }

        public double GetFinalSum()
        {
            GoThrough();
            return MemoryValues.Values.Sum();
        }

        private void GoThrough()
        {
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    ParseMask(line);
                }
                else
                {
                    var tuple = GetMemoryInput(line);
                    ApplyMemoryInput(tuple);
                }    
            }
        }

        private void ApplyMemoryInput(Tuple<int, int> input)
        {
            char[] binary = Convert.ToString(input.Item2, 2).ToCharArray();
            char[] finalBinary = new char[36];
            Array.Copy(Mask, finalBinary, Mask.Length);
            int difference = Mask.Length - binary.Length;
            for (int i = Mask.Length - 1; i >= 0; i--)
            {
                if (Mask[i] == 'X')
                {
                    if ((i - difference) >= 0)
                    {
                        finalBinary[i] = binary[i - difference];
                    }
                    else
                    {
                        finalBinary[i] = '0';
                    }
                }
            }

            if (MemoryValues.ContainsKey(input.Item1))
            {
                MemoryValues[input.Item1] = Convert.ToInt64(new string(finalBinary), 2);
            }
            else
            {
                MemoryValues.Add(input.Item1, Convert.ToInt64(new string(finalBinary), 2));
            }
        }

        public double GetFinalSum2()
        {
            GoThrough2();
            return MemoryValues.Values.Sum();
        }

        private void GoThrough2()
        {
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    ParseMask(line);
                }
                else
                {
                    var tuple = GetMemoryInput(line);
                    ApplyMemoryInput2(tuple);
                }
            }
        }

        private void ApplyMemoryInput2(Tuple<int, int> input)
        {
            char[] binary = Convert.ToString(input.Item1, 2).ToCharArray();
            char[] finalBinary = null;
            if (binary.Length > Mask.Length)
            {
                finalBinary = new char[binary.Length];
                int diff = binary.Length - Mask.Length;
                for (int i = 0; i < Mask.Length; i++)
                {
                    finalBinary[i + diff] = Mask[i];
                }
            }
            else
            {
                finalBinary = new char[36];
                Array.Copy(Mask, finalBinary, Mask.Length);
            }

            for (int i = finalBinary.Length - 1; i >= 0; i--)
            {
                int difference = finalBinary.Length - binary.Length;
                if (finalBinary[i] == '0')
                {
                    if ((i - difference) >= 0)
                    {
                        finalBinary[i] = binary[i - difference];
                    }
                }
            }

            double basicSum = 0;
            for (int i = finalBinary.Length - 1; i >= 0; i--)
            {
                if (finalBinary[i] == '1')
                {
                    basicSum += Math.Pow(2, finalBinary.Length - 1 - i);
                }
            }
            var allSubsets = GetAllCombinations(finalBinary);
            allSubsets.Add(new List<double> {0});
            allSubsets.ForEach(x =>
            {
                double sum = basicSum + x.Sum();
                if (MemoryValues.ContainsKey(sum))
                {
                    MemoryValues[sum] = input.Item2;
                } else {
                    MemoryValues.Add(sum, input.Item2);
                }
            });
        }

        private List<List<double>> GetAllCombinations(char[] binary)
        {
            var indexes = GetAllInvertedIndexesOfFloating(binary);
            List<double> options = new List<double>();
            indexes.ForEach(index =>
            {
                options.Add(Math.Pow(2, index));
            });

            var allSubsets = Subsets(options, options.Count).ToList();
            return allSubsets;
        }

        private List<int> GetAllInvertedIndexesOfFloating(char[] binary)
        {
            var foundIndexes = new List<int>();
            var str = new string(binary);
            for (int i = str.IndexOf('X'); i > -1; i = str.IndexOf('X', i + 1))
            {
                foundIndexes.Add(binary.Length - i - 1);
            }

            return foundIndexes;
        }

        static IEnumerable<List<double>> Subsets(List<double> objects, int maxLength)
        {
            if (objects == null || maxLength <= 0)
                yield break;
            var stack = new Stack<int>(maxLength);
            int i = 0;
            while (stack.Count > 0 || i < objects.Count)
            {
                if (i < objects.Count)
                {
                    if (stack.Count == maxLength)
                        i = stack.Pop() + 1;
                    stack.Push(i++);
                    yield return (from index in stack.Reverse()
                        select objects[index]).ToList();
                }
                else
                {
                    i = stack.Pop() + 1;
                    if (stack.Count > 0)
                        i = stack.Pop() + 1;
                }
            }
        }
    }
}
