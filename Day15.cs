using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day15
    {
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"Inputs/Input15.txt").ToList();
            Dictionary<Point, Point> sensors = new Dictionary<Point, Point>();
            foreach (var line in lines)
            {
                var sensorAndBeacon = line.Split(":");
                var sensorX = int.Parse(sensorAndBeacon[0].Substring(sensorAndBeacon[0].IndexOf("=") + 1, sensorAndBeacon[0].IndexOf(',') - (sensorAndBeacon[0].IndexOf("=") + 1)));
                var sensorY = int.Parse(sensorAndBeacon[0].Split(",")[1].Substring(3));
                var beaconX = int.Parse(sensorAndBeacon[1].Substring(sensorAndBeacon[1].IndexOf("=") + 1, sensorAndBeacon[1].IndexOf(',') - (sensorAndBeacon[1].IndexOf("=") + 1)));
                var beaconY = int.Parse(sensorAndBeacon[1].Split(",")[1].Substring(3));
                sensors.Add(new Point(sensorX, sensorY), new Point(beaconX, beaconY));
            }
            var map = new Dictionary<Point, string>();
            foreach (var sensor in sensors)
            {
                map.Add(sensor.Key, "S");
                if (!map.ContainsKey(sensor.Value))
                    map.Add(sensor.Value, "B");
            }
            var rowToCheck = 2000000;
            foreach (var sensor in sensors)
                FillMapWithNoBeaconZone(map, sensor, rowToCheck);
            //Console.Write(map.Print());
            Console.WriteLine(map.Where(p => p.Key.Y == rowToCheck).Count(p => p.Value == "#"));
        }
        private static void FillMapWithNoBeaconZone(Dictionary<Point, string> map, KeyValuePair<Point, Point> sensor, int rowToCHeck)
        {
            var allPointsWithinManhattenDistance = FindPointsWithinManhattenDistance(sensor.Key, ManhattanDistance(sensor.Key, sensor.Value), rowToCHeck);
            foreach (var point in allPointsWithinManhattenDistance)
            {
                if (!map.ContainsKey(point))
                    map.Add(point, "#");
            }
        }
        private static HashSet<Point> FindPointsWithinManhattenDistance(Point startPoint, int distance, int rowToCheck)
        {
            var result = new HashSet<Point>();
            var ySpread = Math.Abs(startPoint.Y - rowToCheck);
            for (int x = startPoint.X - (distance - ySpread); x <= startPoint.X + (distance - ySpread); x++)
            {
                result.Add(new Point(x, rowToCheck));
            }
            return result;
        }
        private static int ManhattanDistance(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
        public static void Part2()
        {
            var lines = File.ReadAllLines(@"Inputs/Input15.txt").ToList();
            Dictionary<Point, Point> sensors = new Dictionary<Point, Point>();
            foreach (var line in lines)
            {
                var sensorAndBeacon = line.Split(":");
                var sensorX = int.Parse(sensorAndBeacon[0].Substring(sensorAndBeacon[0].IndexOf("=") + 1, sensorAndBeacon[0].IndexOf(',') - (sensorAndBeacon[0].IndexOf("=") + 1)));
                var sensorY = int.Parse(sensorAndBeacon[0].Split(",")[1].Substring(3));
                var beaconX = int.Parse(sensorAndBeacon[1].Substring(sensorAndBeacon[1].IndexOf("=") + 1, sensorAndBeacon[1].IndexOf(',') - (sensorAndBeacon[1].IndexOf("=") + 1)));
                var beaconY = int.Parse(sensorAndBeacon[1].Split(",")[1].Substring(3));
                sensors.Add(new Point(sensorX, sensorY), new Point(beaconX, beaconY));
            }
            var rowToCheckMin = 0;
            var rowToCheckMax = 4000000;
            foreach (var sensor in sensors)
            {
                FindPointsWithinManhattenDistance2(sensor.Key, ManhattanDistance(sensor.Key, sensor.Value), rowToCheckMin, rowToCheckMax);
            }
            foreach (var row in _lines.OrderBy(l => l.Key))
            {
                var start = 0;
                foreach (var range in row.Value.OrderBy(r => r.Start.Value))
                {
                    if (range.Start.Value <= start + 1 && range.End.Value >= start)
                    {
                        start = range.End.Value;
                    }
                }
                if (start < rowToCheckMax)
                {
                    Console.WriteLine((((long)start + (long)1) * (long)4000000) + (long)row.Key);
                    break;
                }

            }
        }
        private static Dictionary<int, List<Range>> _lines = new Dictionary<int, List<Range>>();
        static void FindPointsWithinManhattenDistance2(Point startPoint, int distance, int rowToCheckMin, int rowToCheckMax)
        {
            for (int y = Math.Max((startPoint.Y - distance), rowToCheckMin); y <= Math.Min((startPoint.Y + distance), rowToCheckMax); y++)
            {
                var ySpread = Math.Abs(startPoint.Y - y);
                if (!_lines.ContainsKey(y))
                    _lines.Add(y, new List<Range>());
                _lines[y].Add(new Range(Math.Max((startPoint.X - (distance - ySpread)), rowToCheckMin), Math.Min((startPoint.X + (distance - ySpread)), rowToCheckMax)));
            }
        }
    }
}
