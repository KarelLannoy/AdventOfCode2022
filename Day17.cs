using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day17
    {
        public static void Part1()
        {
            var jetPattern = File.ReadAllText(@"Inputs/Input17.txt").ToList();
            var horizontalLine = new List<Point> { new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0) };
            var plus = new List<Point> { new Point(2, 1), new Point(3, 0), new Point(3, 1), new Point(3, 2), new Point(4, 1) };
            var backwardL = new List<Point> { new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(4, 1), new Point(4, 2) };
            var verticalLine = new List<Point> { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) };
            var cube = new List<Point> { new Point(2, 0), new Point(3, 0), new Point(2, 1), new Point(3, 1) };
            var shapeList = new List<List<Point>> { horizontalLine, plus, backwardL, verticalLine, cube };
            var leftBound = 0;
            var rightBound = 6;


            HashSet<Point> tower = new HashSet<Point>();
            for (int i = 0; i < 7; i++)
            {
                tower.Add(new Point(i, -1));
            }

            var rockCount = 0;
            var index = 0;
            var patternSize = jetPattern.Count;
            List<Point> fallingRock = null;
            while (rockCount < 2022)
            {
                if (fallingRock == null)
                {
                    var highestRockPoint = tower.Max(t => t.Y);
                    var rockIndex = rockCount % shapeList.Count;
                    fallingRock = new List<Point>();
                    for (int j = 0; j < shapeList[rockIndex].Count; j++)
                    {
                        fallingRock.Add(new Point(shapeList[rockIndex][j].X, shapeList[rockIndex][j].Y + highestRockPoint + 4));
                    }
                }

                var rockHistory = new List<Point>(fallingRock);

                var patternIndex = index % patternSize;
                for (int i = 0; i < fallingRock.Count; i++)
                {
                    switch (jetPattern[patternIndex])
                    {
                        case '>':
                            if (rockHistory.Max(f => f.X) < rightBound)
                            {
                                fallingRock[i] = new Point(fallingRock[i].X + 1, fallingRock[i].Y);
                            }
                            break;
                        case '<':
                            if (rockHistory.Min(f => f.X) > leftBound)
                            {

                                fallingRock[i] = new Point(fallingRock[i].X - 1, fallingRock[i].Y);
                            }
                            break;
                    }

                }

                index++;

                if (fallingRock.Any(r => tower.Contains(r)))
                {
                    fallingRock = rockHistory;
                }
                rockHistory = new List<Point>(fallingRock);

                for (int i = 0; i < fallingRock.Count; i++)
                {
                    fallingRock[i] = new Point(fallingRock[i].X, fallingRock[i].Y - 1);
                }

                if (fallingRock.Any(r => tower.Contains(r)))
                {
                    rockHistory.ForEach(p => tower.Add(p));
                    rockCount++;
                    fallingRock = null;
                    continue;
                }
            }

            Console.WriteLine(tower.Max(p => p.Y) + 1);

        }

        public static void Part2()
        {
            var jetPattern = File.ReadAllText(@"Inputs/Input17.txt").ToList();
            var horizontalLine = new List<Point> { new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0) };
            var plus = new List<Point> { new Point(2, 1), new Point(3, 0), new Point(3, 1), new Point(3, 2), new Point(4, 1) };
            var backwardL = new List<Point> { new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(4, 1), new Point(4, 2) };
            var verticalLine = new List<Point> { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) };
            var cube = new List<Point> { new Point(2, 0), new Point(3, 0), new Point(2, 1), new Point(3, 1) };
            var shapeList = new List<List<Point>> { horizontalLine, plus, backwardL, verticalLine, cube };
            var leftBound = 0;
            var rightBound = 6;


            HashSet<Point> tower = new HashSet<Point>();
            for (int i = 0; i < 7; i++)
            {
                tower.Add(new Point(i, -1));
            }

            HashSet<string> duplicate = new HashSet<string>();
            long rockCount = 0;
            var index = 0;
            var patternSize = jetPattern.Count;
            List<Point> fallingRock = null;
            var startLoop = "";
            while (rockCount < 1000000000000)
            {
                if (fallingRock == null)
                {
                    var highestRockPoint = tower.Max(t => t.Y);
                    var rockIndex = rockCount % shapeList.Count;
                    fallingRock = new List<Point>();
                    for (int j = 0; j < shapeList[(int)rockIndex].Count; j++)
                    {
                        fallingRock.Add(new Point(shapeList[(int)rockIndex][j].X, shapeList[(int)rockIndex][j].Y + highestRockPoint + 4));
                    }
                }

                var rockHistory = new List<Point>(fallingRock);

                var patternIndex = index % patternSize;
                for (int i = 0; i < fallingRock.Count; i++)
                {
                    switch (jetPattern[patternIndex])
                    {
                        case '>':
                            if (rockHistory.Max(f => f.X) < rightBound)
                            {
                                fallingRock[i] = new Point(fallingRock[i].X + 1, fallingRock[i].Y);
                            }
                            break;
                        case '<':
                            if (rockHistory.Min(f => f.X) > leftBound)
                            {

                                fallingRock[i] = new Point(fallingRock[i].X - 1, fallingRock[i].Y);
                            }
                            break;
                    }

                }

                index++;

                if (fallingRock.Any(r => tower.Contains(r)))
                {
                    fallingRock = rockHistory;
                }
                rockHistory = new List<Point>(fallingRock);

                for (int i = 0; i < fallingRock.Count; i++)
                {
                    fallingRock[i] = new Point(fallingRock[i].X, fallingRock[i].Y - 1);
                }

                if (fallingRock.Any(r => tower.Contains(r)))
                {
                    rockHistory.ForEach(p => tower.Add(p));
                    rockCount++;
                    fallingRock = null;
                    var highestRockPoint = tower.Max(t => t.Y);
                    for (int y = highestRockPoint - 4; y <= highestRockPoint; y++)
                    {
                        if (tower.Contains(new Point(0, y)) && tower.Contains(new Point(1, y)) && tower.Contains(new Point(2, y)) && tower.Contains(new Point(3, y)) && tower.Contains(new Point(4, y)) && tower.Contains(new Point(5, y)) && tower.Contains(new Point(6, y)))
                        {
                            //full line
                            //Console.WriteLine($"Full Line: {y} -> shape: {(rockCount - 1) % shapeList.Count}");
                            tower.RemoveWhere(r => r.Y < y);
                            StringBuilder sb = new StringBuilder();
                            sb.Append((rockCount - 1) % shapeList.Count);
                            for (int i = y; i <= highestRockPoint; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    if (tower.Contains(new Point(j, i)))
                                    {
                                        sb.Append($"({j},{i - y})");
                                    }
                                }
                            }
                            if (duplicate.Contains(sb.ToString()))
                            {
                                Console.WriteLine(duplicate.ToList().IndexOf(sb.ToString()));
                                Console.WriteLine($"highestRockPoint: {highestRockPoint} - RockCount: {rockCount}");
                            }
                            else
                            {
                                duplicate.Add(sb.ToString());
                            }
                        }
                    }

                }

            }
        }

    }
}
