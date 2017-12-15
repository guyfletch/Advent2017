using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day5
    {
        public string answer = "";

        public Day5(string input, bool part2)
        {
            answer = ExitSteps(input, part2);
        }
        public string ExitSteps(string input, bool part2)
        {
            int steps = 0;
            var instructions = input.Split('\n').Select(x => Convert.ToInt32(x)).ToList();
            int currentPos = 0;
            while(currentPos < instructions.Count)
            {
                int move = Convert.ToInt32(instructions[currentPos]);
                if(!part2 || move < 3)
                {
                    instructions[currentPos] = move + 1;
                }
                else
                {
                    instructions[currentPos] = move - 1;
                }
                currentPos += move;
                if(currentPos < 0)
                {
                    currentPos = 0;
                }
                steps += 1;
            }

            return steps.ToString();
        }
    }
}
