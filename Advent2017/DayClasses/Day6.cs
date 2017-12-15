using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day6
    {
        public string answer = "";
        public Day6(string input, bool part2)
        {
            answer = MemShuffle(input, part2);
        }
        string MemShuffle(string input, bool part2)
        {
            int stepCount = 0;
            var memories = input.Split('\t').Select(x=>Convert.ToInt32(x)).ToArray();
            var history = new List<string>();
            history.Add(string.Join(",",memories));
            while (true)
            {
                var redistribute = memories.Max();
                var maxMemLoc = Array.IndexOf(memories, redistribute);
                memories[maxMemLoc] = 0;
                int workingIndex = maxMemLoc;
                while (redistribute > 0)
                {
                    workingIndex += 1;
                    if(workingIndex >= memories.Count())
                    {
                        workingIndex = 0;
                    }
                    memories[workingIndex] += 1;
                    redistribute -= 1;
                }
                stepCount += 1;
                string memory = string.Join(",", memories);
                if (!history.Contains(memory))
                {
                    history.Add(memory);
                }
                else
                {
                    history.Add(memory);
                    break;
                }    
            }
            if (part2)
            {
                return (stepCount -  history.IndexOf(history.Last())).ToString();
            }
            return stepCount.ToString();
        }
    }
}
