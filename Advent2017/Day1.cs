using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    public class Day1
    {

        public string answer = "";
        public Day1(string input, Boolean part2)
        {
            if (part2)
            {
                answer = ReportValueB(input);
            }
            else
            {
                answer = ReportValue(input);
            }
        }
        public string ReportValue(string input)
        {
            string firstChar = input.Substring(0, 1);
            string testStr = input + firstChar;
            int outVal = 0;
            for (int i = 1; i < testStr.Length; i++)
            {
                int value1 = Convert.ToInt32(testStr.Substring(i - 1, 1));
                int value2 = Convert.ToInt32(testStr.Substring(i,1));
                if (value1 == value2)
                {
                    outVal += value1;
                }
            }
            return outVal.ToString();
        }

        public string ReportValueB(string input)
        {
            int outVal = 0;
            int loopVal = input.Length / 2;
            for(int i = 0; i < loopVal; i++)
            {
                int value1 = Convert.ToInt32(input.Substring(i,1));
                int value2 = Convert.ToInt32(input.Substring(loopVal + i, 1));
                if(value1 == value2)
                {
                    outVal += value1;
                    outVal += value2;
                }
            }
            return outVal.ToString();
        }
    }
}
