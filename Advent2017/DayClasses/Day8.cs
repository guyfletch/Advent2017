using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day8
    {
        public string answer = "";
        public Day8(string input, bool part2)
        {
            answer = RegCalc(input, part2);
        }
        string RegCalc(string input, bool part2)
        {
            var Registers = new List<Reg>();
            var inputs = input.Split('\n');
            int allTimeMax = 0;
            foreach (var i in inputs)
            {
                var arr = i.Split(' ');
                string regName = arr[0];
                var r = Registers.Where(x => x.Name == regName).SingleOrDefault();
                if (r == null)
                {
                    r = new Reg(regName);
                    Registers.Add(r);
                }
            }
            foreach(var i in inputs)
            {
                var arr = i.Split(' ');
                string regName = arr[0];
                var r = Registers.Where(x => x.Name == regName).SingleOrDefault();
                string conditionOp = arr[5];
                string conditionReg = arr[4];
                int conditionVal = Convert.ToInt32(arr[6]);
                string direction = arr[1];
                int distance = Convert.ToInt32(arr[2]);
                var test = Registers.Where(x => x.Name == conditionReg).SingleOrDefault();
                bool conditionTrue = false;
                switch (conditionOp)
                {
                    case ">":
                        if(test.Value > conditionVal)
                        {
                            conditionTrue = true;
                        }
                        break;
                    case ">=":
                        if(test.Value >= conditionVal)
                        {
                            conditionTrue = true;
                        }
                        break;
                    case "<":
                        if(test.Value < conditionVal)
                        {
                            conditionTrue = true;
                        }
                        break;
                    case "<=":
                        if(test.Value <= conditionVal)
                        {
                            conditionTrue = true;
                        }
                        break;
                    case "==":
                        if(test.Value == conditionVal)
                        {
                            conditionTrue = true;
                        }
                        break;
                    case "!=":
                        if(test.Value != conditionVal)
                        {
                            conditionTrue = true;
                        }
                        break;
                }
                if (conditionTrue && direction == "inc")
                {
                    r.Value = r.Value + distance;
                }
                else if(conditionTrue && direction == "dec")
                {
                    r.Value = r.Value - distance;
                }

                if (r.Value > allTimeMax)
                {
                    allTimeMax = r.Value;
                }

            }

            var max = Registers.Max(y => y.Value);
            if (!part2)
            {
                return max.ToString();
            }
            else
            {
                return allTimeMax.ToString();
            }
        }
    }
    class Reg
    {
        public Reg(string nm)
        {
            Name = nm;
            Value = 0;
        }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
