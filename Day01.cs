using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day01
    {
        public static void Part1()
        {
            List<long> totalCalories = CalculateCalories();
            Console.WriteLine(totalCalories.Max());
        }

        public static void Part2()
        {
            List<long> totalCalories = CalculateCalories();
            Console.WriteLine(totalCalories.OrderByDescending(c=>c).Take(3).Sum(c=>c));
        }

        private static List<long> CalculateCalories()
        {
            var lines = File.ReadAllLines(@"Inputs/Input01.txt").ToList();
            var totalCalories = new List<long>();
            long previousNumber = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (string.IsNullOrEmpty(line))
                {
                    totalCalories.Add(previousNumber);
                    previousNumber = 0;
                }
                else
                    previousNumber += long.Parse(line);
            }
            return totalCalories;
        }
    }
}
