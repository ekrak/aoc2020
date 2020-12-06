using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day5
{
    public class Day5Resolver
    {

        public string Resolve(StreamReader input, int task)
        {
            List<BoardingPass> boardingPasses = LoadBoardingPasses(input);

            if (task == 1)
            {
                return Resolve1Internal(boardingPasses);

            }

            return Resolve2Internal(boardingPasses);

        }

        private List<BoardingPass> LoadBoardingPasses(StreamReader input)
        {
            List<BoardingPass> boardingPasses = new List<BoardingPass>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                boardingPasses.Add(new BoardingPass(line.Trim()));
            }

            return boardingPasses;
        }

        private string Resolve2Internal(List<BoardingPass> boardingPasses)
        {
            var ids = boardingPasses.Select(pass => pass.GetId()).ToList();
            var result = Enumerable.Range(ids.Min(), ids.Max() - ids.Min()).Except(ids).ToList();
            return result.Single().ToString();
        }

        private string Resolve1Internal(List<Boa
    ass> boardingPasses)
        {
            return boardingPasses.Select(pass => pass.GetId()).Max().ToString();
        }

        
    }
}
