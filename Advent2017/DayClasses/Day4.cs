using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day4
    {
        public string answer = "";

        public Day4(string input)
        {
            answer = PassPhraseCheck(input);
        }
        public string PassPhraseCheck(string input)
        {
            int phraseCount = 0;

            var inputs = input.Split('\n');
            foreach(var i in inputs)
            {
                string line = i.Replace("\r", "");
                bool valid = true;
                var words = line.Split(' ');
                var wordSeen = new List<string>();
                var anagram = new List<string>();
                foreach(var w in words)
                {
                    if (wordSeen.Contains(w))
                    {
                        valid = false;
                        break;
                    }
                    var w2 = String.Concat(w.OrderBy(c => c));
                    if (anagram.Contains(w2))
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        wordSeen.Add(w);
                        anagram.Add(w2);
                    }
                }
                if (valid)
                {
                    phraseCount += 1;
                }
            }

            return phraseCount.ToString();
        }
    }
}
