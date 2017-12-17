using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day17
    {
        public string answer = "";
        public Day17(string input, bool part2)
        {
            answer = Spinlock(input, part2);
        }
        string Spinlock(string input, bool part2)
        {
            var loopMax = 2018;
            if (part2)
            {
                loopMax = 50000001;
            }
            var spinner = new List<string>() { "0" };
            var loc = 0;
            var step = Convert.ToInt32(input);
            var target = 0;
            if (!part2)
            {
                for (int i = 1; i < loopMax; i++)
                {
                    loc = (loc + step) % i + 1;
                    spinner.Insert(loc, i.ToString());
                }
            }
            else
            {
                for(int i = 1; i < loopMax; i++)
                {
                    loc = ((loc + step)%i) + 1;
                    if(loc == 1)
                    {
                        target = i;
                    }
                }
            }
            if (!part2)
            {
                loc = spinner.IndexOf("2017");
            }
            else
            {
                return target.ToString();
            }
            return spinner[loc + 1];
        }
    }
}
