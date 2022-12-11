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

            var lcm = lcmOfList(monkeyList.Select(m => m.Test).ToList());

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

        public static long lcmOfList(List<long> list)
        {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < list.Count; i++)
                {

                    // lcm_of_array_elements (n1, n2, ... 0) = 0.
                    // For negative number we convert into
                    // positive and calculate lcm_of_array_elements.
                    if (list[i] == 0)
                    {
                        return 0;
                    }
                    else if (list[i] < 0)
                    {
                        list[i] = list[i] * (-1);
                    }
                    if (list[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete
                    // division i.e. without remainder then replace
                    // number with quotient; used for find next factor
                    if (list[i] % divisor == 0)
                    {
                        divisible = true;
                        list[i] = list[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number
                // from array multiply with lcm_of_array_elements
                // and store into lcm_of_array_elements and continue
                // to same divisor for next factor finding.
                // else increment divisor
                if (divisible)
                {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate
                // we found all factors and terminate while loop.
                if (counter == list.Count)
                {
                    return lcm_of_array_elements;
                }
            }
        }
    }
}
