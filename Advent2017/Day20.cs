using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2017
{
    class Day20
    {
        public string answer = "";
        public Day20(string input, bool part2)
        {
            answer = Particles(input, part2);
        }

        string Particles(string input, bool part2)
        {
            var parts = new List<Particle>();
            Regex r = new Regex("-?\\d+");
            var inputs = input.Split('\n');
            var idNum = 0;
            foreach(var i in inputs)
            {
                var els = new List<int>();
                var el = r.Matches(i);
                foreach(var m in el)
                {
                    els.Add(Convert.ToInt32(m.ToString()));
                }
                var p = new Particle(idNum, new xyz(els[0], els[1], els[2]), new xyz(els[3], els[4], els[5]), new xyz(els[6], els[7], els[8]));
                parts.Add(p);
                idNum += 1;
            }
            for(int i = 0; i < 1000; i++)
            {
                foreach (var p in parts)
                {
                    //var currents = new Dictionary<int, List<int>>();
                    if (!p.Collide)
                    {
                        p.Step();
                        //var posl = new List<int>() { p.position.x, p.position.y, p.position.z };
                        //if (currents.ContainsValue(posl))
                        //{
                        //    p.Collide = true;
                        //    var p2 = currents.Where(x=>x.Value == posl).Select(x => x.Key).Single();
                        //    var p1 = parts.Where(pr => pr.ID == p2).Single();
                        //    p1.Collide = true;
                        //}
                        //else
                        //{
                        //    currents.Add(p.ID, posl);
                        //}
                    }
                }
                foreach(var p in parts)
                {
                    if (!p.Collide)
                    {
                        var matches = parts.Where(par => par.position.x == p.position.x).Where(par => par.position.y == p.position.y).Where(par => par.position.z == p.position.z);
                        if (matches.Count() > 1)
                        {
                            foreach (var m in matches)
                            {
                                m.Collide = true;
                            }
                        }
                    }
                }
            }
            long minSum = Int64.MaxValue;
            foreach(var p in parts)
            {
                var i = p.Sum();
                if(i < minSum)
                {
                    minSum = i;
                }
            }
            var part = parts.Where(p => p.Sum() == minSum).Single();
            var index = parts.IndexOf(part);
            if (!part2)
            {
                return index.ToString();
            }
            else
            {
                return parts.Where(p => p.Collide == false).Count().ToString();
            }
        }
    }
    class Particle
    {
        public Particle(int idnum)
        {
            ID = idnum;
            position = new xyz();
            velocity = new xyz();
            acceleration = new xyz();
            past = new List<xyz>();
            pastM = new List<int>();
            Collide = false;
        }
        public Particle(int idnum, xyz pos, xyz vel, xyz acc)
        {
            ID = idnum;
            position = pos;
            velocity = vel;
            acceleration = acc;
            past = new List<xyz>();
            pastM = new List<int>();
            Collide = false;
        }
        public xyz position { get; set; }
        public xyz velocity { get; set; }
        public xyz acceleration { get; set; }
        public List<xyz> past { get; set; }
        public List<int> pastM { get; set; }
        public int Step()
        {
            velocity.x += acceleration.x;
            velocity.y += acceleration.y;
            velocity.z += acceleration.z;
            position.x += velocity.x;
            position.y += velocity.y;
            position.z += velocity.z;
            past.Add(position);
            pastM.Add(Distance());
            return 0;
        }
        public long Sum()
        {
            long l = 0;
            try
            {
                l = pastM.Sum();
            }
            catch
            {
                l = Int64.MaxValue;
            }
            return l;
        }
        public int Distance()
        {
            return Math.Abs(position.x) + Math.Abs(position.y) + Math.Abs(position.z);
        }
        public bool Collide { get; set; }
        public int ID { get; set; }
    }

    class xyz
    {
        public xyz()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public xyz(int a, int b, int c)
        {
            x = a;
            y = b;
            z = c;
        }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }
}
