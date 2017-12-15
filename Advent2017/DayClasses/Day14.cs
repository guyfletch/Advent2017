using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day14
    {
        public string answer = "";
        public Day14(string input, bool part2)
        {
            answer = Defrag(input, part2);
        }
        string Defrag(string input, bool part2)
        {
            int ones = 0;
            var frags = new List<string>();
            for(int i = 0; i < 128; i++)
            {
                var d = new Day10(input + "-" + i, true);
                string placeholder = d.answer;
                placeholder = String.Join(String.Empty, placeholder.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                frags.Add(placeholder);
            }
            if (!part2)
            {
                foreach (var f in frags)
                {
                    string test = f.Replace("0", "");
                    ones += test.Length;
                }
                return ones.ToString();
            }
            else
            {
                var visited = new bool[128, 128];
                int regions = 0;
                for(int y = 0; y < visited.GetLength(1);y++)
                {
                    for(int x = 0; x< visited.GetLength(0);x++)
                    {
                        if(visited[x,y] || frags[x][y] == '0')
                        {
                            continue;
                        }
                        this.Visit(x, y, frags, visited);
                        regions++;
                    }
                }
                return regions.ToString();
            }
        }
        private void Visit(int x, int y, List<string> input, bool[,] visited)
        {
            if (visited[x, y])
            {
                return;
            }

            visited[x, y] = true;

            if (input[x][y] == '0')
            {
                return;
            }

            if (x > 0) this.Visit(x - 1, y, input, visited);
            if (x < 127) this.Visit(x + 1, y, input, visited);
            if (y > 0) this.Visit(x, y - 1, input, visited);
            if (y < 127) this.Visit(x, y + 1, input, visited);
        }
    }
}
