using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day2
{
    public class Day2Resolver
    {
        public string Resolve(StreamReader input, int task)
        {
            if (task == 1)
            {
                return Resolve1Internal(input);

            }

            return Resolve2Internal(input);

        }

        private string Resolve2Internal(StreamReader input)
        {
            int counter = 0;
            string line;
            while ((line = input.ReadLine()) != null)
            {
                PasswordChecker checker = new PasswordChecker(line);
                if (checker.Validate2()) counter++;
            }

            return counter.ToString();
        }

        private string Resolve1Internal(StreamReader input)
        {
            int counter = 0;
            string line;
            while ((line = input.ReadLine()) != null)
            {
                PasswordChecker checker = new PasswordChecker(line);
        
      if (checker.Validate()) counter++;
            }

            return counter.ToString();
        }
    }
}
