using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day16
    {
        public string answer = "";
        public Day16(string input, bool part2)
        {
            answer = programDance(input, part2);
        }
        string programDance(string input, bool part2)
        {
            var moves = input.Split(',');
            string dancers = "abcdefghijklmnop";
            var maxLoop = 1;
            var pastSteps = new List<string>();
            if (part2)
            {
                maxLoop = 1000000000 % 48;
            }
            for (int i = 0; i < maxLoop; i++)
            {
                foreach (var move in moves)
                {
                    switch (move[0])
                    {
                        case 'x':
                            var exchange = move.Substring(1).Split('/')
                                .Select(x => Convert.ToInt32(x)).ToArray();
                            var s1 = dancers[exchange[0]];
                            var s2 = dancers[exchange[1]];
                            dancers = dancers.Replace(s1, 'x')
                                .Replace(s2, s1)
                                .Replace('x', s2);
                            break;
                        case 's':
                            var size = Convert.ToInt32(move.Substring(1));
                            var sb = new StringBuilder();
                            sb.Append(dancers.Substring(dancers.Length - size));
                            dancers = sb.Append(dancers.Substring(0, dancers.Length - size)).ToString();
                            break;
                        case 'p':
                            var swap = move.Substring(1).Split('/').ToArray();
                            dancers = dancers.Replace(swap[0], "x")
                                .Replace(swap[1], swap[0])
                                .Replace("x", swap[1]);
                            break;
                    }
                }
                if (!pastSteps.Contains(dancers))
                {
                    pastSteps.Add(dancers);
                }
                else
                {
                    return i.ToString() + "," + pastSteps.Count.ToString();
                }
            }
            return dancers;
        }
    }
}


