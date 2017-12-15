using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day12
    {
        public string answer = "";
        public Day12(string input, bool part2)
        {
            answer = Pipe(input, part2);
        }

        string Pipe(string input, bool part2)
        {
            var progs = new List<Prog>();
            var inputs = input.Split('\n');
            foreach(var i in inputs)
            {
                var id = Convert.ToInt32(i.Substring(0, i.IndexOf(' ')));
                var link = i.Substring(i.IndexOf('>') + 1).Trim().Split(',').Select(x=>Convert.ToInt32(x)).ToList();
                var p = new Prog(id, link);
                progs.Add(p);
            }

            var groups = 0;
            bool part1 = true;
            while(part2 && progs.Count > 0 || part1)
            {
                var linked = new List<Prog>() { progs[0] };
                var next = new List<int>();
                next.AddRange(progs[0].Links);
                while (next.Count > 0)
                {
                    var id = next[0];
                    try
                    {
                        var currentProgram = progs.Where(x => x.ID == id).SingleOrDefault();
                        if (!linked.Contains(currentProgram))
                        {
                            linked.Add(currentProgram);
                            next.AddRange(currentProgram.Links);
                        }
                        next.RemoveAt(0);
                        if (part2)
                        {
                            progs.Remove(currentProgram);
                        }
                    }
                    catch
                    {
                        next.RemoveAt(0);
                    }
                }
                if (!part2)
                {
                    groups = linked.Count;
                }
                else
                {
                    groups += 1;
                }
                part1 = false;
            }

            return groups.ToString();
        }
    }
    class Prog
    {
        public Prog(int pID)
        {
            ID = pID;
        }
        public Prog(int pID, List<int> conList)
        {
            ID = pID;
            Links = new List<int>();
            foreach(var i in conList)
            {
                Links.Add(i);
            }
        }
        public int ID { get; set; }
        public List<int> Links { get; set; }
    }
}
