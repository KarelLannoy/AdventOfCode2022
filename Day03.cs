using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day03
    {
        public static void Part1()
        {
            var score = 0;
            var rucksacks = File.ReadAllLines(@"Inputs/Input03.txt").ToList().Select(i => new Tuple<string, string>(i.Substring(0, i.Length / 2), i.Substring(i.Length / 2))).ToList();
            foreach (var rucksack in rucksacks)
            {
                var same = rucksack.Item1.Intersect(rucksack.Item2).First();
                int index = char.ToUpper(same) - 64;
                if (char.IsUpper(same))
                    index += 26;
                score += index;
            }
            Console.WriteLine(score);

        }

        public static void Part2()
        {
            var score = 0;
            var rucksacks = File.ReadAllLines(@"Inputs/Input03.txt").ToList().Select(i => i).ToList();
            for (int i = 0; i < rucksacks.Count; i+=3)
            {
                var same = rucksacks[i].Intersect(rucksacks[i + 1]).Intersect(rucksacks[i + 2]).First();
                int index = char.ToUpper(same) - 64;
                if (char.IsUpper(same))
                    index += 26;
                score += index;
            }

            Console.WriteLine(score);
        }
    }
}
