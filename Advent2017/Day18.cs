using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017
{
    class Day18
    {

        public static string PartTwo(string input)
        {
            var p0 = new DuetProgram(input);
            var p1 = new DuetProgram(input);

            p1.Registers['p'] = 1;

            var p1SendCount = 0;

            while (true)
            {
                p0.Execute();
                p1.InputQueue = p0.OutputQueue;
                p0.OutputQueue = new List<long>();
                p1.Execute();
                p0.InputQueue = p1.OutputQueue;
                p1SendCount += p1.OutputQueue.Count;
                p1.OutputQueue = new List<long>();

                if (p0.InputQueue.Count == 0 && p1.InputQueue.Count == 0)
                {
                    return p1SendCount.ToString();
                }
            }

            throw new Exception();
        }

        public string answer = "";
        public Day18(string input, bool part2)
        {
            if (!part2)
            {
                answer = ReadReg(input);
            }
            else
            {
                answer = Duet(input);
            }
        }
        string ReadReg(string input)
        {
            var inputs = input.Split('\n');
            long recover = 0;
            var regs = new List<Register>();
            long lastFreq = -1;
            for (int j = 0; j < inputs.Count(); j++)
            {
                var lst = inputs[j].Split(' ');
                var r = regs.Where(x => x.Name == lst[1].Replace('\r',' ').Trim()).SingleOrDefault();
                var r2 = new Register("test");
                var v = 0;
                bool isInt = false;
                if(lst.Length > 2)
                {
                    r2 = regs.Where(x => x.Name == lst[2].Replace('\r', ' ').Trim()).SingleOrDefault();
                    isInt = int.TryParse(lst[2], out v);
                }
                else
                {
                    r2 = null;
                }
                switch (lst[0])
                {
                    case "set":
                        if (r == null)
                        {
                            if(isInt)
                            {
                                regs.Add(new Register(lst[1], v));
                            }
                            else if(r2 != null)
                            {
                                regs.Add(new Register(lst[1], r2.Value));
                            }
                            else
                            {
                                regs.Add(new Register(lst[1]));
                            }
                        }
                        else
                        {
                            if (isInt)
                            {
                                r.Value = v;
                            }
                            else if(r2 != null)
                            {
                                r.Value = r2.Value;
                            }
                            else
                            {
                                r.Value = 0;
                            }
                        }
                        break;
                    case "snd":
                        if(isInt)
                        {
                            if(v > 0)
                            {
                                lastFreq = v;
                            }
                        }
                        else if(r != null && r.Value != 0)
                        {
                            lastFreq = r.Value;
                        }
                        break;
                    case "add":
                        if (r == null)
                        {
                            regs.Add(new Register(lst[1]));
                            r = regs.Where(x => x.Name == lst[1]).SingleOrDefault();
                        }
                        if (isInt)
                        {
                            r.Value = r.Value + v;
                        }
                        else if(r2 != null)
                        {
                            r.Value = r.Value + r2.Value;
                        }
                        break;
                    case "mul":
                        if (r == null)
                        {
                            regs.Add(new Register(lst[1]));
                        }
                        else if (r2 != null)
                        {
                            r.Value = r.Value * r2.Value;
                        }
                        else if (isInt)
                        {
                            r.Value = r.Value * v;

                        }
                        break;
                    case "mod":
                        if (r == null)
                        {
                            regs.Add(new Register(lst[1]));
                        }
                        else if (r2 != null)
                        {
                            r.Value = r.Value % r2.Value;
                        }
                        else if (isInt)
                        {
                            r.Value = r.Value % v;
                        }
                        break;
                    case "rcv":
                        if(r == null)
                        {
                            regs.Add(new Register(lst[1]));
                        }
                        else if(r.Value != 0)
                        {
                            recover = lastFreq;
                            return recover.ToString();
                        }
                        break;
                    case "jgz":
                        if(r == null)
                        {
                            regs.Add(new Register(lst[1]));
                        }
                        else if(r.Value > 0)
                        {
                            if(r2 != null)
                            {
                                j = j + Convert.ToInt32(r2.Value) - 1;
                            }
                            else if (isInt)
                            {
                                j = j + v - 1;
                            }
                        }
                        break;
                }
            }
            return "";
        }
        string Duet(string input)
        {
            var inputs = input.Split('\n');
            var progA = new Program(0);
            var progB = new Program(1);
            int prog1Sent = 0;
            while((progA.Running || progB.Running))
            {

                for(int j = progA.step; j < inputs.Count(); j++)
                {
                    var cmd = inputs[j].Split(' ');
                    long l = 0;
                    if(cmd.Count() > 2)
                    {
                        l = Int64.TryParse(cmd[2], out l) ? l : progA.regs.Where(x => x.Name == cmd[2].Replace('\r', ' ').Trim()).SingleOrDefault().Value;
                    }
                    var r = progA.regs.Where(x => x.Name == cmd[1].Replace('\r', ' ').Trim()).SingleOrDefault();
                    j = Step(r, inputs[j], l, j, progA);
                    progA.step = j;
                    if(progA.Waiting || !progA.Running)
                    {
                        j = inputs.Count() + 1;
                    }
                }
                for (int j = progB.step; j < inputs.Count(); j++)
                {
                    var cmd = inputs[j].Split(' ');
                    long l = 0;
                    if (cmd.Count() > 2)
                    {
                        l = Int64.TryParse(cmd[2], out l) ? l : progB.regs.Where(x => x.Name == cmd[2].Replace('\r', ' ').Trim()).SingleOrDefault().Value;
                    }
                    var r = progB.regs.Where(x => x.Name == cmd[1].Replace('\r', ' ').Trim()).SingleOrDefault();
                    j = Step(r, inputs[j], l, j, progB);
                    progB.step = j;
                    if (progB.Waiting || !progB.Running)
                    {
                        j = inputs.Count() + 1;
                    }
                }
                if (progA.snd.Count() > 0)
                {
                    var sent = new List<long>();
                    foreach (var s in progA.snd)
                    {
                        var rb = progB.Rec.FirstOrDefault();
                        if (rb != null)
                        {
                            progB.Rec.Remove(rb);
                            var rbReg = progB.regs.Where(x => x.Name == rb).SingleOrDefault();
                            rbReg.Value = s;
                            sent.Add(s);
                        }
                        else
                        {
                            progB.Waiting = false;
                            break;
                        }
                    }
                    foreach (var s in sent)
                    {
                        progA.snd.Remove(s);
                    }
                }
                if (progB.snd.Count() > 0)
                {
                    var sent = new List<long>();
                    foreach (var s in progB.snd)
                    {
                        var rb = progA.Rec.FirstOrDefault();
                        if (rb != null)
                        {
                            progA.Rec.Remove(rb);
                            var rbReg = progA.regs.Where(x => x.Name == rb).SingleOrDefault();
                            rbReg.Value = s;
                            sent.Add(s);
                            prog1Sent += 1;
                        }
                        else
                        {
                            progA.Waiting = false;
                            break;
                        }
                    }
                    foreach (var s in sent)
                    {
                        progB.snd.Remove(s);
                    }
                }
                if(progB.snd.Count() == 0 && progA.Rec.Count > 0)
                {
                    progA.Running = false;
                }
                if(progA.snd.Count() == 0 && progB.Rec.Count > 0)
                {
                    progB.Running = false;
                }
            }
            return prog1Sent.ToString();
        }

        int Step(Register r, string cmd, long l, int step, Program prog)
        {
            var x = cmd.Split(' ');
            int returnStep = step;
            switch (x[0])
            {
                case "set":
                    r.Value = l;
                    break;
                case "add":
                    r.Value = r.Value + l;
                    break;
                case "mul":
                    r.Value = r.Value * l;
                    break;
                case "mod":
                    r.Value = r.Value & l;
                    break;
                case "jgz":
                    if(r.Value > 0)
                    {
                        returnStep = step + (int)l;
                    }
                    break;
                case "snd":
                    long v = 0;
                    v = Int64.TryParse(x[1], out v) ? v : prog.regs.Where(y=>y.Name == x[1].Replace('\r',' ').Trim()).Single().Value;
                    prog.snd.Add(v);
                    break;
                case "rcv":
                    prog.Rec.Add(r.Name);
                    prog.Waiting = true;
                    break;

            }
            return returnStep;
        }
    }
    class Register
    {
        public Register(string nm)
        {
            Name = nm;
            Value = 0;
        }
        public Register(string nm, long val)
        {
            Name = nm;
            Value = val;
        }
        public string Name { get; set; }
        public long Value { get; set; }
    }
    class Program
    {
        public Program(int id)
        {
            ID = id;
            step = 0;
            regs = new List<Register>() { new Register("a"), new Register("b"), new Register("f"), new Register("i"), new Register("p", id) };
            Running = true;
            Waiting = false;
            Rec = new List<string>();
            snd = new List<long>();
        }
        public int ID { get; set; }
        public int step { get; set; }
        public List<Register> regs { get; set; }
        public List<long> snd { get; set; }
        public bool Running { get; set; }
        public bool Waiting { get; set; }
        public List<string> Rec { get; set; }
    }

    public class DuetProgram
    {
        public List<long> OutputQueue { get; set; }
        public List<long> InputQueue { get; set; }
        public List<string> Instructions { get; set; }
        public int InstructionPointer { get; set; }
        public Dictionary<char, long> Registers { get; set; }

        public DuetProgram(string input)
        {
            Instructions = input.Split('\n').ToList();
            OutputQueue = new List<long>();
            InputQueue = new List<long>();
            InstructionPointer = 0;
            Registers = new Dictionary<char, long>();

            for (var c = 'a'; c <= 'z'; c++)
            {
                Registers.Add(c, 0);
            }
        }

        private long GetValue(string value)
        {
            if (Registers.ContainsKey(value[0]))
            {
                return Registers[value[0]];
            }

            return long.Parse(value);
        }

        public void Execute()
        {
            while (true)
            {
                if (InstructionPointer >= Instructions.Count)
                {
                    return;
                }

                var instruction = Instructions[InstructionPointer++];
                var instructionWords = instruction.Split(' ').ToList();

                var command = instruction.Split(' ').First();
                var register = default(char);
                var value = string.Empty;

                switch (command)
                {
                    case "snd":
                        value = instructionWords[1];

                        OutputQueue.Add(GetValue(value));
                        break;
                    case "set":
                        register = instructionWords[1][0];
                        value = instructionWords[2];

                        Registers[register] = GetValue(value);
                        break;
                    case "add":
                        register = instructionWords[1][0];
                        value = instructionWords[2];

                        Registers[register] += GetValue(value);
                        break;
                    case "mul":
                        register = instructionWords[1][0];
                        value = instructionWords[2];

                        Registers[register] *= GetValue(value);
                        break;
                    case "mod":
                        register = instructionWords[1][0];
                        value = instructionWords[2];

                        Registers[register] %= GetValue(value);
                        break;
                    case "rcv":
                        register = instructionWords[1][0];

                        if (InputQueue.Count > 0)
                        {
                            Registers[register] = InputQueue.First();
                            InputQueue.RemoveAt(0);
                        }
                        else
                        {
                            InstructionPointer--;
                            return;
                        }

                        break;
                    case "jgz":
                        value = instructionWords[1];

                        if (GetValue(value) > 0)
                        {
                            var jumpCount = GetValue(instructionWords[2]);

                            InstructionPointer--;
                            InstructionPointer += (int)jumpCount;
                        }

                        break;
                    default:
                        throw new Exception();
                }
            }
        }
    }
}
