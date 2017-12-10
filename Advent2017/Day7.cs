using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day7
    {
        public string answer = "";
        public Day7(string input, bool part2)
        {
            var inputs = input.Split('\n');
            answer = MemBalance(inputs, part2);
        }

        string MemBalance(string[] inputs, bool part2)
        {
            var Processes = new List<MemoryProcess>();
            foreach(var i in inputs)
            {
                var a = i.Split(' ');
                if(a.Length == 2)
                {
                    var m = new MemoryProcess(a[0], Convert.ToInt32(a[1].Replace("(","").Replace(")","")));
                    Processes.Add(m);
                }
                else
                {
                    var ls = new List<MemoryProcess>();
                    for(int j = 3; j < a.Length; j++)
                    {
                        string placeHolder = a[j].Replace(",", "");
                        ls.Add(new MemoryProcess(placeHolder));
                    }
                    var m = new MemoryProcess(a[0], Convert.ToInt32(a[1].Replace("(","").Replace(")","")), ls);
                    Processes.Add(m);
                }
            }

            foreach(var p in Processes)
            {
                if(p.ChildProcesses != null && p.ChildProcesses.Count > 0)
                {
                    foreach(var c in p.ChildProcesses)
                    {
                        var f = Processes.Where(x => x.Name == c.Name).SingleOrDefault();
                        f.Parent = p.Name;
                    }
                }
            }

            var l = Processes.Where(x => x.Parent == null || x.Parent == "").SingleOrDefault();
            if (!part2)
            {
                return l.Name;
            }
            else
            {
                foreach(var p in Processes)
                {
                    if(p.Parent != null)
                    {
                        var pa = Processes.Where(x => x.Name == p.Parent).SingleOrDefault();
                        var c = pa.ChildProcesses.Where(x => x.Name == p.Name).SingleOrDefault();
                        c.Size = p.Size;
                        c.ChildProcesses = p.ChildProcesses;
                    }
                }
                int weight = l.TotalWeight();
                var childWeight = weight / l.ChildProcesses.Count;
                return weight.ToString();
            }
        }
    }

    class MemoryProcess
    {
        public MemoryProcess(string nm)
        {
            Name = nm;
        }
        public MemoryProcess(string nm, int sz)
        {
            Name = nm;
            Size = sz;
        }

        public MemoryProcess(string nm, int sz, List<MemoryProcess> children)
        {
            Name = nm;
            Size = sz;
            ChildProcesses = children;
        }

        public string Name { get; set; }
        public int Size { get; set; }
        public List<MemoryProcess> ChildProcesses { get; set; }
        public string Parent { get; set; }

        public int Carrying { get; set; }

        public int TotalWeight()
        {
            if(ChildProcesses != null)
            {
                Carrying = ChildProcesses.Sum(c => c.TotalWeight());
                return Size + Carrying;
            }
            else
            {
                return Size;
            }
        }
    }
}
