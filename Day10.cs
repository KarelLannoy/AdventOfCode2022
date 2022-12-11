using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day10
    {
        public static void Part1()
        {
            var instructions = File.ReadAllLines(@"Inputs/Input10.txt").ToList();

            var signalStrengths = new List<long>();
            long x = 1;
            int cycle = 0;

            foreach (var instruction in instructions)
            {
                var instructionCycles = instruction == "noop" ? 1 : 2;
                for (int i = 0; i < instructionCycles; i++)
                {
                    cycle++;
                    if (cycle == 20 || (cycle - 20) % 40 == 0)
                    {
                        signalStrengths.Add(cycle * x);
                    }
                    
                }
                if (cycle >= 220)
                {
                    break;
                }
                if (instruction != "noop")
                {
                    x += long.Parse(instruction.Split(" ")[1]);
                }
            }

            Console.WriteLine(signalStrengths.Sum());
        }

        public static void Part2()
        {
            var instructions = File.ReadAllLines(@"Inputs/Input10.txt").ToList();
            long x = 1;
            int cycle = 0;
            var line = 0;
            Dictionary<int, string> lines = new Dictionary<int, string>();
            for (int i = 0; i < 6; i++)
            {
                lines[i] = "";
            }
            foreach (var instruction in instructions)
            {
                var instructionCycles = instruction == "noop" ? 1 : 2;
                for (int i = 0; i < instructionCycles; i++)
                {
                    cycle++;
                    if (cycle == x + (line * 40) || cycle == x + (line * 40) + 1 || cycle == x + (line * 40) + 2)
                        lines[line] += "#";
                    else
                        lines[line] += ".";

                    if (cycle % 40 == 0)
                    {
                        line++;
                    }
                }
                if (cycle >= 240)
                {
                    break;
                }
                if (instruction != "noop")
                {
                    x += long.Parse(instruction.Split(" ")[1]);
                }
            }
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }
}
