using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020_1.DayResolvers.Day20
{
    public class Tile
    {
        public long Id { get; set; }
        public bool[][] TileArray { get; set; }

        public int Size => TileArray.Length;

        private int rotate = 0;

        public int Rotate
        {
            get => rotate;
            set { rotate = value == 360 ? 0 : value; }
        }

        #region Current sides

        public string Left
        {
            get
            {
                switch (Rotate)
                {
                    case 0:
                        return OriginalLeft;
                    case 90:
                        return OriginalBottom;
                    case 180:
                        return OriginalRight.Reverse().ToString();
                    case 270:
                        return OriginalTop.Reverse().ToString();
                    default:
                        return OriginalLeft;
                }
            }
        }

        public string Right
        {
            get
            {
                switch (Rotate)
                {
                    case 0:
                        return OriginalRight;
                    case 90:
                        return OriginalTop;
                    case 180:
                        return OriginalLeft.Reverse().ToString();
                    case 270:
                        return OriginalBottom.Reverse().ToString();
                    default:
                        return OriginalLeft;
                }
            }
        }

        public string Top
        {
            get
            {
                switch (Rotate)
                {
                    case 0:
                        return OriginalTop;
                    case 90:
                        return OriginalLeft.Reverse().ToString();
                    case 180:
                        return OriginalBottom.Reverse().ToString();
                    case 270:
                        return OriginalRight;
                    default:
                        return OriginalLeft;
                }
            }
        }

        public string Bottom
        {
            get
            {
                switch (Rotate)
                {
                    case 0:
                        return OriginalBottom;
                    case 90:
                        return OriginalRight.Reverse().ToString();
                    case 180:
                        return OriginalTop.Reverse().ToString();
                    case 270:
                        return OriginalLeft;
                    default:
                        return OriginalLeft;
                }
            }
        }

        #endregion

        #region Sides

        private string left = null;

        private string OriginalLeft
        {
            get
            {
                if (left == null)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        left += TileArray[i][0];
                    }
                }

                return left;
            }
        }

        private string top = null;

        private string OriginalTop
        {
            get
            {
                if (top == null)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        top += TileArray[0][i];
                    }
                }

                return top;
            }
        }

        private string right = null;

        private string OriginalRight
        {
            get
            {
                if (right == null)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        right += TileArray[i][Size - 1];
                    }
                }

                return right;
            }
        }

        private string bottom = null;

        private string OriginalBottom
        {
            get
            {
                if (bottom == null)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        bottom += TileArray[Size - 1][i];
                    }
                }

                return bottom;
            }
        }

        #endregion

        public Tile(List<string> lines)
        {
            ParseId(lines[0]);
            TileArray = new bool[lines.Count - 1][];
            for (int i = 1; i < lines.Count; i++)
            {
                TileArray[i-1] = new bool[lines.Count - 1];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    TileArray[i-1][j] = lines[i][j] == '#';
                }
            }
        }

        private void ParseId(string line)
        {
            var match = new Regex(@"Tile\s([0-9]+):").Match(line);
            Id = long.Parse(match.Groups[1].Value);
        }
    }
}
