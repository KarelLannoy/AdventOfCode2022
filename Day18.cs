using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day18
    {
        public static void Part1()
        {
            var droplets = File.ReadAllLines(@"Inputs/Input18.txt").Select(d => new Point3D(int.Parse(d.Split(",")[0]), int.Parse(d.Split(",")[1]), int.Parse(d.Split(",")[2]))).ToHashSet();
            var freeSides = 0;
            foreach (var droplet in droplets)
            {
                var sides = GetSides(droplet);
                freeSides += (6 - sides.Count(d => droplets.Contains(d)));
            }
            Console.WriteLine(freeSides);
        }

        private static List<Point3D> GetSides(Point3D droplet)
        {
            List<Point3D> sides = new List<Point3D>() { new Point3D(1, 0, 0), new Point3D(-1, 0, 0), new Point3D(0, 1, 0), new Point3D(0, -1, 0), new Point3D(0, 0, 1), new Point3D(0, 0, -1) };
            List<Point3D> pointSides = new List<Point3D>();
            foreach (var side in sides)
            {
                pointSides.Add(new Point3D(droplet.X + side.X, droplet.Y + side.Y, droplet.Z + side.Z));
            }
            return pointSides;
        }

        public static void Part2()
        {
            var droplets = File.ReadAllLines(@"Inputs/Input18.txt").Select(d => new Point3D(int.Parse(d.Split(",")[0]), int.Parse(d.Split(",")[1]), int.Parse(d.Split(",")[2]))).ToHashSet();
            var outsideDroplets = new List<Point3D>();
            var maxX = droplets.Max(d => d.X);
            var maxY = droplets.Max(d => d.Y);
            var maxZ = droplets.Max(d => d.Z);
            var minX = droplets.Min(d => d.X);
            var minY = droplets.Min(d => d.Y);
            var minZ = droplets.Min(d => d.Z);
            var freeSides = 0;
            foreach (var droplet in droplets)
            {
                var sides = GetSides(droplet);
                foreach (var side in sides)
                {
                    
                    if (AreSidesFree(side, droplets, maxX, minX, maxY, minY, maxZ, minZ, new HashSet<Point3D>()))
                    {
                        freeSides++;
                    }
                }
            }

            Console.WriteLine(freeSides);
        }

        
        private static bool AreSidesFree(Point3D droplet, HashSet<Point3D> droplets, int maxX, int minX, int maxY, int minY, int MaxZ, int minZ, HashSet<Point3D> visited)
        {
            if (droplets.Contains(droplet))
            {
                return false;
            }
            visited.Add(droplet);
            List<Point3D> sides = new List<Point3D>() { new Point3D(1, 0, 0), new Point3D(-1, 0, 0), new Point3D(0, 1, 0), new Point3D(0, -1, 0), new Point3D(0, 0, 1), new Point3D(0, 0, -1) };
            foreach (var side in sides)
            {
                var pointSide = new Point3D(droplet.X + side.X, droplet.Y + side.Y, droplet.Z + side.Z);
                if (visited.Contains(pointSide))
                {
                    continue;
                }
                if (droplets.Contains(pointSide))
                {
                    continue;
                }
                if (pointSide.X > maxX || pointSide.Y > maxY || pointSide.Z > MaxZ || pointSide.X < minX || pointSide.Y < minY || pointSide.Z < minZ)
                {
                    return true;
                }
                if (droplets.Contains(pointSide))
                {
                    continue;
                }

                if (AreSidesFree(pointSide, droplets, maxX, minX, maxY, minY, MaxZ, minZ, visited))
                    return true;

            }
            return false;
        }
    }


}
