using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day19
    {
        public static void Part1()
        {
            var bluePrints = new List<BluePrint>();
            var bluePrintsLine = File.ReadAllLines(@"Inputs/Input19.txt");
            foreach (var line in bluePrintsLine)
            {
                BluePrint bp = new BluePrint();
                bp.BluePrintNumber = int.Parse(line.Split(":")[0].Split(" ")[1]);
                bp.OreCosts = new Cost() { CostType = "Ore", Amount = int.Parse(line.Split("Each ore robot costs ")[1].Substring(0, 1)) };
                bp.ClayCost = new Cost() { CostType = "Ore", Amount = int.Parse(line.Split("Each clay robot costs ")[1].Substring(0, 1)) };
                bp.ObsidianCost = new List<Cost>() { new Cost() { CostType = "Ore", Amount = int.Parse(line.Split("Each obsidian robot costs ")[1].Substring(0, 1)) } ,
                    new Cost() { CostType = "Clay", Amount = int.Parse(line.Split("Each obsidian robot costs ")[1].Split(" clay.")[0].Split(" ")[3]) } };
                bp.GeodeCost = new List<Cost>() { new Cost() { CostType = "Ore", Amount = int.Parse(line.Split("Each geode robot costs ")[1].Substring(0, 1)) } ,
                    new Cost() { CostType = "Obsidian", Amount = int.Parse(line.Split("Each geode robot costs ")[1].Split(" obsidian.")[0].Split(" ")[3]) } };

                bluePrints.Add(bp);
            }
            var quality = 0;

            foreach (var bluePrint in bluePrints)
            {
                _memoGeode = new Dictionary<int, Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>>();
                Dictionary<string, int> inventory = new Dictionary<string, int>();
                inventory.Add("Ore", 0);
                inventory.Add("Clay", 0);
                inventory.Add("Obsidian", 0);
                inventory.Add("Geode", 0);

                Dictionary<string, int> robots = new Dictionary<string, int>();
                robots.Add("Ore", 1);
                robots.Add("Clay", 0);
                robots.Add("Obsidian", 0);
                robots.Add("Geode", 0);

                Dictionary<string, int> boughtRobots = new Dictionary<string, int>();
                boughtRobots.Add("Ore", 0);
                boughtRobots.Add("Clay", 0);
                boughtRobots.Add("Obsidian", 0);
                boughtRobots.Add("Geode", 0);

                var maxQualityLevel = CalculateMaxQualityLevel(bluePrint, 1, inventory, robots);

                quality += maxQualityLevel * bluePrint.BluePrintNumber;
                Console.WriteLine($"BluePrint: {bluePrint.BluePrintNumber} Quality: {maxQualityLevel}");
                Console.ReadLine();
            }

            Console.WriteLine(quality);
        }

        private static Dictionary<int, Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>> _memoGeode = new Dictionary<int, Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>>();
        private static int CalculateMaxQualityLevel(BluePrint bluePrint, int minutes, Dictionary<string, int> inventory, Dictionary<string, int> robots)
        {

            if (_memoGeode.ContainsKey(minutes))
            {
                var robotsTuple = _memoGeode[minutes].Item2;
                var inventoryTuple = _memoGeode[minutes].Item2;

                if (robotsTuple.Item1 == robots["Ore"] && robotsTuple.Item2 == robots["Clay"] && robotsTuple.Item3 == robots["Obsidian"] && robotsTuple.Item4 == robots["Geode"])
                {
                    if (inventoryTuple.Item1 >= inventory["Ore"] && inventoryTuple.Item2 >= inventory["Clay"] && inventoryTuple.Item3 >= inventory["Obsidian"] && robotsTuple.Item4 >= inventory["Geode"])
                    {
                        return 0;
                    }
                    else
                    {
                        _memoGeode[minutes] = new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(new Tuple<int, int, int, int>(robots["Ore"], robots["Clay"], robots["Obsidian"], robots["Geode"]),
                    new Tuple<int, int, int, int>(inventory["Ore"], inventory["Clay"], inventory["Obsidian"], inventory["Geode"]));
                    }
                }
            }
            else
            {
                _memoGeode.Add(minutes, new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(new Tuple<int, int, int, int>(robots["Ore"], robots["Clay"], robots["Obsidian"], robots["Geode"]),
                    new Tuple<int, int, int, int>(inventory["Ore"], inventory["Clay"], inventory["Obsidian"], inventory["Geode"])));
            }



            if (minutes > 24)
            {
                //Console.WriteLine($"Ore: {inventory["Ore"]} Clay: {inventory["Clay"]} Obsidian: {inventory["Obsidian"]} Geode: {inventory["Geode"]}");
                return inventory["Geode"];
            }

            var maxLevel = inventory["Geode"];
            var maxLevelBoughtGeodeRobot = 0;
            var maxLevelBoughtObsidianRobot = 0;
            var maxLevelBoughtClayRobot = 0;
            var maxLevelBoughtOreRobot = 0;
            var maxLevelDoNothing = 0;
            var bought = false;

            var geodeRobotOreCost = bluePrint.GeodeCost.First(o => o.CostType == "Ore").Amount;
            var geodeRobotObsidianCost = bluePrint.GeodeCost.First(o => o.CostType == "Obsidian").Amount;
            if (inventory["Ore"] >= geodeRobotOreCost && inventory["Obsidian"] >= geodeRobotObsidianCost)
            {
                var newInventory = new Dictionary<string, int>(inventory);
                var newRobots = new Dictionary<string, int>(robots);
                newInventory["Ore"] -= geodeRobotOreCost;
                newInventory["Obsidian"] -= geodeRobotObsidianCost;
                FillInventory(newInventory, robots);
                newRobots["Geode"] += 1;
                return CalculateMaxQualityLevel(bluePrint, minutes + 1, newInventory, newRobots);
            }


            var obsidianRobotOreCost = bluePrint.ObsidianCost.First(o => o.CostType == "Ore").Amount;
            var obsidianRobotClayCost = bluePrint.ObsidianCost.First(o => o.CostType == "Clay").Amount;
            if (inventory["Ore"] >= obsidianRobotOreCost && inventory["Clay"] >= obsidianRobotClayCost && robots["Obsidian"] <= geodeRobotObsidianCost)
            {
                var newInventory = new Dictionary<string, int>(inventory);
                var newRobots = new Dictionary<string, int>(robots);
                newInventory["Ore"] -= obsidianRobotOreCost;
                newInventory["Clay"] -= obsidianRobotClayCost;
                FillInventory(newInventory, robots);
                newRobots["Obsidian"] += 1;
                return CalculateMaxQualityLevel(bluePrint, minutes + 1, newInventory, newRobots);
            }

            var maxOreCost = Math.Max(Math.Max(Math.Max(obsidianRobotOreCost, geodeRobotOreCost), bluePrint.ClayCost.Amount), bluePrint.OreCosts.Amount);
            if (inventory["Ore"] >= bluePrint.OreCosts.Amount && robots["Ore"] <= maxOreCost && inventory["Ore"] < maxOreCost * 2)
            {
                var newInventory = new Dictionary<string, int>(inventory);
                var newRobots = new Dictionary<string, int>(robots);
                newInventory["Ore"] -= bluePrint.OreCosts.Amount;
                FillInventory(newInventory, robots);
                newRobots["Ore"] += 1;
                maxLevelBoughtOreRobot = CalculateMaxQualityLevel(bluePrint, minutes + 1, newInventory, newRobots);
                bought = true;
            }

            if (inventory["Ore"] >= bluePrint.ClayCost.Amount && robots["Clay"] <= obsidianRobotClayCost && inventory["Clay"] < obsidianRobotClayCost * 2)
            {
                var newInventory = new Dictionary<string, int>(inventory);
                var newRobots = new Dictionary<string, int>(robots);
                newInventory["Ore"] -= bluePrint.ClayCost.Amount;
                FillInventory(newInventory, robots);
                newRobots["Clay"] += 1;
                maxLevelBoughtClayRobot = CalculateMaxQualityLevel(bluePrint, minutes + 1, newInventory, newRobots);
                bought = true;
            }

            FillInventory(inventory, robots);
            maxLevelDoNothing = CalculateMaxQualityLevel(bluePrint, minutes + 1, inventory, robots);




            return new List<int>() { maxLevel, maxLevelDoNothing, maxLevelBoughtOreRobot, maxLevelBoughtClayRobot, maxLevelBoughtObsidianRobot, maxLevelBoughtGeodeRobot }.Max();
        }

        private static void FillInventory(Dictionary<string, int> inventory, Dictionary<string, int> robots)
        {
            //Fill Inventory
            inventory["Ore"] += robots["Ore"];
            inventory["Clay"] += robots["Clay"];
            inventory["Obsidian"] += robots["Obsidian"];
            inventory["Geode"] += robots["Geode"];
        }

        public static void Part2()
        {

        }

        public class BluePrint
        {
            public int BluePrintNumber { get; set; }
            public Cost OreCosts { get; set; }
            public Cost ClayCost { get; set; }
            public List<Cost> ObsidianCost { get; set; }
            public List<Cost> GeodeCost { get; set; }
        }

        public class Cost
        {
            public string CostType { get; set; }
            public int Amount { get; set; }
        }


    }


}
