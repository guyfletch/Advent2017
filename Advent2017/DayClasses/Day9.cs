using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day9
    {
        public string answer = "";
        public Day9(string input, bool part2)
        {
            answer = GarbageParse(input, part2);
        }

        string GarbageParse(string input, bool part2)
        {
            int level = 0;
            int score = 0;
            int removed = 0;
            bool garbage = false;
            bool skip = false;
            var sb = input.ToArray();
            for (int i = 0; i < sb.Count(); i++)
            {
                if (!skip)
                {
                    if (!garbage)
                    {
                        if (sb[i] == '{')
                        {
                            level += 1;
                        }
                        else if (sb[i] == '<')
                        {
                            garbage = true;
                        }
                        else if (sb[i] == '}')
                        {
                            score += level;
                            level -= 1;
                        }
                    }
                    else
                    {
                        if (sb[i] == '!')
                        {
                            skip = true;
                        }
                        else if (sb[i] == '>')
                        {
                            garbage = false;
                        }
                        else
                        {
                            removed += 1;
                        }
                    }
                }
                else
                {
                    skip = false;
                }

            }
            if (!part2)
            {
                return score.ToString();
            }
            else
            {
                return removed.ToString();
            }
        }
    }
}
