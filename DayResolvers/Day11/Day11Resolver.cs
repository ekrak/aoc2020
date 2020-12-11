using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day11
{
    public class Day11Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            SeatMap seatMap = new SeatMap(input);
            if (task == 1) return Resolve1Internal(seatMap);

            return Resolve2Internal(seatMap);
        }

        private string Resolve2Internal(SeatMap seatMap)
        {
            return seatMap.GetOccupiedSeats(2).ToString();
        }

        private string Resolve1Internal(SeatMap seatMap)
        {
            return seatMap.GetOccupiedSeats(1).ToString();
        }
    }
}