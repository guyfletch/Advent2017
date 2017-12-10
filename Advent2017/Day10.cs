using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day10
    {
        public string answer = "";
        public Day10(string input, bool part2)
        {
            answer = KnotHash(input, part2);
        }
        string KnotHash(string input, bool part2)
        {
            int length = 0;
            int skip = 0;
            int position = 0;
            int output = 0;
            var hash = new int[256];
            for (int j = 0; j < hash.Length; j++)
            {
                hash[j] = j;
            }
            var inputs = new List<int>(); // new int[input.Length];
            if (!part2)
            {
                inputs = input.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                HashOnce(ref length, ref skip, ref position, hash, inputs);
                output = hash[0] * hash[1];
                return output.ToString();
            }
            else
            {
                inputs = Encoding.ASCII.GetBytes(input).Select(x => Convert.ToInt32(x)).ToList();
                inputs.Add(17);
                inputs.Add(31);
                inputs.Add(73);
                inputs.Add(47);
                inputs.Add(23);
                for(int i = 0; i < 64; i++)
                {
                    HashOnce(ref length, ref skip, ref position, hash, inputs);
                }
                var dense = new int[16];
                for(int j = 0; j < 16; j++)
                {
                    var sparse = new int[16];
                    sparse = hash.Skip(j * 16).Take(16).ToArray();
                    int a = sparse[0] ^ sparse[1] ^ sparse[2] ^ sparse[3] ^
                        sparse[4] ^ sparse[5] ^ sparse[6] ^ sparse[7] ^
                        sparse[8] ^ sparse[9] ^ sparse[10] ^ sparse[11] ^
                        sparse[12] ^ sparse[13] ^ sparse[14] ^ sparse[15];
                    dense[j] = a;
                }
                string outStr = "";
                for(int k = 0; k < 16; k++)
                {
                    var digit = dense[k];
                    string hexVal = digit.ToString("X");
                    if(hexVal.Length == 1)
                    {
                        hexVal = "0" + hexVal;
                    }
                    outStr = outStr + hexVal;
                }
                return outStr;
            }

        }

        private static void HashOnce(ref int length, ref int skip, ref int position, int[] hash, List<int> inputs)
        {
            foreach (var i in inputs)
            {
                length = Convert.ToInt32(i);
                var selection = new int[length];
                for (int j = 0; j < length; j++)
                {
                    int location = j + position;
                    while(location > 255)
                    {
                        location = location - 256;
                    }
                    selection[j] = hash[location];
                }
                selection = selection.Reverse().ToArray();
                for (int j = 0; j < length; j++)
                {
                    int location = j + position;
                    while(location > 255)
                    {
                        location = location - 256;
                    }
                    hash[location] = selection[j];
                }
                position = position + length + skip;
                while(position > 255)
                {
                    position -= 256;
                }
                skip += 1;
                if(skip > 255)
                {
                    skip = skip - 256;
                }
            }
        }
    }
}
