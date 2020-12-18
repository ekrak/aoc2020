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
            long sum = 0;
            for (int i = 1; i <= loops; i++)
            {
                Loop();
            }

            var zKeys = wLayers.Keys.ToList();
            zKeys.ForEach(zKey => { sum += wLayers[zKey].GetActiveCount(); });
            return sum.ToString();
        }

        private void Loop()
        {
            Dictionary<int, CubePocket> newWLayers = new Dictionary<int, CubePocket>();
            var wKeys = wLayers.Keys.ToList();
            wKeys.Sort();
            wKeys.ForEach(key =>
            {
                newWLayers.Add(key, new CubePocket(wLayers[key].Loop(wLayers)));
            });

            var minW = wKeys.Min();
            var maxW = wKeys.Max();

            var lastW = minW;
            List<int> addedWs = new List<int>();
            while (!newWLayers[lastW].IsEmpty())
            {
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
            newWLayers.Remove(lastW);

            addedWs.Clear();
            lastW = maxW;
            while (!newWLayers[lastW].IsEmpty())
            {
                var copy = wLayers[lastW].Copy();
                lastW++;
                copy.W = lastW;
                wLayers.Add(lastW, copy);
                addedWs.Add(lastW);
                newWLayers.Add(lastW, new CubePocket(wLayers[lastW].Loop(wLayers)));
            }

            addedWs.ForEach(w => wLayers.Remove(w));
            newWLayers.Remove(lastW);

            wLayers = newWLayers;

        }
    }
}
