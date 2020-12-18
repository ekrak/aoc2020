using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
        public char Value { get; set; }
        public bool considerW { get; set; } = false;

        public Coordinate(int x, int y, int z, char value)
        {
            X = x;
            Y = y;
            Z = z;
            Value = value;
        }

        public Coordinate(int x, int y, int z, int w, char value)
        {
            considerW = true;
            X = x;
            Y = y;
            Z = z;
            W = w;
            Value = value;
        }

        public Coordinate Copy(bool emptyValue)
        {
            return new Coordinate(X,Y,Z,W, emptyValue ? '.' : Value);
        }

        public bool IsActive => Value == '#';

        public Coordinate Evaluate(Dictionary<int, List<Coordinate>> coordinatesByZ)
        {
            var neighbours = GetNumberOfNeighboursForW(coordinatesByZ);
            if(IsActive && neighbours != 2 && neighbours != 3) return new Coordinate(X, Y, Z, '.');
            if(!IsActive && neighbours == 3) return new Coordinate(X, Y, Z, '#');
            return new Coordinate(X,Y,Z, Value);
        }

        public Coordinate EvaluateWithW(Dictionary<int, CubePocket> coordinatesByW)
        {
            var neighbours = GetNumberOfNeighbours(coordinatesByW);
            if (IsActive && neighbours != 2 && neighbours != 3) return new Coordinate(X, Y, Z, W, '.');
            if (!IsActive && neighbours == 3) return new Coordinate(X, Y, Z, W, '#');
            return new Coordinate(X, Y, Z, W, Value);
        }

        private int GetNumberOfNeighbours(Dictionary<int, CubePocket> coordinatesByW)
        {
            int sum = 0;
            if (coordinatesByW.ContainsKey(W)) sum += GetNumberOfNeighboursForW(coordinatesByW[W].ZLayers);
            if (coordinatesByW.ContainsKey(W - 1)) sum += GetNumberOfNeighboursForW(coordinatesByW[W-1].ZLayers);
            if (coordinatesByW.ContainsKey(W + 1)) sum += GetNumberOfNeighboursForW(coordinatesByW[W+1].ZLayers);
            return sum;
        }

        private int GetNumberOfNeighboursForW(Dictionary<int, List<Coordinate>> coordinatesByZ)
        {
            int sum = 0;
            if(coordinatesByZ.ContainsKey(Z)) sum += GetNumberOfNeighboursForZ2(coordinatesByZ[Z]);
            if (coordinatesByZ.ContainsKey(Z-1)) sum += GetNumberOfNeighboursForZ2(coordinatesByZ[Z-1]);
            if (coordinatesByZ.ContainsKey(Z+1)) sum += GetNumberOfNeighboursForZ2(coordinatesByZ[Z+1]);
            return sum;
        }

        private int GetNumberOfNeighboursForZ2(List<Coordinate> zCoordinates)
        {
            int sum = 0;
            if (zCoordinates.Any(c => c.Y == Y -1)) sum += GetNumberOfNeighboursForY(zCoordinates.Where(c => c.Y == Y - 1).ToList());
            if (zCoordinates.Any(c => c.Y == Y)) sum += GetNumberOfNeighboursForY(zCoordinates.Where(c => c.Y == Y).ToList());
            if (zCoordinates.Any(c => c.Y == Y + 1)) sum += GetNumberOfNeighboursForY(zCoordinates.Where(c => c.Y == Y + 1).ToList());
            return sum;
        }

        private int GetNumberOfNeighboursForY(List<Coordinate> yCoordinates)
        {
            int sum = 0;
            bool isSameZ = yCoordinates.First().Z == Z;
            bool isSameY = yCoordinates.First().Y == Y;
            bool isSameW = yCoordinates.First().W == W;

            var xMinus1 = yCoordinates.FirstOrDefault(c => c.X == X - 1);
            if (xMinus1 != null) sum += xMinus1.Value == '#' ? 1 : 0;

            if (!(isSameZ && isSameY && isSameW))
            {
                var x = yCoordinates.FirstOrDefault(c => c.X == X);
                if (x != null) sum += x.Value == '#' ? 1 : 0;
            }

            var xPlus1 = yCoordinates.FirstOrDefault(c => c.X == X + 1);
            if (xPlus1 != null) sum += xPlus1.Value == '#' ? 1 : 0;

            return sum;
        }


    }
}
