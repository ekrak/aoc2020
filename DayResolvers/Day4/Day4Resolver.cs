using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day4
{
    public class Day4Resolver
    {

        public string Resolve(StreamReader input, int task)
        {
            var passports = LoadPassports(input);
            if (task == 1)
            {
                return Resolve1Internal(passports);

            }

            return Resolve2Internal(passports);

        }

        private List<Passport> LoadPassports(StreamReader input)
        {
            List<Passport> passports = new List<Passport>();
            List<string> currentPassportData = new List<string>();
            string line;
            do
            {
                line = input.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(new Passport(currentPassportData.ToArray()));
                    currentPassportData.Clear();
                }
                else
                {
                    currentPassportData.Add(line);
                }
            } while (line != null);


            return passports;
        }

        private string Resolve2Internal(List<Passport> passports)
        {
            return passports.Count(pass => pass.IsValid2()).ToString();
        }

        private string Resolve1Internal(List<Passport> passports)
        {
        
    turn passports.Count(pass => pass.IsValid()).ToString();
        }

        
    }
}
