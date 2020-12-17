using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class CubePocket
    {
        Dictionary<int, List<Coordinate>> zLayers = new Dictionary<int, List<Coordinate>>();

        public CubePocket(StreamReader input)
        {
           string line = input.ReadLine();
           zLayers.Add(0, new List<Coordinate>());
           int currentY = 0;
           while (line!=null)
           {
               var charArray = line.ToCharArray();
               for (int i = 0; i < charArray.Length; i++)
               {
                   zLayers[0].Add(new Coordinate(i, currentY, 0, charArray[i]));
               }

               currentY++;
               line = input.ReadLine();
           }
        }

        public string GetActiveCubes(int loops)
        {
            string print = Print(0);
            long sum = 0;
            for (int i = 1; i <= loops; i++)
            {
                Loop();
                print += Print(i);
            }

            var zKeys = zLayers.Keys.ToList();
            zKeys.ForEach(zKey => { sum += zLayers[zKey].Count(c => c.IsActive); });
            return sum + Environment.NewLine + print;
        }

        private string Print(int loop)
        {
            string toReturn = loop + " --------------------------------------------------------";
            var minZ = zLayers.Keys.Min();
            var maxZ = zLayers.Keys.Max();

            for (int z = minZ; z <= maxZ; z++)
            {
                toReturn += Environment.NewLine + Environment.NewLine + "Z=" + z + Environment.NewLine;
                var layer = zLayers[z];
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


        private void Loop()
        {
            Dictionary<int, List<Coordinate>> newZLayers = new Dictionary<int, List<Coordinate>>();
            var zKeys = zLayers.Keys.ToList();
            zKeys.Sort();
            zKeys.ForEach(key =>
            {
                newZLayers.Add(key, LoopZ(key));
            });

            var minZ = zKeys.Min();
            var maxZ = zKeys.Max();

            var lastZ = minZ;
            List<int> addedZs = new List<int>();
            while (!IsEmpty(newZLayers[lastZ]))
            {
                var copy = Copy(zLayers[lastZ]);
                lastZ--;
                copy.ForEach(c => c.Z = lastZ);
                zLayers.Add(lastZ, copy);
                addedZs.Add(lastZ);
                newZLayers.Add(lastZ, LoopZ(lastZ));
            }

            addedZs.ForEach(z => zLayers.Remove(z));
            newZLayers.Remove(lastZ);

            addedZs.Clear();
            lastZ = maxZ;
            while (!IsEmpty(newZLayers[lastZ]))
            {
                var copy = Copy(zLayers[lastZ]);
                lastZ++;
                copy.ForEach(c => c.Z = lastZ);
                zLayers.Add(lastZ, copy);
                addedZs.Add(lastZ);
                newZLayers.Add(lastZ, LoopZ(lastZ));
            }

            addedZs.ForEach(z => zLayers.Remove(z));
            newZLayers.Remove(lastZ);

            zLayers = newZLayers;

        }

        private List<Coordinate> LoopZ(int zKey)
        {
            List<Coordinate> newLayer = new List<Coordinate>();
            var layer = zLayers[zKey];
            layer.ForEach(coordinate =>
            {
                newLayer.Add(coordinate.Evaluate(zLayers));
            });

            var minX = newLayer.Select(c => c.X).Min();
            var minY = newLayer.Select(c => c.Y).Min();
            var maxX = newLayer.Select(c => c.X).Max();
            var maxY = newLayer.Select(c => c.Y).Max();

            List<Coordinate> newCoordinates = new List<Coordinate>();
            List<Coordinate> allNewCoordinates = new List<Coordinate>();

            List<Coordinate> tempCoordinates = new List<Coordinate>();
            List<Coordinate> allNewTempCoordinates = new List<Coordinate>();
            do
            {
                newCoordinates = GetSurroundingCoordinates(minX, minY, maxX, maxY, zKey);
                allNewCoordinates.AddRange(newCoordinates);
                allNewTempCoordinates.AddRange(tempCoordinates);
                tempCoordinates = new List<Coordinate>();
                layer.AddRange(newCoordinates);
                newCoordinates.ForEach(coordinate => tempCoordinates.Add(coordinate.Evaluate(zLayers)));
                minX--;
                minY--;
                maxX++;
                maxY++;
            } while (!IsEmpty(tempCoordinates));

            allNewCoordinates.ForEach(c => layer.Remove(c));
            newLayer.AddRange(allNewTempCoordinates);

            return newLayer;
        }

        private List<Coordinate> GetSurroundingCoordinates(int minX, int minY, int maxX, int maxY, int z)
        {
            List<Coordinate> surroundings = new List<Coordinate>(); 
            for (int x = minX-1; x <= maxX + 1; x++)
            {
                surroundings.Add(new Coordinate(x, minY-1, z, '.'));
                surroundings.Add(new Coordinate(x, maxY + 1, z, '.'));
            }

            for (int y = minY; y <= maxY; y++)
            {
                surroundings.Add(new Coordinate(minX-1, y, z, '.'));
                surroundings.Add(new Coordinate(maxX+1, y, z, '.'));
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
