using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Day22
    {
        private static Dictionary<Point, bool> _board = new Dictionary<Point, bool>();
        private static string _instructions = "";

        public static void Part1()
        {
            var boardString = File.ReadAllText(@"Inputs/Input22.txt").Split(Environment.NewLine + Environment.NewLine)[0];
            _instructions = File.ReadAllText(@"Inputs/Input22.txt").Split(Environment.NewLine + Environment.NewLine)[1];
            int y = 0;
            foreach (var line in boardString.Split(Environment.NewLine))
            {
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case '#':
                            _board.Add(new Point(x, y), true);
                            break;
                        case '.':
                            _board.Add(new Point(x, y), false);
                            break;
                        default:
                            break;
                    }
                }
                y++;
            }

            var start = _board.Where(b => !b.Value && b.Key.Y == 0).OrderBy(b => b.Key.X).First().Key;
            int orientation = 0;

            string numberValue = "";
            for (int i = 0; i < _instructions.Length; i++)
            {
                if (char.IsNumber(_instructions[i]))
                {
                    numberValue += _instructions[i];
                }
                else
                {
                    start = MakeMove(start, int.Parse(numberValue), orientation);
                    switch (_instructions[i])
                    {
                        case 'R':
                            orientation += 90;
                            break;
                        case 'L':
                            orientation -= 90;
                            break;
                    }
                    numberValue = "";
                    orientation = ((360 * 100) + orientation) % 360;
                }
            }
            start = MakeMove(start, int.Parse(numberValue), orientation);

            Console.WriteLine((1000 * (start.Y + 1)) + (4 * (start.X + 1)) + (orientation / 90));
        }

        private static Point MakeMove(Point start, int numberOfSteps, int orientation)
        {
            var previousPoint = new Point(start.X, start.Y);
            for (int i = 0; i < numberOfSteps; i++)
            {
                var newPoint = new Point(previousPoint.X, previousPoint.Y);
                switch (orientation)
                {
                    case 0:
                        newPoint = new Point(previousPoint.X + 1, previousPoint.Y);
                        if (!_board.ContainsKey(newPoint))
                        {
                            newPoint = new Point(_board.Where(b => b.Key.Y == start.Y).Min(b => b.Key.X), start.Y);
                        }
                        break;
                    case 90:
                        newPoint = new Point(previousPoint.X, previousPoint.Y + 1);
                        if (!_board.ContainsKey(newPoint))
                        {
                            newPoint = new Point(start.X, _board.Where(b => b.Key.X == start.X).Min(b => b.Key.Y));
                        }
                        break;
                    case 180:
                        newPoint = new Point(previousPoint.X - 1, previousPoint.Y);
                        if (!_board.ContainsKey(newPoint))
                        {
                            newPoint = new Point(_board.Where(b => b.Key.Y == start.Y).Max(b => b.Key.X), start.Y);
                        }
                        break;
                    case 270:
                        newPoint = new Point(previousPoint.X, previousPoint.Y - 1);
                        if (!_board.ContainsKey(newPoint))
                        {
                            newPoint = new Point(start.X, _board.Where(b => b.Key.X == start.X).Max(b => b.Key.Y));
                        }
                        break;
                }
                if (_board[newPoint])
                {
                    return previousPoint;
                }
                previousPoint = newPoint;
            }
            return previousPoint;
        }


        private static Tuple<Point, int> MakeMoveOnCube(Point start, int numberOfSteps, int orientation)
        {
            var previousPoint = new Point(start.X, start.Y);
            var previousOrientation = orientation;

            for (int i = 0; i < numberOfSteps; i++)
            {
                var newPoint = new Point(previousPoint.X, previousPoint.Y);
                switch (orientation)
                {
                    case 0:
                        newPoint = new Point(previousPoint.X + 1, previousPoint.Y);
                        if (!_board.ContainsKey(newPoint))
                        {
                            var quadrant = GetQuadrant(previousPoint);
                            switch (quadrant)
                            {
                                case 2:
                                    //Go To 5
                                    orientation = 180;
                                    newPoint = new Point(99, 149 - previousPoint.Y);
                                    break;
                                case 3:
                                    //Go To 2
                                    orientation = 270;
                                    newPoint = new Point(previousPoint.Y + 50 ,  49);
                                    break;
                                case 5:
                                    //Go To 2
                                    orientation = 180;
                                    newPoint = new Point(149, 149 - previousPoint.Y);
                                    break;
                                case 6:
                                    //Go To 5
                                    orientation = 270;
                                    newPoint = new Point(previousPoint.Y - 100, 149);
                                    break;
                            }
                        }
                        break;
                    case 90:
                        newPoint = new Point(previousPoint.X, previousPoint.Y + 1);
                        if (!_board.ContainsKey(newPoint))
                        {
                            var quadrant = GetQuadrant(previousPoint);
                            switch (quadrant)
                            {
                                case 2:
                                    //Go To 3
                                    orientation = 180;
                                    newPoint = new Point(99, previousPoint.X - 50);
                                    break;
                                case 5:
                                    //Go To 6
                                    orientation = 180;
                                    newPoint = new Point(49, previousPoint.X + 100);
                                    break;
                                case 6:
                                    //Go To 2
                                    newPoint = new Point(previousPoint.X + 100, 0);
                                    break;
                            }
                        }
                        break;
                    case 180:
                        newPoint = new Point(previousPoint.X - 1, previousPoint.Y);
                        if (!_board.ContainsKey(newPoint))
                        {
                            var quadrant = GetQuadrant(previousPoint);
                            switch (quadrant)
                            {
                                case 1:
                                    //Go To 4
                                    orientation = 0;
                                    newPoint = new Point(0, 149 - previousPoint.Y);
                                    break;
                                case 3:
                                    //Go To 4
                                    orientation = 90;
                                    newPoint = new Point(previousPoint.Y - 50, 100);
                                    break;
                                case 4:
                                    //Go To 1
                                    orientation = 0;
                                    newPoint = new Point(50, 149 - previousPoint.Y);
                                    break;
                                case 6:
                                    //Go To 1
                                    orientation = 90;
                                    newPoint = new Point(previousPoint.Y - 100, 0);
                                    break;
                            }
                        }
                        break;
                    case 270:
                        newPoint = new Point(previousPoint.X, previousPoint.Y - 1);
                        if (!_board.ContainsKey(newPoint))
                        {
                            var quadrant = GetQuadrant(previousPoint);
                            switch (quadrant)
                            {
                                case 1:
                                    //Go To 6
                                    orientation = 0;
                                    newPoint = new Point(0, previousPoint.X + 100);
                                    break;
                                case 2:
                                    //Go To 4
                                    newPoint = new Point(previousPoint.X - 100, 199);
                                    break;
                                case 4:
                                    //Go To 1
                                    orientation = 0;
                                    newPoint = new Point(50,previousPoint.X + 50);
                                    break;

                            }
                        }
                        break;
                }
                if (_board[newPoint])
                {
                    return new Tuple<Point, int>(previousPoint, previousOrientation);
                }
                previousPoint = newPoint;
                previousOrientation = orientation;
            }
            return new Tuple<Point, int>(previousPoint, previousOrientation);
        }

        private static int GetQuadrant(Point point)
        {
            if (point.X >= 50 && point.X < 100)
            {
                if (point.Y < 50)
                {
                    return 1;
                }
                if (point.Y < 100)
                {
                    return 3;
                }
                if (point.Y < 150)
                {
                    return 5;
                }
            }
            if (point.X < 50)
            {
                if (point.Y < 150)
                {
                    return 4;
                }
                return 6;
            }
            return 2;
        }

        public static void Part2()
        {
            var start = _board.Where(b => !b.Value && b.Key.Y == 0).OrderBy(b => b.Key.X).First().Key;
            int orientation = 0;

            var maxX = _board.Max(b => b.Key.X) + 1;
            var maxY = _board.Max(b => b.Key.Y) + 1;
            var boardSizeX = maxX / 3;
            var boardSizeY = maxY / 4;



            string numberValue = "";
            for (int i = 0; i < _instructions.Length; i++)
            {
                if (char.IsNumber(_instructions[i]))
                {
                    numberValue += _instructions[i];
                }
                else
                {
                    var result1 = MakeMoveOnCube(start, int.Parse(numberValue), orientation);
                    start = result1.Item1;
                    orientation = result1.Item2;
                    switch (_instructions[i])
                    {
                        case 'R':
                            orientation += 90;
                            break;
                        case 'L':
                            orientation -= 90;
                            break;
                    }
                    numberValue = "";
                    orientation = ((360 * 100) + orientation) % 360;
                }
            }
            var result = MakeMoveOnCube(start, int.Parse(numberValue), orientation);
            start = result.Item1;
            orientation = result.Item2;

            Console.WriteLine((1000 * (start.Y + 1)) + (4 * (start.X + 1)) + (orientation / 90));
        }
    }
}
