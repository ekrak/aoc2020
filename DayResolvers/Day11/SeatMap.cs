using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day11
{
    public class SeatMap
    {
        char[][] seatMap = null;
        public SeatMap(StreamReader input)
        {
            List<string> lines = new List<string>();
            string line = input.ReadLine();
            while (line != null)
            {
                lines.Add(line);
                line = input.ReadLine();
            }


            ConvertToMap(lines);
        }

        private void ConvertToMap(List<string> lines)
        {
            seatMap = new char[lines.Count][];
            for (int i = 0; i < lines.Count; i++)
            {
                seatMap[i] = lines[i].ToCharArray();
            }
        }

        public int GetOccupiedSeats(int task)
        {
            bool hasChanged = true;

            while (hasChanged)
            {
                hasChanged = RevisitMap(task);
            }

            return GetOccupiedSeatsNumber();
        }


        private int GetOccupiedSeatsNumber() => seatMap.SelectMany(row => row).Count(t => t == '#');

        private bool RevisitMap(int task)
        {
            bool changed = false;
            var backupArray = seatMap.Select(a => a.ToArray()).ToArray();
            for (int i = 0; i < seatMap.Length; i++)
            {
                for (int j = 0; j < seatMap[i].Length; j++)
                {
                    var occupied = task == 1 ? ContainsOccupiedNearby(backupArray, i, j) : ContainsOccupiedNearby2(backupArray, i, j);
                    if (backupArray[i][j] == '#' && occupied >= (task == 1 ? 4 : 5))
                    {
                        seatMap[i][j] = 'L';
                        changed = true;
                    }
                    else if (backupArray[i][j] == 'L' && occupied == 0)
                    {
                        seatMap[i][j] = '#';
                        changed = true;
                    }
                }
            }

            return changed;
        }

        private int ContainsOccupiedNearby2(char[][] array, int i, int j)
        {
            int occupied = 0;
            if(ContainsOccupiedInDirection(array, i, j, -1, -1)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, -1, 0)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, -1, 1)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, 0, -1)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, 0, 1)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, 1, -1)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, 1, 0)) occupied++;
            if (ContainsOccupiedInDirection(array, i, j, 1, 1)) occupied++;

            return occupied;
        }

        private bool ContainsOccupiedInDirection(char[][] array, int i, int j, int moveI, int moveJ)
        {
            bool occupied = false;
            bool found = false;
            bool exists = true;
            int currentI = i;
            int currentJ = j;

            while (exists && !found)
            {
                currentI += moveI;
                currentJ += moveJ;
                bool iExists = currentI >= 0 && currentI < array.Length;
                bool jExists = currentJ >= 0 && currentJ < array[i].Length;
                exists = iExists && jExists;

                if (exists)
                {
                    var place = array[currentI][currentJ];
                    found = place != '.';
                    occupied = place == '#';
                }
            }

            return occupied;

        }

        private int ContainsOccupiedNearby(char[][] array, int i, int j)
        {
            int occupied = 0;
            bool iMinus1Exists = (i - 1) >= 0;
            bool jMinus1Exists = (j - 1) >= 0;
            bool iPlus1Exists = (i + 1) < array.Length;
            bool jPlus1Exists = (j + 1) < array[i].Length;

            // x ? ?
            // ? P ?
            // ? ? ?
            if (iMinus1Exists && jMinus1Exists && array[i - 1][j - 1] == '#') occupied++;

            // ? x ?
            // ? P ?
            // ? ? ?
            if (iMinus1Exists && array[i - 1][j] == '#') occupied++;

            // ? ? x
            // ? P ?
            // ? ? ?
            if (iMinus1Exists && jPlus1Exists && array[i - 1][j + 1] == '#') occupied++;

            // ? ? ?
            // x P ?
            // ? ? ?
            if (jMinus1Exists && array[i][j - 1] == '#') occupied++;

            // ? ? ?
            // ? P x
            // ? ? ?
            if (jPlus1Exists && array[i][j + 1] == '#') occupied++;

            // ? ? ?
            // ? P ?
            // x ? ?
            if (iPlus1Exists && jMinus1Exists && array[i + 1][j - 1] == '#') occupied++;

            // ? ? ?
            // ? P ?
            // ? x ?
            if (iPlus1Exists && array[i + 1][j] == '#') occupied++;

            // ? ? ?
            // ? P ?
            // ? ? x
            if (iPlus1Exists && jPlus1Exists && array[i + 1][j + 1] == '#') occupied++;

            return occupied;
        }
    }
}
