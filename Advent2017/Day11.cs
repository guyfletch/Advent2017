using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day11
    {
        public string answer = "";
        public Day11(string input, bool part2)
        {
            answer = FindChild(input, part2);
        }
        string FindChild(string input, bool part2)
        {
            var locations = new List<List<int>>();
            var inputs = input.Split(',');
            var l = new Location();
            foreach(var i in inputs)
            {
                switch (i)
                {
                    case "sw":
                        l.NE -= 1;
                        break;
                    case "se":
                        l.SE += 1;
                        break;
                    case "n":
                        l.N += 1;
                        break;
                    case "ne":
                        l.NE += 1;
                        break;
                    case "nw":
                        l.SE -= 1;
                        break;
                    case "s":
                        l.N -= 1;
                        break;
                }
                locations.Add(new List<int> { l.N, l.SE, l.NE });
            }
            if (!part2)
            {
                return l.N + "," + l.SE + "," + l.NE;
            }
            else
            {
                return PartTwo(input);
            }
        }


        private static Point HexMove(string d, Point location)
        {
            switch (d)
            {
                case "n":
                    return new Point(location.X, location.Y + 2);
                case "s":
                    return new Point(location.X, location.Y - 2);
                case "ne":
                    return new Point(location.X + 1, location.Y + 1);
                case "nw":
                    return new Point(location.X - 1, location.Y + 1);
                case "se":
                    return new Point(location.X + 1, location.Y - 1);
                case "sw":
                    return new Point(location.X - 1, location.Y - 1);
                default:
                    throw new Exception();
            }
        }

        private static int GetDistance(Point origin, Point position)
        {
            var xMoves = Math.Abs(position.X - origin.X);
            var yMoves = (Math.Abs(position.Y - origin.Y) - xMoves) / 2;

            return xMoves + yMoves;
        }

        public static string PartTwo(string input)
        {
            var directions = input.Split(',');
            var origin = new Point(0, 0);
            var position = origin;
            var maxDistance = 0;

            foreach (var d in directions)
            {
                position = HexMove(d, position);
                maxDistance = Math.Max(maxDistance, GetDistance(origin, position));
            }

            return maxDistance.ToString();
        }
    }
    class Location
    {
        public Location()
        {
            N = 0;
            NE = 0;
            SE = 0;
        }
        public int NE { get; set; }
        public int SE { get; set; }
        public int N { get; set; }
    }
}
