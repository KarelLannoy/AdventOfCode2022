using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Extensions
    {
        public static List<T> PopRange<T>(this Stack<T> stack, int amount)
        {
            var result = new List<T>(amount);
            while (amount-- > 0 && stack.Count > 0)
            {
                result.Add(stack.Pop());
            }
            result.Reverse();
            return result;
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static string Print<T>(this IDictionary<Point, T> dictionaryToPrint)
        {
            StringBuilder grid = new StringBuilder();
            for (int y = dictionaryToPrint.Keys.Min(k=>k.Y); y <= dictionaryToPrint.Keys.Max(k => k.Y); y++)
            {
                var line = "";
                for (int x = dictionaryToPrint.Keys.Min(k => k.X); x <= dictionaryToPrint.Keys.Max(k => k.X); x++)
                {
                    line += dictionaryToPrint.ContainsKey(new Point(x, y)) ? dictionaryToPrint[new Point(x, y)].ToString() : ".";
                }
                grid.AppendLine(line);
            }
            return grid.ToString();
        }
    }
}
