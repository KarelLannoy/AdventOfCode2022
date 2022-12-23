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

        public static string Print(this HashSet<Point> hashSetToPrint)
        {
            StringBuilder grid = new StringBuilder();
            for (int y = hashSetToPrint.Min(k => k.Y); y <= hashSetToPrint.Max(k => k.Y); y++)
            {
                var line = "";
                for (int x = hashSetToPrint.Min(k => k.X); x <= hashSetToPrint.Max(k => k.X); x++)
                {
                    line += hashSetToPrint.Contains(new Point(x, y)) ? "#" : ".";
                }
                grid.AppendLine(line);
            }
            return grid.ToString();
        }

        // Split a string into separate strings, as specified by the delimiter.
        public static string[] SplitToStringArray(this string str, string split, bool removeEmpty)
		{
			return str.Split(new string[] { split }, removeEmpty ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
		}

		// Split a string into separate strings, as specified by the delimiter.
		public static string[] SplitToStringArray(this string str, char[] split, bool removeEmpty)
		{
			return str.Split(split, removeEmpty ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
		}

		// Split a string into an int array.
		public static int[] SplitToIntArray(this string str, string split)
		{
			return Array.ConvertAll(str.SplitToStringArray(split, true), s => int.Parse(s));
		}

		// Split a string into an int array.
		public static int[] SplitToIntArray(this string str, params char[] split)
		{
			return Array.ConvertAll(str.SplitToStringArray(split, true), s => int.Parse(s));
		}

		// Split a string into a long array.
		public static long[] SplitToLongArray(this string str, string split)
		{
			return Array.ConvertAll(str.SplitToStringArray(split, true), s => long.Parse(s));
		}

		// Split a string into a long array.
		public static long[] SplitToLongArray(this string str, params char[] split)
		{
			return Array.ConvertAll(str.SplitToStringArray(split, true), s => long.Parse(s));
		}

		public static V Read<K, V>(this Dictionary<K, V> dict, K key)
		{
			if (dict.ContainsKey(key)) return dict[key];
			return default(V);
		}

		public static V Read<K, V>(this Dictionary<K, V> dict, K key, V def)
		{
			if (dict.ContainsKey(key)) return dict[key];
			return def;
		}
	}
}
