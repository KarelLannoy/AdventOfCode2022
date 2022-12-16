using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day11
    {
        public static void Part1()
        {
            var instructions = File.ReadAllLines(@"Inputs/Input11.txt").ToList();
            List<Monkey> monkeyList = new List<Monkey>();

            for (int i = 0; i < instructions.Count; i+=7)
            {
                Monkey m = new Monkey();
                m.MonkeyName = int.Parse(instructions[i].Split(" ")[1].Split(":")[0]);
                m.Item = instructions[i + 1].Substring(18).Split(",").Select(x => long.Parse(x)).ToList();
                m.Operation = instructions[i + 2].Split("=")[1].Trim();
                m.Test = long.Parse(instructions[i + 3].Split("by")[1].Trim());
                m.MonkeyIfTrue = int.Parse(instructions[i + 4].Substring(29));
                m.MonkeyIfFalse = int.Parse(instructions[i + 5].Substring(30));
                monkeyList.Add(m);
            }

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeyList)
                {
                    foreach (var item in monkey.Item)
                    {
                        var evalOperation = monkey.Operation.Replace("old", item.ToString());
                        var operationParts = evalOperation.Split(" ");
                        long newValue = 0;
                        switch (operationParts[1])
                        {
                            case "*":
                                newValue = long.Parse(operationParts[0]) * long.Parse(operationParts[2]);
                                break;
                            case "+":
                                newValue = long.Parse(operationParts[0]) + long.Parse(operationParts[2]);
                                break;

                        }

                        newValue = newValue / 3;

                        if (newValue % monkey.Test == 0)
                            monkeyList.First(m=>m.MonkeyName == monkey.MonkeyIfTrue).Item.Add(newValue);
                        else
                            monkeyList.First(m => m.MonkeyName == monkey.MonkeyIfFalse).Item.Add(newValue);

                        monkey.inspections++;
                    }
                    monkey.Item = new List<long>();
                }
            }

            Console.WriteLine(monkeyList.OrderByDescending(m => m.inspections).Take(2).Select(m=>m.inspections).Aggregate((m, y) => m * y));
            
        }

        public static void Part2()
        {
            var instructions = File.ReadAllLines(@"Inputs/Input11.txt").ToList();
            List<Monkey> monkeyList = new List<Monkey>();

            for (int i = 0; i < instructions.Count; i += 7)
            {
                Monkey m = new Monkey();
                m.MonkeyName = int.Parse(instructions[i].Split(" ")[1].Split(":")[0]);
                m.Item = instructions[i + 1].Substring(18).Split(",").Select(x => long.Parse(x)).ToList();
                m.Operation = instructions[i + 2].Split("=")[1].Trim();
                m.Test = long.Parse(instructions[i + 3].Split("by")[1].Trim());
                m.MonkeyIfTrue = int.Parse(instructions[i + 4].Substring(29));
                m.MonkeyIfFalse = int.Parse(instructions[i + 5].Substring(30));
                monkeyList.Add(m);
            }

            var lcm = Helpers.LcmOfList(monkeyList.Select(m => m.Test).ToList());

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeyList)
                {
                    foreach (var item in monkey.Item)
                    {
                        var evalOperation = monkey.Operation.Replace("old", item.ToString());
                        var operationParts = evalOperation.Split(" ");
                        long newValue = 0;
                        switch (operationParts[1])
                        {
                            case "*":
                                newValue = long.Parse(operationParts[0]) * long.Parse(operationParts[2]);
                                break;
                            case "+":
                                newValue = long.Parse(operationParts[0]) + long.Parse(operationParts[2]);
                                break;

                        }

                        if (newValue > lcm)
                        {
                            newValue = newValue % lcm;
                        }

                        if (newValue % monkey.Test == 0)
                            monkeyList.First(m => m.MonkeyName == monkey.MonkeyIfTrue).Item.Add(newValue);
                        else
                            monkeyList.First(m => m.MonkeyName == monkey.MonkeyIfFalse).Item.Add(newValue);

                        monkey.inspections++;
                    }
                    monkey.Item = new List<long>();
                }
            }

            Console.WriteLine(monkeyList.OrderByDescending(m => m.inspections).Take(2).Select(m => m.inspections).Aggregate((x,y) => x*y));

        }

        private class Monkey
        {
            public int MonkeyName { get; set; }
            public List<long> Item { get; set; }

            public string Operation { get; set; }
            public long Test { get; set; }

            public int MonkeyIfTrue { get; set; }
            public int MonkeyIfFalse { get; set; }

            public long inspections { get; set; }
        }
    }
}
