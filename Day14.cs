using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day14
    {
        private static bool _intoTheAbys = false;
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"Inputs/Input14.txt").ToList();
            var cave = MapCave(lines);
            var lowestY = cave.Max(c => c.Key.Y);
            while (!_intoTheAbys)
            {
                FallOne(new Point(500,0), cave, lowestY);
            }
            Console.WriteLine(cave.Count(k => !k.Value));
        }

        private static void FallOne(Point point, Dictionary<Point, bool> cave, int lowestY)
        {
            var fallen = false;
            var newLocations = new List<Point> { new Point(point.X, point.Y + 1), new Point(point.X - 1, point.Y + 1), new Point(point.X + 1, point.Y + 1) };
            foreach (var location in newLocations)
            {
                if (location.Y > lowestY)
                {
                    _intoTheAbys = true;
                    return;
                }
                if (!cave.ContainsKey(location))
                {
                    fallen = true;
                    FallOne(location, cave, lowestY);
                    return;
                }
            }
            if (!fallen)
            {
                cave.Add(point, false);
            }
        }

        private static Dictionary<Point, bool> MapCave(List<string> lines)
        {
            Dictionary<Point, bool> blockedList = new Dictionary<Point, bool>();
            foreach (var line in lines)
            {
                var points = line.Split(" -> ");
                for (int i = 0; i < points.Length - 1; i++)
                {
                    var firstPoinX = int.Parse(points[i].Split(",")[0]);
                    var firstPoinY = int.Parse(points[i].Split(",")[1]);
                    var secondPoinX = int.Parse(points[i + 1].Split(",")[0]);
                    var secondPoinY = int.Parse(points[i + 1].Split(",")[1]);
                    for (int y = Math.Min(firstPoinY, secondPoinY); y <= Math.Max(firstPoinY, secondPoinY); y++)
                    {
                        for (int x = Math.Min(firstPoinX, secondPoinX); x <= Math.Max(firstPoinX, secondPoinX); x++)
                        {
                            if (!blockedList.ContainsKey(new Point(x, y)))
                            {
                                blockedList.Add(new Point(x, y), true);
                            }
                            
                        }
                    }
                }
            }
            return blockedList;
        }
        private static bool _full = false;
        public static void Part2()
        {
            var lines = File.ReadAllLines(@"Inputs/Input14.txt").ToList();
            var cave = MapCave(lines);
            var caveRocks = cave.Count;
            var lowestY = cave.Max(c => c.Key.Y) + 2;
            while (!_full)
            {
                FallOnePartTwo(new Point(500, 0), cave, lowestY);
            }
            //for (int y = cave.Min(m => m.Key.Y); y <= cave.Max(m => m.Key.Y); y++)
            //{
            //    StringBuilder b = new StringBuilder();
            //    for (int x = cave.Min(m => m.Key.X); x <= cave.Max(m => m.Key.X); x++)
            //    {
            //        if (cave.ContainsKey(new Point(x, y)))
            //        {
            //            b.Append(cave[new Point(x, y)] ? "#" : "O");    
            //        }
            //        else
            //            b.Append(".");
            //    }
            //    Console.WriteLine(b.ToString());
            //}
            Console.WriteLine(cave.Count(k => !k.Value));
        }

        private static void FallOnePartTwo(Point point, Dictionary<Point, bool> cave, int lowestY)
        {
            var fallen = false;
            var newLocations = new List<Point> { new Point(point.X, point.Y + 1), new Point(point.X - 1, point.Y + 1), new Point(point.X + 1, point.Y + 1) };
            foreach (var location in newLocations)
            {
                if (!cave.ContainsKey(location) && location.Y < lowestY)
                {
                    fallen = true;
                    FallOnePartTwo(location, cave, lowestY);
                    return;
                }
                if (!cave.ContainsKey(location) && location.Y >= lowestY)
                {
                    fallen = false;
                    break;
                }
            }
            if (!fallen && !cave.ContainsKey(point))
            {
                if (point == new Point(500,0))
                {
                    _full = true;
                }
                cave.Add(point, false);
            }
        }

    }
}
