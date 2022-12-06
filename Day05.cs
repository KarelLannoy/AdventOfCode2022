using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day05
    {
        //                [M]     [W] [M]    
        //            [L] [Q] [S] [C] [R]    
        //            [Q] [F] [F] [T] [N] [S]
        //    [N]     [V] [V] [H] [L] [J] [D]
        //    [D] [D] [W] [P] [G] [R] [D] [F]
        //[T] [T] [M] [G] [G] [Q] [N] [W] [L]
        //[Z] [H] [F] [J] [D] [Z] [S] [H] [Q]
        //[B] [V] [B] [T] [W] [V] [Z] [Z] [M]
        // 1   2   3   4   5   6   7   8   9 
        public static void Part1()
        {
            var textParts = File.ReadAllText(@"Inputs/Input05.txt").Split(Environment.NewLine + Environment.NewLine);
            var stacksText = textParts[0];
            var moves = textParts[1].Split(Environment.NewLine).ToList();
            Dictionary<int, Stack<string>> places = new Dictionary<int, Stack<string>>();
            for (int i = 1; i <= 9; i++)
            {
                places.Add(i, new Stack<string>());
            }
            var lines = stacksText.Split(Environment.NewLine).ToList();
            lines.RemoveAt(lines.Count - 1);
            for (int lineCount = lines.Count - 1; lineCount >= 0; lineCount--)
            {
                for (int i = 0; i < 9; i++)
                {
                    var crate = lines[lineCount].Substring(i * 3 + (i * 1), 3);
                    if (!string.IsNullOrWhiteSpace(crate))
                        places[i + 1].Push(crate.Substring(1, 1));
                }
            }
            foreach (var move in moves)
            {
                var moveParts = move.Split(" ");
                var nrOfItems = int.Parse(moveParts[1]);
                var fromPlace = int.Parse(moveParts[3]);
                var toPlace = int.Parse(moveParts[5]);

                for (int i = 1; i <= nrOfItems; i++)
                {
                    var value = places[fromPlace].Pop();
                    places[toPlace].Push(value);
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                Console.Write(places[i].Peek());
            }
            Console.WriteLine();
        }

        public static void Part2()
        {
            var textParts = File.ReadAllText(@"Inputs/Input05.txt").Split(Environment.NewLine + Environment.NewLine);
            var stacksText = textParts[0];
            var moves = textParts[1].Split(Environment.NewLine).ToList();
            Dictionary<int, Stack<string>> places = new Dictionary<int, Stack<string>>();
            for (int i = 1; i <= 9; i++)
            {
                places.Add(i, new Stack<string>());
            }
            var lines = stacksText.Split(Environment.NewLine).ToList();
            lines.RemoveAt(lines.Count - 1);
            for (int lineCount = lines.Count - 1; lineCount >= 0; lineCount--)
            {
                for (int i = 0; i < 9; i++)
                {
                    var crate = lines[lineCount].Substring(i * 3 + (i * 1), 3);
                    if (!string.IsNullOrWhiteSpace(crate))
                        places[i + 1].Push(crate.Substring(1, 1));
                }
            }
            foreach (var move in moves)
            {
                var moveParts = move.Split(" ");
                var nrOfItems = int.Parse(moveParts[1]);
                var fromPlace = int.Parse(moveParts[3]);
                var toPlace = int.Parse(moveParts[5]);


                var values = places[fromPlace].PopRange<string>(nrOfItems);
                foreach (var value in values)
                {
                    places[toPlace].Push(value);
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                Console.Write(places[i].Peek());
            }
            Console.WriteLine();
        }
    }
}
