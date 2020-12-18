using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class CubePocket
    {
        public Dictionary<int, List<Coordinate>> ZLayers { get; set; } = new Dictionary<int, List<Coordinate>>();

        private int w = 0;

        public int W
        {
            get => w;

            set
            {
                w = value;
                var zKeys = ZLayers.Keys.ToList();
                zKeys.ForEach(zKey => { ZLayers[zKey].ForEach(c => c.W = w); });
            }
        }

        public CubePocket(StreamReader input)
        {
           string line = input.ReadLine();
           ZLayers.Add(0, new List<Coordinate>());
           int currentY = 0;
           while (line!=null)
           {
               var charArray = line.ToCharArray();
               for (int i = 0; i < charArray.Length; i++)
               {
                   ZLayers[0].Add(new Coordinate(i, currentY, 0, 0, charArray[i]));
               }

               currentY++;
               line = input.ReadLine();
           }
        }

        public CubePocket(Dictionary<int, List<Coordinate>> zLayers)
        {
            ZLayers = zLayers;
        }

        public string GetActiveCubes(int loops)
        {
            string print = Print(0);
            long sum = 0;
            for (int i = 1; i <= loops; i++)
            {
                LoopInternal();
                print += Print(i);
            }

            
            return GetActiveCount() + Environment.NewLine + print;
        }

        public int GetMaxCoordinate()
        {
            List<int> vals = new List<int>();
            var zKeys = ZLayers.Keys.ToList();

            zKeys.ForEach(zKey =>
            {
                var list = ZLayers[zKey];
                vals.Add(list.Max(x => x.X));
                vals.Add(list.Max(x => x.Y));
                vals.Add(list.Max(x => x.Z));
                vals.Add(list.Max(x => x.W));
            });

            vals.AddRange(ZLayers.Keys);
            return vals.Max();

        }

        public int GetMinCoordinate()
        {
            List<int> vals = new List<int>();
            var zKeys = ZLayers.Keys.ToList();

            zKeys.ForEach(zKey =>
            {
                var list = ZLayers[zKey];
                vals.Add(list.Min(x => x.X));
                vals.Add(list.Min(x => x.Y));
                vals.Add(list.Min(x => x.Z));
                vals.Add(list.Min(x => x.W));
            });

            vals.AddRange(ZLayers.Keys);
            return vals.Min();

        }

        public override string ToString()
        {
            return ZLayers.Any(z => z.Value.Any(c => c.IsActive)) ? "Active" : "Inactive";
        }

        public CubePocket Copy()
        {
            Dictionary<int, List<Coordinate>> copyZLayers = new Dictionary<int, List<Coordinate>>();
            var zKeys = ZLayers.Keys.ToList();
            zKeys.ForEach(zKey =>
            {
                List<Coordinate> copyCoordinates = new List<Coordinate>();
                ZLayers[zKey].ForEach(layer => copyCoordinates.Add(layer.Copy(true)));
                copyZLayers.Add(zKey, copyCoordinates);
            });

            return new CubePocket(copyZLayers);
        }

        public bool IsEmpty() => GetActiveCount() == 0;

        public long GetActiveCount()
        {
            long sum = 0;
            var zKeys = ZLayers.Keys.ToList();
            zKeys.ForEach(zKey => { sum += ZLayers[zKey].Count(c => c.IsActive); });
            return sum;
        }

        public string Print(int loop)
        {
            string toReturn = "LOOP " + loop + " --------------------------------------------------------";
            toReturn += "W = " + W;
            var minZ = ZLayers.Keys.Min();
            var maxZ = ZLayers.Keys.Max();

            for (int z = minZ; z <= maxZ; z++)
            {
                toReturn += Environment.NewLine + Environment.NewLine + "Z=" + z + Environment.NewLine;
                var layer = ZLayers[z];
                var minx = layer.Min(x => x.X);
                var maxx = layer.Max(x => x.X);
                var miny = layer.Min(x => x.Y);
                var maxy = layer.Max(x => x.Y);

                for (int y = miny; y <= maxy; y++)
                {
                    for (int x = minx; x <= maxx; x++)
                    {
                        toReturn += layer.Single(c => c.X == x && c.Y == y).Value;
                    }

                    toReturn += Environment.NewLine;
                }
            }

            return toReturn;
        }


        private void LoopInternal()
        {
            ZLayers = Loop();
        }

        public Dictionary<int, List<Coordinate>> Loop(Dictionary<int, CubePocket> wLayers = null)
        {
            Dictionary<int, List<Coordinate>> newZLayers = new Dictionary<int, List<Coordinate>>();
            var zKeys = ZLayers.Keys.ToList();
            zKeys.Sort();
            zKeys.ForEach(key => { newZLayers.Add(key, LoopZ(key, wLayers)); });

            var minZ = zKeys.Min();
            var maxZ = zKeys.Max();



            var minCoordinate = GetMinCoordinate();
            var maxCoordinate = GetMaxCoordinate();

            var lastZ = minZ;
            List<int> addedZs = new List<int>();
            bool go = lastZ >= minCoordinate;
            while (go || !IsEmpty(newZLayers[lastZ]))
            {
                go = lastZ >= minCoordinate;
                var copy = Copy(ZLayers[lastZ]);
                lastZ--;
                copy.ForEach(c => c.Z = lastZ);
                ZLayers.Add(lastZ, copy);
                addedZs.Add(lastZ);
                newZLayers.Add(lastZ, LoopZ(lastZ, wLayers));
            }

            addedZs.ForEach(z => ZLayers.Remove(z));
            if(lastZ != minZ)
                newZLayers.Remove(lastZ);

            addedZs.Clear();
            lastZ = maxZ;
            go = lastZ <= maxCoordinate;
            while (go || !IsEmpty(newZLayers[lastZ]))
            {
                go = lastZ <= maxCoordinate;
                var copy = Copy(ZLayers[lastZ]);
                lastZ++;
                copy.ForEach(c => c.Z = lastZ);
                ZLayers.Add(lastZ, copy);
                addedZs.Add(lastZ);

                newZLayers.Add(lastZ, LoopZ(lastZ, wLayers));
            }

            addedZs.ForEach(z => ZLayers.Remove(z));
            if (lastZ != maxZ)
                newZLayers.Remove(lastZ);

            return newZLayers;
        }

        private List<Coordinate> LoopZ(int zKey, Dictionary<int, CubePocket> wLayers)
        {
            List<Coordinate> newLayer = new List<Coordinate>();
            var layer = ZLayers[zKey];
            layer.ForEach(coordinate =>
            {
                var newCoordinate = wLayers != null ? coordinate.EvaluateWithW(wLayers) : coordinate.Evaluate(ZLayers);
                newLayer.Add(newCoordinate);
            });

            var minX = newLayer.Select(c => c.X).Min();
            var minY = newLayer.Select(c => c.Y).Min();
            var maxX = newLayer.Select(c => c.X).Max();
            var maxY = newLayer.Select(c => c.Y).Max();

            List<Coordinate> newCoordinates = new List<Coordinate>();
            List<Coordinate> allNewCoordinates = new List<Coordinate>();

            List<Coordinate> tempCoordinates = new List<Coordinate>();
            List<Coordinate> allNewTempCoordinates = new List<Coordinate>();



            var minCoordinate = GetMinCoordinate();
            var maxCoordinate = GetMaxCoordinate();

            bool go = true;
            do
            {
                newCoordinates = GetSurroundingCoordinates(minX, minY, maxX, maxY, zKey);
                allNewCoordinates.AddRange(newCoordinates);
                allNewTempCoordinates.AddRange(tempCoordinates);
                tempCoordinates = new List<Coordinate>();
                layer.AddRange(newCoordinates);
                newCoordinates.ForEach(coordinate =>
                {
                    var newCoordinate = wLayers != null ? coordinate.EvaluateWithW(wLayers) : coordinate.Evaluate(ZLayers);
                    tempCoordinates.Add(newCoordinate);
                });
                minX--;
                minY--;
                maxX++;
                maxY++;
                List<int> coordinateExtrems = new List<int>() {minX, minY, maxX, maxY};
                go = coordinateExtrems.Min() >= minCoordinate  || coordinateExtrems.Max() <= maxCoordinate ;
            } while (go || !IsEmpty(tempCoordinates));

            allNewCoordinates.ForEach(c => layer.Remove(c));
            newLayer.AddRange(allNewTempCoordinates);

            return newLayer;
        }

        private List<Coordinate> GetSurroundingCoordinates(int minX, int minY, int maxX, int maxY, int z)
        {
            List<Coordinate> surroundings = new List<Coordinate>(); 
            for (int x = minX-1; x <= maxX + 1; x++)
            {
                surroundings.Add(new Coordinate(x, minY-1, z, W, '.'));
                surroundings.Add(new Coordinate(x, maxY + 1, z, W, '.'));
            }

            for (int y = minY; y <= maxY; y++)
            {
                surroundings.Add(new Coordinate(minX-1, y, z, W, '.'));
                surroundings.Add(new Coordinate(maxX+1, y, z, W, '.'));
            }

            return surroundings;
        }

        private bool IsEmpty(List<Coordinate> coordinates)
        {
            return !coordinates.Any(c => c.IsActive);
        }

        private List<Coordinate> Copy(List<Coordinate> coordinates)
        {
            List<Coordinate> copyList = new List<Coordinate>();
            coordinates.ForEach(c => copyList.Add(c.Copy(true)));
            return copyList;
        }
    }
}
