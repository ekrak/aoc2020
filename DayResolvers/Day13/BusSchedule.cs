using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day13
{
    public class BusSchedule
    {
        public int EarliestTime { get; set; }
        public List<int> Intervals { get; set; } = new List<int>();
        public Dictionary<long,long> IdIndex = new Dictionary<long, long>();

        public BusSchedule(StreamReader input)
        {
            EarliestTime = int.Parse(input.ReadLine());
            ParseIntervals(input.ReadLine());
        }

        private void ParseIntervals(string line)
        {
            string[] values = line.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != "x")
                {
                    //Part 1
                    var key = int.Parse(values[i]);
                    Intervals.Add(key);

                    //Part 2
                    var modVal = key - i;
                    if (modVal == key) modVal = 0;
                    IdIndex.Add(key, modVal);
                }
            }
        }

        public int GetClosestBus()
        {
            Dictionary<int,int> waitingTimes = new Dictionary<int, int>();
            Intervals.ForEach(interval => {
                waitingTimes.Add(interval, GetWaitingTime(interval));
            });

            var minValue = waitingTimes.OrderBy(k => k.Value).First();
            return minValue.Key * minValue.Value;
        }

        private int GetWaitingTime(int interval)
        {
            var modulo = EarliestTime % interval;
            return interval - modulo;
        }

        public long FindTimestamp()
        {
            return ChineseRemainderTheorem.Calculate(IdIndex.Keys.ToArray(), IdIndex.Values.ToArray());
        }
    }
}
