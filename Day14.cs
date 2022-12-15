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
                    for (int y = Math.Min(int.Parse(points[i].Split(",")[1]), int.Parse(points[i + 1].Split(",")[1])); y <= Math.Max(int.Parse(points[i].Split(",")[1]), int.Parse(points[i + 1].Split(",")[1])); y++)
                    {
                        for (int x = Math.Min(int.Parse(points[i].Split(",")[0]), int.Parse(points[i + 1].Split(",")[0])); x <= Math.Max(int.Parse(points[i].Split(",")[0]), int.Parse(points[i + 1].Split(",")[0])); x++)
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
                FallOnePartTwo(new Point(500, 0), cave, lowestY);
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
                    _full = true;
                cave.Add(point, false);
            }
        }
    }
}
