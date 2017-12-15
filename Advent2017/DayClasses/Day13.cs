using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day13
    {
        public string answer = "";
        public Day13(string input, bool part2)
        {
            answer = Firewall(input, part2);
        }
        string Firewall(string input, bool part2)
        {
            var inputs = input.Split('\n');
            var firewalls = new List<FireWall>();
            int maxDepth = 0;
            int penalty = 0;
            foreach (var i in inputs)
            {
                var depth = Convert.ToInt32(i.Substring(0, i.IndexOf(':')));
                var severity = Convert.ToInt32(i.Substring(i.IndexOf(' ') + 1));
                var f = new FireWall(depth, severity);
                firewalls.Add(f);
                maxDepth = depth;
            }
            if (part2)
            {
                int delay = 0;
                while (true)
                {
                    penalty = 0;
                    delay += 1;
                    foreach(var f in firewalls)
                    {
                        f.Position = PositionAt(f,delay);
                        if(f.Position == 0)
                        {
                            penalty += Math.Max(f.Depth,1);
                            break;
                        }
                    }
                    if(penalty == 0)
                    {
                        penalty = delay;
                        break;
                    }
                }
            }
            else
            {
                penalty = RunFirewalls(firewalls, maxDepth, part2);
            }

            return penalty.ToString();
        }

        private int PositionAt(FireWall f, int delay)
        {
            int time = delay + f.Depth;
            int position = time % (2 * (f.Severity - 1));
            return position;
        }

        private static int RunFirewalls(List<FireWall> firewalls, int maxDepth, bool part2)
        {
            int penalty = 0;
            var caughtBy = new List<FireWall>();
            for (int i = 0; i <= maxDepth; i++)
            {
                var fw = firewalls.Where(x => x.Depth == i).SingleOrDefault();
                if (fw != null)
                {
                    if (fw.Position == 0)
                    {
                        caughtBy.Add(fw);
                    }
                }
                foreach (var f in firewalls)
                {
                    f.TimeStep();
                }
            }
            foreach (var c in caughtBy)
            {
                penalty += (c.Depth * c.Severity);
            }
            if (!part2)
            {
                return penalty;
            }
            else
            {
                return caughtBy.Count;
            }
        }
    }
    class FireWall
    {
        public FireWall(int d, int s)
        {
            Depth = d;
            Severity = s;
            Position = 0;
            Down = true;
        }
        public int Depth { get; set; }
        public int Severity { get; set; }
        public int Position { get; set; }
        public bool Down { get; set; }
        public void TimeStep()
        {
            if(Position == Severity - 1)
            {
                Down = false;
            }
            else if(Position == 0)
            {
                Down = true;
            }
            if (Down)
            {
                Position += 1;
            }
            else
            {
                Position -= 1;
            }
        }
    }
}
