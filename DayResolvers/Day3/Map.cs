﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day3
{
    public class Map
    {
        public Map(StreamReader inputReader, Tuple<int, int> initial, Tuple<int, int> increment)
        {
            InitialPosition = initial;
            Increment = increment;
            LoadRows(inputReader);
        }

        public List<Row> Rows { get; set; } = new List<Row>();
        public Tuple<int, int> Increment { get; set; } = new Tuple<int, int>(0, 0);
        public Tuple<int, int> InitialPosition { get; set; } = new Tuple<int, int>(0, 0);
        public Tuple<int, int> CurrentPosition { get; set; } = new Tuple<int, int>(0, 0);

        public void Reset()
        {
            CurrentPosition = new Tuple<int, int>(InitialPosition.Item1, InitialPosition.Item2);
        }

        private void LoadRows(StreamReader inputReader)
        {
            string line;
            while ((line = inputReader.ReadLine()) != null) Rows.Add(new Row(line));
        }

        public int Resolve1()
        {
            var treeCounter = 0;
            for (var i = InitialPosition.Item2; i < Rows.Count; i = i + Increment.Item2)
            {
                if (!Rows[i].IsOpen(CurrentPosition.Item1)) treeCounter++;
                CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1 + Increment.Item1,
                    CurrentPosition.Item2 + Increment.Item2);
            }

            return treeCounter;
        }
    }
}