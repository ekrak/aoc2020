using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day4
{
    public class Passport
    {
        public string[] Input { get; set; }
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }

        private string[] eyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public Passport(string[] inputLines)
        {
            Input = inputLines;
            Parse();
        }

        public bool IsValid()
        {
            return Byr != null && Iyr != null && Eyr != null && Hgt != null 
                   && Hcl != null && Ecl != null && Pid != null;
        }

        public bool IsValid2()
        {
            return IsValid() && ValidateData();
        }

        private bool ValidateData()
        {
            return IsNumberInRange(Byr, 1920, 2002) && IsNumberInRange(Iyr, 2010, 2020)
                    && IsNumberInRange(Eyr, 2020, 2030) && IsValidHeight(Hgt) && IsValidHairColor(Hcl)
                    && IsValidEyeColor(Ecl) && IsValidPid(Pid);
        }

        private bool IsValidPid(string val)
        {
            if (val.Length == 9 && char.IsDigit(val[0]))
            {
                return int.TryParse(val, out int valNum);
            }

            return false;
        }

        private bool IsNumberInRange(string val, int min, int max)
        {
            return int.TryParse(val, out int valNum) && valNum >= min && valNum <= max;
        }

        private bool IsValidHeight(string val)
        {
            if (val.EndsWith("cm"))
            {
                string substring = val.Substring(0, val.Length - 2);
                return IsNumberInRange(substring, 150, 193);
            }
            
            if (val.EndsWith("in"))
            {
                string substring = val.Substring(0, val.Length - 2);
                return IsNumberInRange(substring, 59, 76);
            }

            return false;
        }

        private bool IsValidHairColor(string val)
        {
            if (val.StartsWith("#") && val.Length == 7)
            {
                string substring = val.Substring(1, val.Length - 1);
                return Regex.IsMatch(substring, "[a-f0-9]+");
            }

            return false;
        }

        private bool IsValidEyeColor(string val)
        {
            return eyeColors.Contains(val);
        }

        private void Parse()
        {
            foreach (var line in Input)
            {
                string[] data = line.Trim().Split(' ');
                ParseData(data);
            }
        }

        private void ParseData(string[] data)
        {
            foreach (var part in data)
            {
                string[] keyValue = part.Trim().Split(':');
                AssignData(keyValue[0], keyValue[1]);
            }
        }

        private void AssignData(string key, string value)
        {
            switch (key.ToLower())
            {
                case "byr":
                    Byr = value;
                    break;
                case "iyr":
                    Iyr = value;
                    break;
                case "eyr":
                    Eyr = value;
                    break;
                case "hgt":
                    Hgt = value;
                    break;
                case "hcl":
                    Hcl = value;
                    break;
                case "ecl":
                    Ecl = value;
                    break;
                case "pid":
                    Pid = value;
                    break;
                case "cid":
                    Cid = value;
                    break;

            }
        }
    }
}
