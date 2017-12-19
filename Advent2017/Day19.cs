using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day19
    {
        public string answer = "";
        public Day19(string input, bool part2)
        {
            answer = PathFinder(input, part2);
        }
        string PathFinder(string input, bool part2)
        {
            var inputs = input.Split('\n').Select(x=>x.Replace('\r',' ')).ToArray();
            var currentPosition = new Position(inputs[0].IndexOf('|'), 0, Direction.Down, '|');
            var xPos = 0;
            var yPos = 0;
            var chars = new List<char>();
            bool end = false;
            var visited = new List<Position>();
            var dir = Direction.Down;
            xPos = inputs[0].IndexOf('|');
            while (!end)
            {
                var next = new Position();
                var nextCh = ' ';
                nextCh = FindNext(inputs, xPos, yPos, dir);
                if (nextCh == ' ')
                {
                    dir = ChangeDirection(inputs, currentPosition);
                    if (dir == Direction.End)
                    {
                        end = true;
                        visited.Add(currentPosition);
                        break;
                    }
                    nextCh = FindNext(inputs, xPos, yPos, dir);
                }
                if (nextCh != '|' && nextCh != '-' && nextCh != '+')
                {
                    chars.Add(nextCh);
                }
                switch (dir)
                {
                    case Direction.Down:
                        yPos += 1;
                        break;
                    case Direction.Right:
                        xPos += 1;
                        break;
                    case Direction.Up:
                        yPos -= 1;
                        break;
                    case Direction.Left:
                        xPos -= 1;
                        break;
                }
                next.xPos = xPos;
                next.yPos = yPos;
                next.Direction = dir;
                next.Char = nextCh;
                visited.Add(new Position(currentPosition.xPos, currentPosition.yPos, currentPosition.Direction, currentPosition.Char));
                currentPosition = next;
            }
            if (!part2)
            {
                var sb = new StringBuilder();
                foreach(var c in chars)
                {
                    sb.Append(c);
                }
                return sb.ToString();
            }
            else
            {
                return visited.Count.ToString();
            }
        }

        private static char FindNext(string[] inputs, int xPos, int yPos, Direction dir)
        {
            char nextCh = ' ';
            switch (dir)
            {
                case Direction.Down:
                    nextCh = inputs[yPos + 1][xPos];
                    break;
                case Direction.Right:
                    nextCh = inputs[yPos][xPos + 1];
                    break;
                case Direction.Up:
                    nextCh = inputs[yPos - 1][xPos];
                    break;
                case Direction.Left:
                    nextCh = inputs[yPos][xPos - 1];
                    break;
            }

            return nextCh;
        }

        public Direction ChangeDirection(string[] arr, Position current)
        {
            var dirs = new List<Direction>() { Direction.Down, Direction.Right, Direction.Up, Direction.Left };
            if(current.Char == '+')
            {
                var nextCh = ' ';
                var opposite = Direction.Down;
                switch (current.Direction)
                {
                    case Direction.Down:
                        opposite = Direction.Up;
                        break;
                    case Direction.Right:
                        opposite = Direction.Left;
                        break;
                    case Direction.Up:
                        opposite = Direction.Down;
                        break;
                    case Direction.Left:
                        opposite = Direction.Right;
                        break;
                }
                dirs.Remove(opposite);
                foreach (var d in dirs)
                {
                    nextCh = FindNext(arr, current.xPos, current.yPos, d);
                    if(nextCh != ' ')
                    {
                        return d;
                    }
                }
            }
            return Direction.End;
        }

        public enum Direction
        {
            Down,
            Right,
            Up,
            Left,
            End
        };
    }
    class Position
    {
        public Position()
        {

        }
        public Position(int x, int y, Day19.Direction dir, char c)
        {
            xPos = x;
            yPos = y;
            Direction = dir;
            Char = c;
        }
        public int xPos { get; set; }
        public int yPos { get; set; }
        public Day19.Direction Direction { get; set; }
        public char Char { get; set; }
    }
}
