using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day13
    {
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"Inputs/Input13.txt").ToList();
            var indexList = new List<int>();
            var index = 1;
            for (int i = 0; i < lines.Count; i += 3)
            {
                var left = lines[i];
                var right = lines[i + 1];
                var orderCorrect = Compare(Listify(left), Listify(right));
                if (!orderCorrect.HasValue || orderCorrect.Value)
                    indexList.Add(index);
                index++;
            }

            Console.WriteLine(indexList.Sum());
        }

        private static bool? Compare(List<string> leftList, List<string> rightList)
        {
            for (int i = 0; i < leftList.Count; i++)
            {
                var leftValue = leftList[i];
                if (rightList.Count() - 1 < i)
                    return false;
                var rightValue = rightList[i];
                if (!leftValue.StartsWith('[') && !rightValue.StartsWith('['))
                {
                    if (int.Parse(leftValue) != int.Parse(rightValue))
                    {
                        return (int.Parse(leftValue) < int.Parse(rightValue));
                    }
                }
                if (leftValue.StartsWith('[') || rightValue.StartsWith('['))
                {
                    var valid = Compare(Listify(leftValue), Listify(rightValue));
                    if (valid.HasValue)
                        return valid;
                }
            }

            if (leftList.Count() < rightList.Count())
                return true;
            else
                return null;
            
        }

        private static List<string> Listify(string value)
        {
            if (!value.StartsWith("["))
                return new List<string> { value };
            if (value == "[]")
                return new List<string>();

            List<int> indexesOfReplacement = new List<int>();
            var level = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '[')
                    level++;
                if (value[i] == ',' && level > 1)
                {
                    indexesOfReplacement.Add(i);
                }
                if (value[i] == ']')
                    level--;
            }
            foreach (var index in indexesOfReplacement)
            {
                var stringArray = value.ToCharArray();
                stringArray[index] = ';';
                value = new string(stringArray);
            }
            value = value.Substring(1, value.Length - 2);
            return value.Split(",").Select(value => value.Replace(";", ",")).ToList();
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"Inputs/Input13.txt").Where(l => l != "").ToList();
            var decoderKey1 = "[[2]]";
            var decoderKey2 = "[[6]]";
            lines.Add(decoderKey1);
            lines.Add(decoderKey2);
            lines.Sort(CompareEntries);
            Console.WriteLine((lines.IndexOf(decoderKey1) + 1) * (lines.IndexOf(decoderKey2) + 1));
        }

        public static int CompareEntries(string left, string right)
        {
            var orderCorrect = Compare(Listify(left), Listify(right));
            if (orderCorrect.HasValue)
            {
                if (orderCorrect.Value)
                    return -1;
                else
                    return 1;
            }
            return 0;
        }
    }
}
