using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020_1.DayResolvers.Day18
{
    public enum Operation
    {
        Add,
        Multiply
    }


    public class MathGroup
    {
        public List<object> GroupObjects { get; set; } = new List<object>();

        public MathGroup(string line, out int lastI)
        {
            var cleanLine = line.Trim().Replace(" ", "");
            string previousNumber = string.Empty;
            for (lastI = 0; lastI < cleanLine.Length; lastI++)
            {
                var character = cleanLine[lastI];
                if (character == '(')
                {
                    GroupObjects.Add(new MathGroup(cleanLine.Substring(lastI + 1, cleanLine.Length - lastI - 1), out int returnI));
                    lastI += returnI + 1;
                }

                else if (character == ')')
                {
                    break;
                }

                else if (character == '+')
                {
                    GroupObjects.Add(Operation.Add);
                }

                else if (character == '*')
                {
                    GroupObjects.Add(Operation.Multiply);
                }

                else
                {
                    if (cleanLine.Length > lastI + 1 && int.TryParse(cleanLine[lastI+1].ToString(), out int testNum))
                    {
                        previousNumber += character;
                    }
                    else
                    {
                        GroupObjects.Add(int.Parse(previousNumber + character));
                        previousNumber = string.Empty;
                    }
                }
            }
        }

        public long Evaluate() => EvaluateInternal(GroupObjects);

        private long EvaluateInternal(List<object> groupObjects)
        {
            long num = EvaluateItem(groupObjects.First());
            Operation lastOperation = Operation.Add;
            for (int i = 1; i < groupObjects.Count; i++)
            {
                if (groupObjects[i] is Operation op)
                {
                    lastOperation = op;
                }
                else
                {
                    if (lastOperation == Operation.Add)
                    {
                        num += EvaluateItem(groupObjects[i]);
                    }
                    else
                    {
                        num *= EvaluateItem(groupObjects[i]);
                    }
                }
            }

            return num;

        }

        public long Evaluate2()
        {
            List<object> additionEvaluated = new List<object>();
            bool lastMultiply = true;
            for (int i = 0; i < GroupObjects.Count; i++)
            {
                if (GroupObjects[i] is Operation op)
                {
                    if (op == Operation.Add)
                    {
                        if (lastMultiply)
                        {
                            additionEvaluated.Add(EvaluateItem2(GroupObjects[i - 1]) +
                                                  EvaluateItem2(GroupObjects[i + 1]));
                            i++;
                            lastMultiply = false;
                        }
                        else
                        {
                            var evaluated = EvaluateItem2(additionEvaluated.Last()) + EvaluateItem2(GroupObjects[i + 1]);
                            additionEvaluated.RemoveAt(additionEvaluated.Count-1);
                            additionEvaluated.Add(evaluated);
                        }
                    }
                    else
                    {
                        if (lastMultiply)
                        {
                            additionEvaluated.Add(EvaluateItem2(GroupObjects[i-1]));
                        }
                        additionEvaluated.Add(Operation.Multiply);
                        lastMultiply = true;
                    }
                }
            }

            if (lastMultiply)
            {
                additionEvaluated.Add(EvaluateItem2(GroupObjects[GroupObjects.Count-1]));
            }

            return EvaluateInternal(additionEvaluated);

        }



        private long EvaluateItem(object obj)
        {
            if (obj is int intObj)
            {
                return Convert.ToInt64(intObj);
            }

            if (obj is long longObj)
            {
                return longObj;
            }

            if (obj is MathGroup mg)
            {
                return mg.Evaluate();
            }

            throw new ArgumentException();
        }


        private long EvaluateItem2(object obj)
        {
            if (obj is int intObj)
            {
                return Convert.ToInt64(intObj);
            }

            if (obj is long longObj)
            {
                return longObj;
            }

            if (obj is MathGroup mg)
            {
                return mg.Evaluate2();
            }

            throw new ArgumentException();
        }
    }
}
