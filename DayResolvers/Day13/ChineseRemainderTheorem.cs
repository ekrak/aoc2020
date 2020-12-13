using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day13
{
    public static class ChineseRemainderTheorem
    {
        public static long Calculate(long[] n, long[] a)
        {
            long seed = 1;
            long prod = n.Aggregate(seed, (i, j) => i * j);
            long p;
            long sm = 0;
            for (long i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}
