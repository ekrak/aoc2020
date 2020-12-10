using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day10
{
    public class Day10Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            AdapterChain chain = new AdapterChain(input);
            if (task == 1) return Resolve1Internal(chain);

            return Resolve2Internal(chain);
        }

        private string Resolve2Internal(AdapterChain chain)
        {
            return chain.GetCombinations().ToString();
        }

        private string Resolve1Internal(AdapterChain chain)
        {
            var differences = chain.GetDifferences();
            return $"{differences.Item1}*{differences.Item3}={(differences.Item1 * differences.Item3)}";
        }
    }
}