using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day20
{
    public class SquareImage
    {
        public List<Tile> Tiles = new List<Tile>();

        public SquareImage(StreamReader input)
        {
            List<string> currentTile = new List<string>();

            string line = input.ReadLine();
            while (line != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    currentTile.Add(line);
                }
                else
                {
                    Tiles.Add(new Tile(currentTile));
                    currentTile.Clear();
                }

                line = input.ReadLine();
            }
        }

    }
}
