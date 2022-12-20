using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day20
    {
        public static void Part1()
        {
            var sequence = File.ReadAllText(@"Inputs/Input20.txt").Split(Environment.NewLine).Select(s=>new Value { IntValue = int.Parse(s) }).ToList();
            var workingCopy = new List<Value>(sequence);
            //Console.WriteLine(workingCopy[0].IntValue + ", " + workingCopy[1].IntValue + ", " + workingCopy[2].IntValue + ", " + workingCopy[3].IntValue + ", " + workingCopy[4].IntValue + ", " + workingCopy[5].IntValue + ", " + workingCopy[6].IntValue);
            for (int i = 0; i < sequence.Count; i++)
            {
                var value = sequence[i];
                var valueIndex = workingCopy.IndexOf(value);
                var newValuePos = (valueIndex + value.IntValue) % (sequence.Count - 1);
                if (newValuePos <= 0)
                    newValuePos = (sequence.Count - 1) + newValuePos;
                workingCopy.RemoveAt(valueIndex);
                workingCopy.Insert(newValuePos, value);
                //Console.WriteLine(workingCopy[0].IntValue + ", " + workingCopy[1].IntValue + ", " + workingCopy[2].IntValue + ", " + workingCopy[3].IntValue + ", " + workingCopy[4].IntValue + ", " + workingCopy[5].IntValue + ", " + workingCopy[6].IntValue);
            }
            var indexOf0 = workingCopy.IndexOf(workingCopy.First(v => v.IntValue == 0));

            var value1000 = workingCopy[(indexOf0 + 1000) % (sequence.Count)].IntValue;
            var value2000 = workingCopy[(indexOf0 + 2000) % (sequence.Count)].IntValue;
            var value3000 = workingCopy[(indexOf0 + 3000) % (sequence.Count)].IntValue;

            Console.WriteLine(value1000 + value2000 + value3000);

        }

        public static void Part2()
        {
            var sequence = File.ReadAllText(@"Inputs/Input20.txt").Split(Environment.NewLine).Select(s => new ValuePart2 { IntValue = long.Parse(s) * 811589153 }).ToList();
            var workingCopy = new List<ValuePart2>(sequence);
            //Console.WriteLine(workingCopy[0].IntValue + ", " + workingCopy[1].IntValue + ", " + workingCopy[2].IntValue + ", " + workingCopy[3].IntValue + ", " + workingCopy[4].IntValue + ", " + workingCopy[5].IntValue + ", " + workingCopy[6].IntValue);
            for (int y = 0; y < 10; y++)
            {
                for (int i = 0; i < sequence.Count; i++)
                {
                    var value = sequence[i];
                    var valueIndex = workingCopy.IndexOf(value);
                    var newValuePos = (valueIndex + value.IntValue) % (sequence.Count - 1);
                    if (newValuePos <= 0)
                        newValuePos = (sequence.Count - 1) + newValuePos;
                    workingCopy.RemoveAt(valueIndex);
                    workingCopy.Insert((int)newValuePos, value);
                    //Console.WriteLine(workingCopy[0].IntValue + ", " + workingCopy[1].IntValue + ", " + workingCopy[2].IntValue + ", " + workingCopy[3].IntValue + ", " + workingCopy[4].IntValue + ", " + workingCopy[5].IntValue + ", " + workingCopy[6].IntValue);
                }
            }
            var indexOf0 = workingCopy.IndexOf(workingCopy.First(v => v.IntValue == 0));

            var value1000 = workingCopy[(indexOf0 + 1000) % (sequence.Count)].IntValue;
            var value2000 = workingCopy[(indexOf0 + 2000) % (sequence.Count)].IntValue;
            var value3000 = workingCopy[(indexOf0 + 3000) % (sequence.Count)].IntValue;

            Console.WriteLine(value1000 + value2000 + value3000);
        }

        private class Value
        {
            public int IntValue { get; set; }
        }

        private class ValuePart2
        {
            public long IntValue { get; set; }
        }

    }
}
