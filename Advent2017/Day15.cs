using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day15
    {
        public string answer = "";
        public Day15(string input, bool part2)
        {
            answer = GeneratorMatch(input,part2).ToString();
        }
        int GeneratorMatch(string input, bool part2)
        {
            int matches = 0;
            var generators = input.Split('\n');
            var a = generators[0].Split(' ');
            var b = generators[1].Split(' ');
            var previousA = Convert.ToInt64(a[1]);
            var previousB = Convert.ToInt64(b[1]);
            var factorA = 16807L;
            var factorB = 48271L;
            var loopMax = 0;
            if (!part2)
            {
                loopMax = 40000000;
            }
            else
            {
                loopMax = 5000000;
            }

            for(int i = 0; i < loopMax; i++)
            {
                previousA = (previousA * factorA) % Int32.MaxValue;
                previousB = (previousB * factorB) % Int32.MaxValue;
                if (part2)
                {
                    while(previousA % 4 != 0)
                    {
                        previousA = (previousA * factorA) % Int32.MaxValue;
                    }
                    while(previousB % 8 != 0)
                    {
                        previousB = (previousB * factorB) % Int32.MaxValue;
                    }
                }
                var testa = Convert.ToString(previousA, 2).PadLeft(16,'0');
                var testb = Convert.ToString(previousB, 2).PadLeft(16,'0');
                testa = testa.Substring(testa.Length - 16);
                testb = testb.Substring(testb.Length - 16);
                if(testa == testb)
                {
                    matches += 1;
                }
            }

            return matches;
        }
    }
}
