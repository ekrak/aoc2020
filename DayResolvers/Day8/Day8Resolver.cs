﻿using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day8
{
    public class Day8Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            if (task == 1) return Resolve1Internal(input);

            return Resolve2Internal(input);
        }

        private string Resolve2Internal(StreamReader input)
        {
            Game game = new Game(input);
            return game.Run2().ToString();
        }

        private string Resolve1Internal(StreamReader input)
        {
            Game game = new Game(input);
            return game.Run().ToString();
        }
    }
}