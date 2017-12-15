using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day3
    {
        public string answer = "";

        public Day3(string input)
        {
            Memory chipLocation = MemoryCount(input);
            int x = Math.Abs(chipLocation.x);
            int y = Math.Abs(chipLocation.y);
            answer = (x + y).ToString();
        }
        Memory MemoryCount(string input)
        {
            int numSteps = 1;
            int memLoc = Convert.ToInt32(input);
            int x = 0;
            int y = 0;
            int sideLength = 0;
            var current = new Memory(x,y,numSteps);

            while(numSteps <= memLoc)
            {
                //step right 1 then go up
                x += 1;
                sideLength += 2;
                for (int j = 0; j < sideLength; j++)
                {
                    numSteps += 1;
                    current = new Memory(x, y, numSteps);
                    if(numSteps == memLoc)
                    {
                        return (current);
                    }
                    if (j < sideLength - 1)
                    {
                        y += 1;
                    }
                }
                //go left
                for(int j = 0; j < sideLength; j++)
                {
                    numSteps += 1;
                    x -= 1;
                    current = new Memory(x, y, numSteps);
                    if (numSteps == memLoc)
                    {
                        return (current);
                    }
                }
                //go down
                for(int j = 0; j < sideLength; j++)
                {
                    numSteps += 1;
                    y -= 1;
                    current = new Memory(x, y, numSteps);
                    if (numSteps == memLoc)
                    {
                        return (current);
                    }
                }
                //go right
                for(int j = 0; j < sideLength; j++)
                {
                    numSteps += 1;
                    x += 1;
                    current = new Memory(x, y, numSteps);
                    if (numSteps == memLoc)
                    {
                        return (current);
                    }
                }
            }
            return current;
        }
    }

    class Memory
    {
        public Memory(int xVal, int yVal, int locVal)
        {
            x = xVal;
            y = yVal;
            value = locVal;

        }
        public int x { get; set; }
        public int y { get; set; }
        public int value { get; set; }
    }
}
