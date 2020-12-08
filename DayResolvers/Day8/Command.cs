using System;

namespace AdventOfCode2020_1.DayResolvers.Day8
{
    public enum CommandName
    {
        NOP, ACC, JMP, Unknown
    }
    public class Command
    {
        public string Raw { get; set; }
        public CommandName Name { get; set; }
        public int Value { get; set; }

        public Command(string line)
        {
            Raw = line;
            Parse();
        }

        public Tuple<int, int> Execute(int currentStep, int currentValue)
        {
            switch (Name)
            {
                case CommandName.ACC:
                    return new Tuple<int, int>(currentStep+1, currentValue + Value);
                case CommandName.JMP:
                    return new Tuple<int, int>(currentStep + Value, currentValue);
                default:
                    return new Tuple<int, int>(currentStep + 1, currentValue);
            }
        }

        private void Parse()
        {
            string[] splits = Raw.Split(' ');
            Name = ParseCommand(splits[0]);
            Value = int.Parse(splits[1]);
        }

        private CommandName ParseCommand(string val)
        {
            switch (val.Trim().ToLower())
            {
                case "nop":
                    return CommandName.NOP;
                case "jmp":
                    return CommandName.JMP;
                case "acc":
                    return CommandName.ACC;
            }

            return CommandName.Unknown;
        }
    }
}
