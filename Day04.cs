using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day04
    {
        public static void Part1()
        {
            var pairs = File.ReadAllLines(@"Inputs/Input04.txt").ToList().Select(l => new Tuple<string, string>(l.Split(",")[0], l.Split(",")[1])).ToList();
            var numberOfConcerns = 0;
            foreach (var pair in pairs)
            {
                var lowerBound1 = int.Parse(pair.Item1.Split("-")[0]);
                var upperBound1 = int.Parse(pair.Item1.Split("-")[1]);
                var lowerBound2 = int.Parse(pair.Item2.Split("-")[0]);
                var upperBound2 = int.Parse(pair.Item2.Split("-")[1]);


                if ((lowerBound1 >= lowerBound2 && upperBound1 <= upperBound2) || lowerBound2 >= lowerBound1 && upperBound2 <= upperBound1)
                {
                    numberOfConcerns++;
                }


            }
            Console.WriteLine(numberOfConcerns);
        }

        public static void Part2()
        {
            var pairs = File.ReadAllLines(@"Inputs/Input04.txt").ToList().Select(l => new Tuple<string, string>(l.Split(",")[0], l.Split(",")[1])).ToList();
            var numberOfConcerns = 0;
            foreach (var pair in pairs)
            {
                var lowerBound1 = int.Parse(pair.Item1.Split("-")[0]);
                var upperBound1 = int.Parse(pair.Item1.Split("-")[1]);
                var lowerBound2 = int.Parse(pair.Item2.Split("-")[0]);
                var upperBound2 = int.Parse(pair.Item2.Split("-")[1]);


                if ((lowerBound1 >= lowerBound2 && lowerBound1 <= upperBound2) || (lowerBound1 <= lowerBound2 && upperBound1 >= lowerBound2) || (lowerBound2 >= lowerBound1 && lowerBound2 <= upperBound1) || (lowerBound2 <= lowerBound1 && upperBound2 >= lowerBound1)) 
                {
                    numberOfConcerns++;
                }


            }
            Console.WriteLine(numberOfConcerns);
        }
    }
}
