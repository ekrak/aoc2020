using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public char Value { get; set; }

        public Coordinate(int x, int y, int z, char value)
        {
            X = x;
            Y = y;
            Z = z;
            Value = value;
        }

        public Coordinate Copy(bool emptyValue)
        {
            return new Coordinate(X,Y,Z, emptyValue ? '.' : Value);
        }

        public bool IsActive => Value == '#';

        public Coordinate Evaluate(Dictionary<int, List<Coordinate>> coordinatesByZ)
        {
            var neighbours = GetNumberOfNeighbours(coordinatesByZ);
            if(IsActive && neighbours != 2 && neighbours != 3) return new Coordinate(X, Y, Z, '.');
            if(!IsActive && neighbours == 3) return new Coordinate(X, Y, Z, '#');
            return new Coordinate(X,Y,Z, Value);
        }

        private int GetNumberOfNeighbours(Dictionary<int, List<Coordinate>> coordinatesByZ)
        {
            int sum = 0;
            if(coordinatesByZ.ContainsKey(Z)) sum += GetNumberOfNeighboursForZ(coordinatesByZ[Z]);
            if (coordinatesByZ.ContainsKey(Z-1)) sum += GetNumberOfNeighboursForZ(coordinatesByZ[Z-1]);
            if (coordinatesByZ.ContainsKey(Z+1)) sum += GetNumberOfNeighboursForZ(coordinatesByZ[Z+1]);
            return sum;
        }

        private int GetNumberOfNeighboursForZ(List<Coordinate> zCoordinates)
        {
            int sum = 0;
            bool isSameZ = zCoordinates.First().Z == Z;

            var xMinus1yMinus1 = zCoordinates.FirstOrDefault(c => c.X == X - 1 && c.Y == Y - 1);
            if (xMinus1yMinus1 != null) sum += xMinus1yMinus1.Value == '#' ? 1 : 0;

            var xyMinus1 = zCoordinates.FirstOrDefault(c => c.X == X && c.Y == Y - 1);
            if (xyMinus1 != null) sum += xyMinus1.Value == '#' ? 1 : 0;

            var xPlus1yMinus1 = zCoordinates.FirstOrDefault(c => c.X == X + 1 && c.Y == Y - 1);
            if (xPlus1yMinus1 != null) sum += xPlus1yMinus1.Value == '#' ? 1 : 0;

            var xMinus1y = zCoordinates.FirstOrDefault(c => c.X == X - 1 && c.Y == Y);
            if (xMinus1y != null) sum += xMinus1y.Value == '#' ? 1 : 0;

            var xPlus1y = zCoordinates.FirstOrDefault(c => c.X == X + 1 && c.Y == Y);
            if (xPlus1y != null) sum += xPlus1y.Value == '#' ? 1 : 0;

            var xMinus1yPlus1 = zCoordinates.FirstOrDefault(c => c.X == X - 1 && c.Y == Y + 1);
            if (xMinus1yPlus1 != null) sum += xMinus1yPlus1.Value == '#' ? 1 : 0;

            var xyPlus1 = zCoordinates.FirstOrDefault(c => c.X == X && c.Y == Y + 1);
            if (xyPlus1 != null) sum += xyPlus1.Value == '#' ? 1 : 0;

            var xPlus1yPlus1 = zCoordinates.FirstOrDefault(c => c.X == X + 1 && c.Y == Y + 1);
            if (xPlus1yPlus1 != null) sum += xPlus1yPlus1.Value == '#' ? 1 : 0;

            if (!isSameZ)
            {
                var xy = zCoordinates.FirstOrDefault(c => c.X == X && c.Y == Y);
                if (xy != null) sum += xy.Value == '#' ? 1 : 0;
            }

            return sum;
        }


    }
}
