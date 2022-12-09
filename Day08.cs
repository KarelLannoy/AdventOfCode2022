using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day08
    {
        public static void Part1()
        {
            var matrix = File.ReadAllLines(@"Inputs/Input08.txt").ToList();
            var visibleTreeCounter = 0;
            for (int y = 0; y < matrix.Count; y++)
            {
                for (int x = 0; x < matrix[y].Length; x++)
                {
                    if (IsTreeVisible(int.Parse(matrix[y][x].ToString()), x, y, matrix))
                    {
                        visibleTreeCounter++;
                    }
                }
            }
            Console.WriteLine(visibleTreeCounter.ToString());
        }

        private static bool IsTreeVisible(int tree, int posx, int posy, List<string> matrix)
        {
            //Edge always visible
            if (posx == 0 || posy == 0 || posx == matrix[posy].Length - 1 || posy == matrix.Count - 1)
            {
                return true;
            }
            //LookLeft
            var left = true;
            for (int i = posx - 1; i >= 0; i--)
            {
                if (int.Parse(matrix[posy][i].ToString()) >= tree)
                {
                    left = false;
                    break;
                }
            }
            //LookRight
            var right = true;
            for (int i = posx + 1; i < matrix[posy].Length; i++)
            {
                if (int.Parse(matrix[posy][i].ToString()) >= tree)
                {
                    right = false;
                    break;
                }
            }
            //LookUp
            var up = true;
            for (int i = posy - 1; i >= 0; i--)
            {
                if (int.Parse(matrix[i][posx].ToString()) >= tree)
                {
                    up = false;
                    break;
                }
            }
            //LookDown
            var down = true;
            for (int i = posy + 1; i < matrix.Count; i++)
            {
                if (int.Parse(matrix[i][posx].ToString()) >= tree)
                {
                    down = false;
                    break;
                }
            }
            return left || right || up || down;
        }

        public static void Part2()
        {
            var matrix = File.ReadAllLines(@"Inputs/Input08.txt").ToList();
            var highestScenicScore = 0;
            for (int y = 0; y < matrix.Count; y++)
            {
                for (int x = 0; x < matrix[y].Length; x++)
                {
                    var scenicScore = CalculateScenicScore(int.Parse(matrix[y][x].ToString()), x, y, matrix);
                    if (scenicScore > highestScenicScore)
                        highestScenicScore = scenicScore;
                }
            }
            Console.WriteLine(highestScenicScore.ToString());
        }

        private static int CalculateScenicScore(int tree, int posx, int posy, List<string> matrix)
        {
            //Disregard edge
            if (posx==0 || posy == 0 || posx == matrix[posy].Length - 1 || posy == matrix.Count - 1)
            {
                return 0;
            }
            //LookLeft
            int left = 0;
            for (int i = posx - 1; i >= 0; i--)
            {
                left++;
                if (int.Parse(matrix[posy][i].ToString()) >= tree)
                {
                    break;
                }
            }
            //LookRight
            int right = 0;
            for (int i = posx + 1; i < matrix[posy].Length; i++)
            {
                right++;
                if (int.Parse(matrix[posy][i].ToString()) >= tree)
                {
                    break;
                }
            }
            //LookUp
            int up = 0;
            for (int i = posy - 1; i >= 0; i--)
            {
                up++;
                if (int.Parse(matrix[i][posx].ToString()) >= tree)
                {
                    break;
                }
            }
            //LookDown
            int down = 0;
            for (int i = posy + 1; i < matrix.Count; i++)
            {
                down++;
                if (int.Parse(matrix[i][posx].ToString()) >= tree)
                {
                    break;
                }
            }
            return left * right * up * down;
        }
    }
}
