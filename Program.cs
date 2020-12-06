using AdventOfCode2020_1.DayResolvers.Day7;

namespace AdventOfCode2020_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region Day 1

            //READ ALL AT ONCE
            //int day = 1;
            //int task = 1;
            //string input = FileLoader.LoadInput(day, task);
            //Day1Resolver resolver = new Day1Resolver();
            //FileLoader.SaveResult(day,task + 1, resolver.Resolve2(input));

            //READ WITH STREAM
            //int day = 1;
            //int task = 1;
            //StreamReader input = FileLoader.LoadInputAsStream(day, task);
            //Day1Resolver resolver = new Day1Resolver();
            //FileLoader.SaveResult(day, task + 1, resolver.Resolve(input, 2));

            #endregion

            #region Day 2

            //int day = 2;
            //int task = 2;
            //StreamReader input = FileLoader.LoadInputAsStream(day, task-1);
            //Day2Resolver resolver = new Day2Resolver();
            //FileLoader.SaveResult(day, task, resolver.Resolve(input, task));

            #endregion

            #region Day 3

            //int day = 3;
            //int task = 2;
            //StreamReader input = FileLoader.LoadInputAsStream(day, 1);
            //Day3Resolver resolver = new Day3Resolver();
            //FileLoader.SaveResult(day, task, resolver.Resolve(input, task));

            #endregion

            #region Day 4

            //int day = 4;
            //int task = 2;
            //StreamReader input = FileLoader.LoadInputAsStream(day, 1);
            //Day4Resolver resolver = new Day4Resolver();
            //FileLoader.SaveResult(day, task, resolver.Resolve(input, task));

            #endregion

            #region Day 5

            //int day = 5;
            //int task = 2;
            //StreamReader input = FileLoader.LoadInputAsStream(day, 1);
            //Day5Resolver resolver = new Day5Resolver();
            //FileLoader.SaveResult(day, task, resolver.Resolve(input, task));

            #endregion

            #region Day 6

            //int day = 6;
            //int task = 2;
            //StreamReader input = FileLoader.LoadInputAsStream(day, 1);
            //Day6Resolver resolver = new Day6Resolver();
            //FileLoader.SaveResult(day, task, resolver.Resolve(input, task));

            #endregion

            #region Day 7

            var day = 7;
            var task = 0;
            var input = FileLoader.LoadInputAsStream(day, task);
            var resolver = new Day7Resolver();
            FileLoader.SaveResult(day, task, resolver.Resolve(input, 1));

            #endregion
        }
    }
}