using System;
using System.Collections.Generic;
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
    }
}
