using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day2
    {
        public string answer = "";

        public Day2(string input, bool part2)
        {
            if (part2)
            {
                answer = ReadSpreadsheet(input, part2);
            }
            else
            {
                answer = ReadSpreadsheet(input);
            }
        }
        string ReadSpreadsheet(string input)
        {
            var inputs = input.Split('\n');
            int sumVal = 0;
            foreach (var i in inputs)
            {
                var v = i.Split('\t');
                int maxVal = 0;
                int minVal = 1000000;
                foreach(var n in v)
                {
                    int num = Convert.ToInt32(n);
                    if(num > maxVal)
                    {
                        maxVal = num;
                    }
                    if(num < minVal)
                    {
                        minVal = num;
                    }
                }
                sumVal = sumVal + (maxVal - minVal);
            }

            return sumVal.ToString();
        }
        string ReadSpreadsheet(string input, bool part2)
        {
            int sumVal = 0;
            var inputs = input.Split('\n');
            foreach(var i in inputs)
            {
                var v = i.Split('\t');
                var nums = new List<int>();
                foreach(var num in v)
                {
                    nums.Add(Convert.ToInt32(num));
                }
                for(int j = 0; j < nums.Count; j++)
                {
                    int div = 0;
                    for (int k = j + 1; k < nums.Count; k++)
                    {
                        int remainder;
                        int tempDiv;
                        int num1 = nums[j];
                        int num2 = nums[k];
                        if(num1 > num2)
                        {
                            remainder = num1 % num2;
                            tempDiv = num1 / num2;
                        }
                        else
                        {
                            remainder = num2 % num1;
                            tempDiv = num2 / num1;
                        }
                        if(remainder == 0)
                        {
                            div = tempDiv;
                            break;
                        }
                    }
                    if(div != 0)
                    {
                        sumVal = sumVal + div;
                        break;
                    }
                }
            }
            return sumVal.ToString();
        }
    }
}
