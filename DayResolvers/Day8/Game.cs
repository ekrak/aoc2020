using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day8
{
    public class Game
    {
        public List<Command> Commands { get; set; } = new List<Command>();

        public int Value { get; set; } = 0;

        public int CurrentStep { get; set; } = 0;

        public Game(StreamReader input)
        {
            string line = input.ReadLine();
            while (line != null)
            {
                Commands.Add(new Command(line));
                line = input.ReadLine();
            }
        }

        public int Run()
        {
            List<Command> executedCommands = new List<Command>();
            Command currentCommand = Commands.First();

            while (!executedCommands.Contains(currentCommand))
            {
                var result = currentCommand.Execute(CurrentStep, Value);
                executedCommands.Add(currentCommand);
                currentCommand = Commands[result.Item1];
                CurrentStep = result.Item1;
                Value = result.Item2;
            }

            return Value;
        }

        //A bit dumb - try all the possibilities and hope it won't take forever :D 
        public int Run2()
        {
            var allJmps = Commands.Where(cmd => cmd.Name == CommandName.JMP).ToList();
            var allNops = Commands.Where(cmd => cmd.Name == CommandName.NOP).ToList();

            foreach (var jmp in allJmps)
            {
                jmp.Name = CommandName.NOP;
                var result = InternalRun();
                if (result.Item2) 
                    return result.Item1;

                jmp.Name = CommandName.JMP;
                CurrentStep = 0;
                Value = 0;

            }

            foreach (var nop in allNops)
            {
                nop.Name = CommandName.JMP;
                var result = InternalRun();
                if (result.Item2)
                    return result.Item1;

                nop.Name = CommandName.NOP;
                CurrentStep = 0;
                Value = 0;

            }

            return Run();
        }

        private Tuple<int, bool> InternalRun()
        {
            List<Command> executedCommands = new List<Command>();
            Command currentCommand = Commands.First();
            bool isLastCommand = false;

            while (!executedCommands.Contains(currentCommand) && !isLastCommand)
            {
                isLastCommand = Commands.IndexOf(currentCommand) == Commands.Count - 1;
                var result = currentCommand.Execute(CurrentStep, Value);
                executedCommands.Add(currentCommand);
                if(!isLastCommand)
                    currentCommand = Commands[result.Item1];
                CurrentStep = result.Item1;
                Value = result.Item2;
            }

            return new Tuple<int, bool>(Value, isLastCommand);
        }
    }
}
