using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day6
{
    public class Day6Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            var cdForms = LoadCDForms(input);

            if (task == 1) return Resolve1Internal(cdForms);

            return Resolve2Internal(cdForms);
        }

        private string Resolve2Internal(List<CDForm> cdForms)
        {
            return cdForms.Sum(form => form.AnsweredByAll.Count).ToString();
        }

        private string Resolve1Internal(List<CDForm> cdForms)
        {
            return cdForms.Sum(form => form.Answers.Count).ToString();
        }

        private List<CDForm> LoadCDForms(StreamReader input)
        {
            var cdForms = new List<CDForm>();
            var currentLines = new List<string>();
            string line;
            do
            {
                line = input.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    cdForms.Add(new CDForm(currentLines.ToArray()));
                    currentLines.Clear();
                }
                else
                {
                    currentLines.Add(line);
                }
            } while (line != null);


            return cdForms;
        }
    }
}