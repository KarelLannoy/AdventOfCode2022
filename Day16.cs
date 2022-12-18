using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day16
    {
        public static void Part1()
        {
            var valves = new Dictionary<string, Valve>();
            var lines = File.ReadAllLines(@"Inputs/Input16.txt").ToList();
            foreach (var line in lines)
            {
                var valve = new Valve();
                valve.Name = line.Substring(6, 2);
                valve.FlowRate = int.Parse(line.Split(";")[0].Split("=")[1]);
                valve.Tunnels = line.Split(";")[1].Replace("tunnels lead to valves", "").Replace("tunnel leads to valve", "").Trim().Split(",").ToList().Select(s => s.Trim()).ToList();
                valves.Add(valve.Name, valve);
            }
            var currentValve = valves["AA"];
            FindOptimalPath(currentValve, currentValve, valves, 0, 0, "", 30);
            Console.WriteLine(_optimalFlowRate);
            _optimalFlowRate = 0;
            _memo = new Dictionary<Position, int>();
        }

        private static int _optimalFlowRate = 0;
        private static Dictionary<Position, int> _memo = new Dictionary<Position, int>();
        private static Dictionary<string, Valve> _things = new Dictionary<string, Valve>();
        private static void FindOptimalPath(Valve currentValve, Valve previousValve, Dictionary<string, Valve> allVales, int steps, int flowRate, string path, int minutes)
        {
            if (steps >= minutes)
            {
                if (_optimalFlowRate < flowRate)
                {
                    _optimalFlowRate = flowRate;
                    _things = allVales;
                    Console.WriteLine(flowRate + " -> " + path);
                }
                return;
            }

            if (_memo.ContainsKey(new Position(currentValve.Name, steps)))
            {
                if (_memo[new Position(currentValve.Name, steps)] >= flowRate)
                {
                    return;
                }
                else
                {
                    _memo[new Position(currentValve.Name, steps)] = flowRate;
                }
            }
            else
            {
                _memo.Add(new Position(currentValve.Name, steps), flowRate);
            }


            path += currentValve.Name + " | ";
            if (!currentValve.Open && currentValve.FlowRate > 0)
            {
                var newValues = allVales.ToDictionary(entry => entry.Key, entry => (Valve)entry.Value.Clone());
                var newCurrentValue = newValues[currentValve.Name];
                var newSteps = steps + 1;
                var newFlowRate = flowRate + (newCurrentValue.FlowRate * (minutes - newSteps));
                newCurrentValue.Open = true;
                foreach (var neighbor in currentValve.Tunnels)
                {
                    FindOptimalPath(newValues[neighbor], currentValve, newValues, newSteps + 1, newFlowRate, path, minutes);
                }
            }
            foreach (var neighbor in currentValve.Tunnels)
            {
                if (previousValve.Open && previousValve.Name == neighbor)
                {
                    continue;
                }
                if (previousValve.FlowRate == 0 && previousValve.Name == neighbor)
                {
                    continue;
                }
                FindOptimalPath(allVales[neighbor], currentValve, allVales, steps + 1, flowRate, path, minutes);
            }

            return;
        }

        
        public static void Part2()
        {
            var valves = new Dictionary<string, Valve>();
            var lines = File.ReadAllLines(@"Inputs/Input16.txt").ToList();
            foreach (var line in lines)
            {
                var valve = new Valve();
                valve.Name = line.Substring(6, 2);
                valve.FlowRate = int.Parse(line.Split(";")[0].Split("=")[1]);
                valve.Tunnels = line.Split(";")[1].Replace("tunnels lead to valves", "").Replace("tunnel leads to valve", "").Trim().Split(",").ToList().Select(s => s.Trim()).ToList();
                valves.Add(valve.Name, valve);
            }
            var currentValve = valves["AA"];
            FindOptimalPath(currentValve, currentValve, valves, 0, 0, "", 26);
            //Console.WriteLine("me: " + _optimalFlowRate);
            var myPart = _optimalFlowRate;
            _memo = new Dictionary<Position, int>();
            _optimalFlowRate = 0;
            FindOptimalPath(currentValve, currentValve, _things, 0, 0, "", 26);
            Console.WriteLine(myPart+_optimalFlowRate);
        }
        private struct Position
        {
            public string Name;
            public int Step;
            public Position(string name, int step)
            {
                Name = name;
                Step = step;
            }
        }
        private class Valve : ICloneable
        {
            public string Name { get; set; }
            public int FlowRate { get; set; }
            public List<string> Tunnels { get; set; }
            public bool Open { get; set; }

            public Valve()
            {
                Tunnels = new List<string>();
            }

            public object Clone()
            {
                return new Valve()
                {
                    Name = Name,
                    FlowRate = FlowRate,
                    Open = Open,
                    Tunnels = Tunnels,
                };
            }
        }
    }
}
