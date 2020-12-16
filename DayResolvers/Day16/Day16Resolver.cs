using System.Globalization;
using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day16
{
    public class Day16Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            LanguageProcessor dp = new LanguageProcessor(input);
            if (task == 1) return Resolve1Internal(dp);

            return Resolve2Internal(dp);
        }

        private string Resolve2Internal(LanguageProcessor input)
        {
            return input.GetDeparturesMultiplied("departure").ToString(CultureInfo.InvariantCulture);
        }

        private string Resolve1Internal(LanguageProcessor input)
        {
            return input.GetSumOfInvalidTickets().ToString(CultureInfo.InvariantCulture);
        }
    }
}