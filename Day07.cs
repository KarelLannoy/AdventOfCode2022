using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day07
    {

        public static void Part1()
        {
            List<Directory> directoryList = new List<Directory>();
            Directory root = new Directory();
            root.Name = "/";
            directoryList.Add(root);
            Directory currentDir = null;
            var input = File.ReadAllLines(@"Inputs/Input07.txt");
            foreach (var command in input)
            {
                if (command.StartsWith("$ cd"))
                {
                    var dirName = command.Substring(5);
                    if (dirName == "/")
                    {
                        currentDir = root;
                        continue;
                    }
                    if (dirName == "..")
                    {
                        currentDir = currentDir.Parent;
                        continue;
                    }
                    if (currentDir.Directories.Any(d => d.Name == dirName))
                        currentDir = currentDir.Directories.First(d => d.Name == dirName);
                    else
                    {
                        Directory newDir = new Directory();
                        newDir.Name = dirName;
                        newDir.Parent = currentDir;
                        currentDir.Directories.Add(newDir);
                        currentDir = newDir;
                        directoryList.Add(newDir);
                    }
                    continue;
                }
                if (command.StartsWith("$ ls"))
                    continue;
                if (command.StartsWith("dir"))
                {
                    var dirName = command.Split(" ")[1];
                    if (!currentDir.Directories.Any(d => d.Name == dirName))
                    {
                        Directory newDir = new Directory();
                        newDir.Name = dirName;
                        newDir.Parent = currentDir;
                        currentDir.Directories.Add(newDir);
                        directoryList.Add(newDir);
                    }
                    continue;
                }
                var fileParts = command.Split(" ");
                currentDir.Files.Add(new Tuple<string, long>(fileParts[1], long.Parse(fileParts[0])));
            }

            var totalSize = CalculateSize(root);
            Console.WriteLine(directoryList.Where(d => d.Size <= 100000).Sum(d => d.Size));

        }

        private static long CalculateSize(Directory dir)
        {
            long size = dir.Files.Sum(t => t.Item2);
            foreach (var child in dir.Directories)
            {
                size+= CalculateSize(child);
            }
            dir.Size = size;
            return size;
        }

        public static void Part2()
        {
            List<Directory> directoryList = new List<Directory>();
            Directory root = new Directory();
            root.Name = "/";
            directoryList.Add(root);
            Directory currentDir = null;
            var input = File.ReadAllLines(@"Inputs/Input07.txt");
            foreach (var command in input)
            {
                if (command.StartsWith("$ cd"))
                {
                    var dirName = command.Substring(5);
                    if (dirName == "/")
                    {
                        currentDir = root;
                        continue;
                    }
                    if (dirName == "..")
                    {
                        currentDir = currentDir.Parent;
                        continue;
                    }
                    if (currentDir.Directories.Any(d => d.Name == dirName))
                        currentDir = currentDir.Directories.First(d => d.Name == dirName);
                    else
                    {
                        Directory newDir = new Directory();
                        newDir.Name = dirName;
                        newDir.Parent = currentDir;
                        currentDir.Directories.Add(newDir);
                        currentDir = newDir;
                        directoryList.Add(newDir);
                    }
                    continue;
                }
                if (command.StartsWith("$ ls"))
                    continue;
                if (command.StartsWith("dir"))
                {
                    var dirName = command.Split(" ")[1];
                    if (!currentDir.Directories.Any(d => d.Name == dirName))
                    {
                        Directory newDir = new Directory();
                        newDir.Name = dirName;
                        newDir.Parent = currentDir;
                        currentDir.Directories.Add(newDir);
                        directoryList.Add(newDir);
                    }
                    continue;
                }
                var fileParts = command.Split(" ");
                currentDir.Files.Add(new Tuple<string, long>(fileParts[1], long.Parse(fileParts[0])));
            }

            var totalSize = CalculateSize(root);

            var freeSpace = 70000000 - root.Size;
            var neededSpaceToClear = 30000000 - freeSpace;

            Console.WriteLine(directoryList.Where(d => d.Size > neededSpaceToClear).Min(d => d.Size));


        }

        private class Directory
        {
            public string Name { get; set; }
            public List<Tuple<string, long>> Files { get; set; }
            public List<Directory> Directories { get; set; }
            public Directory Parent { get; set; }
            public long Size { get; set; }

            public Directory()
            {
                Files = new List<Tuple<string, long>>();
                Directories = new List<Directory>();
            }
        }
    }


}
