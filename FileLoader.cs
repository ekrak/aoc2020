using System.IO;

namespace AdventOfCode2020_1
{
    public static class FileLoader
    {
        private const string basePath = "D:\\WorkOther\\AoC2020\\";

        public static string LoadInput(int day, int task)
        {
            var path = Path.Combine(basePath, day.ToString(), $"input{task}.txt");
            return File.ReadAllText(path);
        }

        public static StreamReader LoadInputAsStream(int day, int task)
        {
            var path = Path.Combine(basePath, day.ToString(), $"input{task}.txt");
            return new StreamReader(path);
        }

        public static void SaveResult(int day, int task, string result)
        {
            var path = Path.Combine(basePath, day.ToString(), $"result{task}.txt");
            File.WriteAllText(path, result);
        }
    }
}