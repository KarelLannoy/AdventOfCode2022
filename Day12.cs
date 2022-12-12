using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day12
    {
        private static List<int> _possibleRoutes;
        private static Dictionary<Point, int> _memory;
        public static void Part1()
        {
            _possibleRoutes = new List<int>();
            _memory = new Dictionary<Point, int>();
            Dictionary<Point, int> map = new Dictionary<Point, int>();
            var input = File.ReadAllLines(@"Inputs/Input12.txt").ToList();
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    map.Add(new Point(x, y), input[y][x]);
                }
            }

            var start = map.FirstOrDefault(kvp => kvp.Value == 83).Key;
            var end = map.FirstOrDefault(kvp => kvp.Value == 69).Key;
            map[start] = 97;
            map[end] = 122;

            Move(start, map, 0, end, new List<Point>(), new List<Point>());

            Console.WriteLine(_possibleRoutes.Min());
        }

        public static void Move(Point current, Dictionary<Point, int> map, int nrOfSTeps, Point end, List<Point> visited, List<Point> skipPoints)
        {
            if (current == end)
            {
                _possibleRoutes.Add(nrOfSTeps);
                return;
            }
            if(_memory.ContainsKey(current))
            {
                if (_memory[current] <= nrOfSTeps)
                    return;
                else
                    _memory[current] = nrOfSTeps;
            }else
            {
                _memory.Add(current, nrOfSTeps);
            }
            visited.Add(current);
            
            var newSteps = new List<Point>() { new Point(current.X + 1, current.Y), new Point(current.X - 1, current.Y), new Point(current.X, current.Y + 1), new Point(current.X, current.Y - 1) };
            foreach (var step in newSteps)
            {
                if (skipPoints.Contains(step))
                    continue;
                if (visited.Contains(step))
                    continue;
                if (!map.ContainsKey(step))
                    continue;
                if (map[current] + 1 >= map[step])
                {
                    Move(step, map, nrOfSTeps + 1, end, new List<Point>(visited), skipPoints);
                }
            }
        }

        public static void Part2()
        {
            var minStepsRoute = new List<int>();
            Dictionary<Point, int> map = new Dictionary<Point, int>();
            var input = File.ReadAllLines(@"Inputs/Input12.txt").ToList();
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    map.Add(new Point(x, y), input[y][x]);
                }
            }
            var start = map.FirstOrDefault(kvp => kvp.Value == 83).Key;
            var end = map.FirstOrDefault(kvp => kvp.Value == 69).Key;
            map[start] = 97;
            map[end] = 122;

            var startingPoints = map.Where(x => x.Value == 97).ToList();

            foreach (var point in startingPoints)
            {
                _possibleRoutes = new List<int>();
                Move(point.Key, map, 0, end, new List<Point>(), startingPoints.Select(p=>p.Key).ToList());
                if (_possibleRoutes.Count > 0)
                    minStepsRoute.Add(_possibleRoutes.Min());            
            }

            Console.WriteLine(minStepsRoute.Min());
        }
    }
}
