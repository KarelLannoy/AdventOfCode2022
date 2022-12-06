using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day06
    {

        public static void Part1()
        {
            var input = File.ReadAllText(@"Inputs/Input06.txt");

            var buffer = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (buffer.Contains(input[i]))
                {
                    var position = buffer.IndexOf(input[i]);
                    for (int pos = 0; pos <= position; pos++)
                    {
                        buffer.RemoveAt(0);
                    }
                }
                buffer.Add(input[i]);
                if (buffer.Count() == 4)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }

        public static void Part2()
        {
            var input = File.ReadAllText(@"Inputs/Input06.txt");

            var buffer = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (buffer.Contains(input[i]))
                {
                    var position = buffer.IndexOf(input[i]);
                    for (int pos = 0; pos <= position; pos++)
                    {
                        buffer.RemoveAt(0);
                    }
                }
                buffer.Add(input[i]);
                if (buffer.Count() == 14)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }

    
}
