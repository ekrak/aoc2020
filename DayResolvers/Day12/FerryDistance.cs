using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020_1.DayResolvers.Day12
{
    public class FerryDistance
    {
        public List<Tuple<char, int>> Instructions { get; set; } = new List<Tuple<char, int>>();
        public char Direction { get; set; } = 'E';
        public int CurrentWE { get; set; } = 0;
        public int CurrentNS { get; set; } = 0;

        private char[] rightArray = new char[4] {'N', 'E', 'S', 'W'};
        private char[] leftArray = new char[4] { 'N', 'W', 'S', 'E' };


        public int CurrentWaypointWE { get; set; } = 10;
        public int CurrentWaypointNS { get; set; } = -1;


        public FerryDistance(StreamReader input)
        {
            string line = input.ReadLine();
            while (line != null)
            {
                Instructions.Add(ParseInstruction(line));
                line = input.ReadLine();
            }
        }

        private Tuple<char, int> ParseInstruction(string line)
        {
            char tupleChar = line[0];
            int tupleInt = int.Parse(line.Substring(1, line.Length - 1));
            return new Tuple<char, int>(tupleChar, tupleInt);
        }

        private void Reset()
        {
            Direction = 'E';
            CurrentWE = 0;
            CurrentWaypointWE = 10;
            CurrentWaypointNS = -1;
        }

        public int GetManhattanDistance()
        {
            FollowInstructions();
            return Math.Abs(CurrentNS) + Math.Abs(CurrentWE);
        }

        public int GetManhattanDistance2()
        {
            FollowInstructions2();
            return Math.Abs(CurrentNS) + Math.Abs(CurrentWE);
        }

        private void FollowInstructions()
        {
            Reset();
            Instructions.ForEach(Move);
        }

        private void FollowInstructions2()
        {
            Reset();
            Instructions.ForEach(Move2);
        }

        private void Move(Tuple<char, int> instruction)
        {
            switch (instruction.Item1)
            {
                case 'F':
                    MoveForward(instruction.Item2);
                    break;
                case 'L':
                    TurnLeft(instruction.Item2);
                    break;
                case 'R':
                    TurnRight(instruction.Item2);
                    break;
                case 'N':
                    CurrentNS -= instruction.Item2;
                    break;
                case 'S':
                    CurrentNS += instruction.Item2;
                    break;
                case 'E':
                    CurrentWE += instruction.Item2;
                    break;
                case 'W':
                    CurrentWE -= instruction.Item2;
                    break;
            }
        }

        private void MoveForward(int instructionItem2)
        {
            switch (Direction)
            {
                case 'N':
                    CurrentNS -= instructionItem2;
                    break;
                case 'S':
                    CurrentNS += instructionItem2;
                    break;
                case 'E':
                    CurrentWE += instructionItem2;
                    break;
                case 'W':
                    CurrentWE -= instructionItem2;
                    break;
            }
        }

        private void TurnLeft(int value)
        {
            var currentIndex = Array.IndexOf(leftArray, Direction);
            var turnBy = value / 90;
            var tempIndex = currentIndex + turnBy;
            var finalIndex = tempIndex % 4;
            Direction = leftArray[finalIndex];
        }

        private void TurnRight(int value)
        {
            var currentIndex = Array.IndexOf(rightArray, Direction);
            var turnBy = value / 90;
            var tempIndex = currentIndex + turnBy;
            var finalIndex = tempIndex % 4;
            Direction = rightArray[finalIndex];
        }

        private void Move2(Tuple<char, int> instruction)
        {
            switch (instruction.Item1)
            {
                case 'F':
                    MoveForward2(instruction.Item2);
                    break;
                case 'L':
                    TurnLeft2(instruction.Item2);
                    break;
                case 'R':
                    TurnRight2(instruction.Item2);
                    break;
                case 'N':
                    CurrentWaypointNS -= instruction.Item2;
                    break;
                case 'S':
                    CurrentWaypointNS += instruction.Item2;
                    break;
                case 'E':
                    CurrentWaypointWE += instruction.Item2;
                    break;
                case 'W':
                    CurrentWaypointWE -= instruction.Item2;
                    break;
            }
        }

        private void MoveForward2(int instructionItem2)
        {
            var moveNS = CurrentWaypointNS * instructionItem2;
            var moveWE = CurrentWaypointWE * instructionItem2;
            CurrentNS += moveNS;
            CurrentWE += moveWE;
        }

        private void TurnLeft2(int instructionItem2)
        {
            var prevCurrentWaypointNS = CurrentWaypointNS;
            var prevCurrentWaypointWE = CurrentWaypointWE;

            switch (instructionItem2)
            {
                case 90:
                    CurrentWaypointNS = -prevCurrentWaypointWE;
                    CurrentWaypointWE = prevCurrentWaypointNS;
                    break;
                case 180:
                    CurrentWaypointNS = -prevCurrentWaypointNS;
                    CurrentWaypointWE = -prevCurrentWaypointWE;
                    break;
                case 270:
                    CurrentWaypointNS = prevCurrentWaypointWE;
                    CurrentWaypointWE = -prevCurrentWaypointNS;
                    break;
                default:
                    break;
            }
        }

        private void TurnRight2(int instructionItem2)
        {
            var prevCurrentWaypointNS = CurrentWaypointNS;
            var prevCurrentWaypointWE = CurrentWaypointWE;

            switch (instructionItem2)
            {
                case 90:
                    CurrentWaypointNS = prevCurrentWaypointWE;
                    CurrentWaypointWE = -prevCurrentWaypointNS;
                    break;
                case 180:
                    CurrentWaypointNS = -prevCurrentWaypointNS;
                    CurrentWaypointWE = -prevCurrentWaypointWE;
                    break;
                case 270:
                    CurrentWaypointNS = -prevCurrentWaypointWE;
                    CurrentWaypointWE = prevCurrentWaypointNS;
                    break;
                default:
                    break;
            }
        }
    }
}
