﻿// See https://aka.ms/new-console-template for more information
using AdventOfCode2022;
using System.Diagnostics;

Stopwatch sw = new Stopwatch();
sw.Start();

Day01.Part1();

sw.Stop();
Console.WriteLine($"Timing: {sw.Elapsed}");
sw.Reset();
sw.Start();

Day01.Part2();

sw.Stop();
Console.WriteLine($"Timing: {sw.Elapsed}");