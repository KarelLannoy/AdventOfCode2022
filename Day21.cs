using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day21
    {
        public static void Part1()
        {
            var monkeys = new Dictionary<string, Monkey>();
            var monkeyLines = File.ReadAllText(@"Inputs/Input21.txt").Split(Environment.NewLine).ToList();
            foreach (var line in monkeyLines)
            {
                var monkey = new Monkey();
                monkey.Name = line.Split(":")[0];
                if (char.IsNumber(line.Split(": ")[1][0]))
                {
                    monkey.Value = int.Parse(line.Split(": ")[1]);
                }
                else
                {
                    monkey.LeftReference = line.Split(": ")[1].Split(" ")[0];
                    monkey.Operator = line.Split(": ")[1].Split(" ")[1];
                    monkey.RightReference = line.Split(": ")[1].Split(" ")[2];
                }
                monkeys.Add(monkey.Name, monkey);

            }

            Console.WriteLine(monkeys["root"].GetValue(monkeys));
        }

        public static void Part2()
        {
            var monkeys = new Dictionary<string, Monkey>();
            var monkeyLines = File.ReadAllText(@"Inputs/Input21.txt").Split(Environment.NewLine).ToList();
            foreach (var line in monkeyLines)
            {
                var monkey = new Monkey();
                monkey.Name = line.Split(":")[0];
                if (char.IsNumber(line.Split(": ")[1][0]))
                {
                    monkey.Value = int.Parse(line.Split(": ")[1]);
                }
                else
                {
                    monkey.LeftReference = line.Split(": ")[1].Split(" ")[0];
                    monkey.Operator = line.Split(": ")[1].Split(" ")[1];
                    monkey.RightReference = line.Split(": ")[1].Split(" ")[2];
                }
                monkeys.Add(monkey.Name, monkey);

            }

            for (long i = 3587647562900 - 200; i < 3587647562900; i+=1)
            {
                monkeys["humn"].Value = i;
                if (monkeys["root"].IsEqual(monkeys))
                {
                    Console.WriteLine(i);
                    break;
                }

            }

        }

        private class Monkey
        {
            public string Name { get; set; }
            private long? _value;
            public long Value
            {
                set
                {
                    _value = value;
                }
            }
            public string LeftReference { get; set; }
            public string RightReference { get; set; }
            public string Operator { get; set; }
            public bool IsEqual(Dictionary<string, Monkey> monkeys)
            {
                return monkeys[LeftReference].GetValue(monkeys) == monkeys[RightReference].GetValue(monkeys);
            }

            public long GetValue(Dictionary<string, Monkey> monkeys)
            {
                if (_value.HasValue)
                    return _value.Value;

                switch (this.Operator)
                {
                    case "+":
                        return monkeys[LeftReference].GetValue(monkeys) + monkeys[RightReference].GetValue(monkeys);
                    case "-":
                        return monkeys[LeftReference].GetValue(monkeys) - monkeys[RightReference].GetValue(monkeys);
                    case "*":
                        return monkeys[LeftReference].GetValue(monkeys) * monkeys[RightReference].GetValue(monkeys);
                    case "/":
                        return monkeys[LeftReference].GetValue(monkeys) / monkeys[RightReference].GetValue(monkeys);
                }
                throw new Exception("should not get here");
            }
        }


    }
}
