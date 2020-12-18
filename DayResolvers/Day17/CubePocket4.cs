using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day17
{
    public class CubePocket4
    {
        Dictionary<int, CubePocket> wLayers = new Dictionary<int, CubePocket>();

        public CubePocket4(StreamReader input)
        {
           CubePocket pocket = new CubePocket(input);
           wLayers.Add(0,pocket);
        }

        public CubePocket4(CubePocket input)
        {
            wLayers.Add(0, input);
        }

        public string GetActiveCubes(int loops)
        {
            string print = "";
            long sum = 0;
            for (int i = 1; i <= loops; i++)
            {
                Loop();
                //print += Print(i) + Environment.NewLine;
            }

            var zKeys = wLayers.Keys.ToList();
            zKeys.ForEach(zKey =>
            {
                sum += wLayers[zKey].GetActiveCount();
            });
            return sum.ToString() + Environment.NewLine + print;
        }

        public int GetMaxCoordinate()
        {
            List<int> values = new List<int>();
            var keys = wLayers.Keys.ToList();
            keys.ForEach(key =>
            {
                values.Add(wLayers[key].GetMaxCoordinate());
            });

            values.AddRange(keys);
            return values.Max();
        }

        public int GetMinCoordinate()
        {
            List<int> values = new List<int>();
            var keys = wLayers.Keys.ToList();
            keys.ForEach(key =>
            {
                values.Add(wLayers[key].GetMinCoordinate());
            });

            values.AddRange(keys);
            return values.Min();
        }

        private string Print(int i)
        {
            string print = "????????????  LOOP " + i + "  ??????????????" + Environment.NewLine;
            var keys = wLayers.Keys.ToList();
            keys.Sort();
            keys.ForEach(key => { print += wLayers[key].Print(i) + Environment.NewLine; } );

            return print;
        }

        private void Loop()
        {
            Dictionary<int, CubePocket> newWLayers = new Dictionary<int, CubePocket>();
            var wKeys = wLayers.Keys.ToList();
            wKeys.Sort();
            wKeys.ForEach(key =>
            {
                CubePocket pocket = new CubePocket(wLayers[key].Loop(wLayers));
                pocket.W = key;
                newWLayers.Add(key, pocket);
            });

            var minW = wKeys.Min();
            var maxW = wKeys.Max();

            var minCoordinate = GetMinCoordinate();
            var maxCoordinate = GetMaxCoordinate();

            var lastW = minW;
            List<int> addedWs = new List<int>();
            bool go = lastW >= minCoordinate ;
            while (go || !newWLayers[lastW].IsEmpty())
            {
                go = lastW >= minCoordinate ;
                var copy = wLayers[lastW].Copy();
                lastW--;
                copy.W = lastW;
                wLayers.Add(lastW, copy);
                addedWs.Add(lastW);
                CubePocket newCubePocket = new CubePocket(wLayers[lastW].Loop(wLayers));
                newCubePocket.W = lastW;
                newWLayers.Add(lastW, newCubePocket);
            }

            addedWs.ForEach(z => wLayers.Remove(z));
            if(lastW!=minW)
                newWLayers.Remove(lastW);

            addedWs.Clear();
            lastW = maxW;
            go = lastW <= maxCoordinate;
            while (go || !newWLayers[lastW].IsEmpty())
            {
                go = lastW <= maxCoordinate ;
                var copy = wLayers[lastW].Copy();
                lastW++;
                copy.W = lastW;
                wLayers.Add(lastW, copy);
                addedWs.Add(lastW);
                CubePocket newCubePocket = new CubePocket(wLayers[lastW].Loop(wLayers));
                newCubePocket.W = lastW;
                newWLayers.Add(lastW, newCubePocket);
            }

            addedWs.ForEach(w => wLayers.Remove(w));

            if (lastW != maxW)
                newWLayers.Remove(lastW);

            wLayers = newWLayers;

        }
    }
}
