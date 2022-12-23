using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day23
    {
        public static void Part1()
        {
            var lines = File.ReadAllText(@"Inputs/Input23.txt").Split(Environment.NewLine);
            var elves = new HashSet<Point>();
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var pos in line)
                {
                    if (pos == '#')
                    {
                        elves.Add(new Point(x, y));
                    }
                    x++;
                }
                y++;
            }


            elves = PlayRound(elves, 10);
            Console.WriteLine(elves.Print());

            Console.WriteLine((((Math.Abs(elves.Max(p => p.X) + 1) - elves.Min(p=>p.X))) * ((Math.Abs(elves.Max(p => p.Y) + 1) - elves.Min(p => p.Y)))) - elves.Count);
        }

        private static HashSet<Point> PlayRound(HashSet<Point> elves, int numberOfRounds)
        {
            var directions = new List<string>() { "N", "S", "W", "E" };
            
            for (int i = 0; i < numberOfRounds; i++)
            {
                var moves = new Dictionary<Point, Point>();
                foreach (var elve in elves)
                {
                    var adjacentPoints = GetAdjacentPoints(elve);
                    var neighbours = adjacentPoints.Where(p => elves.Contains(p.Value)).ToList();
                    if (neighbours.Count == 0)
                        continue;
                    for (int d = 0; d < directions.Count; d++)
                    {
                        var indexOfDirection = (d + i) % directions.Count;
                        var direction = directions[indexOfDirection];
                        var neigboursToCheck = neighbours.Where(n => n.Key.Contains(direction)).ToList();
                        if (neigboursToCheck.Count == 0)
                        {
                            if (!moves.Any(m=>m.Value == adjacentPoints[direction]))
                            {
                                moves.Add(elve, adjacentPoints[direction]);
                            }
                            else
                            {
                                moves.Remove(moves.First(m => m.Value == adjacentPoints[direction]).Key);
                            }
                            break;
                        }
                    }

                }

                var elvesWhoDontMove = elves.Where(e=>!moves.ContainsKey(e)).ToHashSet();
                foreach (var item in moves)
                {
                    if (!elvesWhoDontMove.Contains(item.Value))
                    {
                        elvesWhoDontMove.Add(item.Value);
                    }
                }
                elves = elvesWhoDontMove;
                //Console.WriteLine(elves.Print());
            }
            return elves;
        }

        private static Dictionary<string, Point> GetAdjacentPoints(Point elve)
        {
            var result = new Dictionary<string, Point>();
            result.Add("N", new Point(elve.X, elve.Y - 1));
            result.Add("NE", new Point(elve.X + 1, elve.Y - 1));
            result.Add("E", new Point(elve.X + 1, elve.Y));
            result.Add("SE", new Point(elve.X + 1, elve.Y + 1));
            result.Add("S", new Point(elve.X, elve.Y + 1));
            result.Add("SW", new Point(elve.X - 1, elve.Y + 1));
            result.Add("W", new Point(elve.X - 1, elve.Y));
            result.Add("NW", new Point(elve.X - 1, elve.Y - 1));
            return result;
        }

        public static void Part2()
        {
            var lines = File.ReadAllText(@"Inputs/Input23.txt").Split(Environment.NewLine);
            var elves = new HashSet<Point>();
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var pos in line)
                {
                    if (pos == '#')
                    {
                        elves.Add(new Point(x, y));
                    }
                    x++;
                }
                y++;
            }


            Console.WriteLine(PlayRoundUntilEnd(elves));
        }

        private static long PlayRoundUntilEnd(HashSet<Point> elves)
        {
            var directions = new List<string>() { "N", "S", "W", "E" };
            var numberOfRounds = 1000000;

            for (long i = 0; i < numberOfRounds; i++)
            {
                var moves = new Dictionary<Point, Point>();
                foreach (var elve in elves)
                {
                    var adjacentPoints = GetAdjacentPoints(elve);
                    var neighbours = adjacentPoints.Where(p => elves.Contains(p.Value)).ToList();
                    if (neighbours.Count == 0)
                        continue;
                    for (int d = 0; d < directions.Count; d++)
                    {
                        var indexOfDirection = (int)((d + i) % directions.Count);
                        var direction = directions[indexOfDirection];
                        var neigboursToCheck = neighbours.Where(n => n.Key.Contains(direction)).ToList();
                        if (neigboursToCheck.Count == 0)
                        {
                            if (!moves.Any(m => m.Value == adjacentPoints[direction]))
                            {
                                moves.Add(elve, adjacentPoints[direction]);
                            }
                            else
                            {
                                moves.Remove(moves.First(m => m.Value == adjacentPoints[direction]).Key);
                            }
                            break;
                        }
                    }

                }

                if (moves.Count == 0)
                {
                    return i + 1;
                }
                var elvesWhoDontMove = elves.Where(e => !moves.ContainsKey(e)).ToHashSet();
                foreach (var item in moves)
                {
                    if (!elvesWhoDontMove.Contains(item.Value))
                    {
                        elvesWhoDontMove.Add(item.Value);
                    }
                }
                elves = elvesWhoDontMove;
                //Console.WriteLine(elves.Print());
            }

            throw new Exception("Not enough rounds");
        }
    }
}
