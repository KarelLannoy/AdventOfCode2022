using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day02
    {
        public static void Part1()
        {
            var games = File.ReadAllLines(@"Inputs/Input02.txt").ToList().Select(l => new Tuple<string, string>(l.Split(" ")[0], l.Split(" ")[1]));
            var score = 0;

            foreach (var game in games)
            {
                switch (game.Item2)
                {
                    case "X":
                        score += 1;
                        switch (game.Item1)
                        {
                            case "A":
                                score += 3;
                                break;
                            case "B":
                                score += 0;
                                break;
                            case "C":
                                score += 6;
                                break;
                        }
                        break;
                    case "Y":
                        score += 2;
                        switch (game.Item1)
                        {
                            case "A":
                                score += 6;
                                break;
                            case "B":
                                score += 3;
                                break;
                            case "C":
                                score += 0;
                                break;
                        }
                        break;
                    case "Z":
                        score += 3;
                        switch (game.Item1)
                        {
                            case "A":
                                score += 0;
                                break;
                            case "B":
                                score += 6;
                                break;
                            case "C":
                                score += 3;
                                break;
                        }
                        break;
                }
            }

            Console.WriteLine(score);
        }

        public static void Part2()
        {
            var games = File.ReadAllLines(@"Inputs/Input02.txt").ToList().Select(l => new Tuple<string, string>(l.Split(" ")[0], l.Split(" ")[1]));
            var score = 0;

            foreach (var game in games)
            {
                switch (game.Item2)
                {
                    case "X":
                        score += 0;
                        switch (game.Item1)
                        {
                            case "A":
                                score += 3;
                                break;
                            case "B":
                                score += 1;
                                break;
                            case "C":
                                score += 2;
                                break;
                        }
                        break;
                    case "Y":
                        score += 3;
                        switch (game.Item1)
                        {
                            case "A":
                                score += 1;
                                break;
                            case "B":
                                score += 2;
                                break;
                            case "C":
                                score += 3;
                                break;
                        }
                        break;
                    case "Z":
                        score += 6;
                        switch (game.Item1)
                        {
                            case "A":
                                score += 2;
                                break;
                            case "B":
                                score += 3;
                                break;
                            case "C":
                                score += 1;
                                break;
                        }
                        break;
                }
            }

            Console.WriteLine(score);
        }
    }
}
