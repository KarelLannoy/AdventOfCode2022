using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day09
    {
        public static void Part1()
        {
            var head = new Point(0, 0);
            var tail = new Point(0, 0);
            Dictionary<Point, int> visited = new Dictionary<Point, int>();
            visited.Add(tail, 1);
            var instructions = File.ReadAllLines(@"Inputs/Input09.txt").Select(l => new Tuple<string, int>(l.Split(" ")[0], int.Parse(l.Split(" ")[1]))).ToList();
            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.Item2; i++)
                {
                    switch (instruction.Item1)
                    {
                        case "R":
                            head.X += 1;
                            break;
                        case "U":
                            head.Y += 1;
                            break;
                        case "L":
                            head.X -= 1;
                            break;
                        case "D":
                            head.Y -= 1;
                            break;
                    }
                    tail = LetTailFollowHead(tail, head, visited);
                }
            }
            Console.WriteLine(visited.Count());
        }

        private static Point LetTailFollowHead(Point tail, Point head, Dictionary<Point, int> visited)
        {
            var difX = Math.Abs(head.X - tail.X);
            var difY = Math.Abs(head.Y - tail.Y);
            if (difX > 1 || difY > 1)
            {
                //Move Tail
                if (difY == 0 || (difX != 0 && difY != 0))
                {
                    if (head.X - tail.X > 0)
                        tail.X++;
                    else
                        tail.X--;
                }
                if (difX == 0 || (difX != 0 && difY != 0))
                {
                    if (head.Y - tail.Y > 0)
                        tail.Y++;
                    else
                        tail.Y--;
                }

                if (visited != null)
                {
                    if (visited.ContainsKey(tail))
                        visited[tail]++;
                    else
                        visited.Add(tail, 1);
                }
            }
            return tail;
        }

        public static void Part2()
        {
            var knotList = new List<Point>();
            for (int i = 0; i < 10; i++)
            {
                knotList.Add(new Point());
            }
            var tail = knotList[9];
            Dictionary<Point, int> visited = new Dictionary<Point, int>();
            visited.Add(tail, 1);
            var instructions = File.ReadAllLines(@"Inputs/Input09.txt").Select(l => new Tuple<string, int>(l.Split(" ")[0], int.Parse(l.Split(" ")[1]))).ToList();
            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.Item2; i++)
                {
                    switch (instruction.Item1)
                    {
                        case "R":
                            knotList[0] = new Point(knotList[0].X + 1, knotList[0].Y);
                            break;
                        case "U":
                            knotList[0] = new Point(knotList[0].X, knotList[0].Y + 1);
                            break;
                        case "L":
                            knotList[0] = new Point(knotList[0].X - 1, knotList[0].Y);
                            break;
                        case "D":
                            knotList[0] = new Point(knotList[0].X, knotList[0].Y - 1);
                            break;
                    }
                    for (int y = 1; y < knotList.Count; y++)
                    {
                        knotList[y] = LetTailFollowHead(knotList[y], knotList[y - 1], y == knotList.Count - 1 ? visited : null);
                    }
                }
            }
            Console.WriteLine(visited.Count());
        }
    }
}
