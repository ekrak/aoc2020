using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day6
{
    public class CDForm
    {
        public CDForm(string[] lines)
        {
            Lines = lines;
            Analyze();
        }

        public string[] Lines { get; set; }

        public List<char> Answers { get; set; }

        public List<char> AnsweredByAll { get; set; }

        private void Analyze()
        {
            var allChars = new List<char>();
            for (var i = 0; i < Lines.Length; i++) allChars.AddRange(Lines[i]);

            Answers = allChars.Distinct().ToList();

            AnsweredByAll = Answers.Where(character => allChars.Count(c => c == character) == Lines.Length).ToList();
        }
    }
}