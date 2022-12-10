using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day10 : IDays
    {
        struct Cycle
        {
            public int Cycles { get; set; }
            public int Value { get; set; }
            public Cycle(int cycles, int value)
            {
                this.Cycles = cycles;
                this.Value = value;
            }
        }
        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input10.txt");
            int cycle = 1;
            int number = 1;
            int result = 0;
            List<Cycle> cycles = new List<Cycle>();
            List<int> strength = new List<int> { 20, 60, 100, 140, 180, 220};
            cycles.Add(new Cycle(0, number));

            foreach (string line in lines)
            {
                if (line.StartsWith("addx"))
                {
                    cycle++;
                    cycles.Add(new Cycle(cycle, number));
                    cycle++;
                    number += Int32.Parse(line.Remove(0, 5));
                    cycles.Add(new Cycle(cycle, number));
                }
                else
                {
                    cycle++;
                    cycles.Add(new Cycle(cycle, number));
                }
            }

            foreach(Cycle x in cycles)
            {
                if (strength.Contains(x.Cycles))
                {
                    result += x.Value * x.Cycles;
                }
            }

            Console.WriteLine(result);
        }

        public void PartTwo()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input10.txt"); int cycle = 1;
            int number = 1;
            int result = 0;
            List<Cycle> cycles = new List<Cycle>();
            List<int> strength = new List<int> { 20, 60, 100, 140, 180, 220 };
            cycles.Add(new Cycle(0, number));

            foreach (string line in lines)
            {
                if (line.StartsWith("addx"))
                {
                    cycle++;
                    cycles.Add(new Cycle(cycle, number));
                    cycle++;
                    number += Int32.Parse(line.Remove(0, 5));
                    cycles.Add(new Cycle(cycle, number));
                }
                else
                {
                    cycle++;
                    cycles.Add(new Cycle(cycle, number));
                }
            }

            int j = 0;
            for(int i = 0; i < cycles.Count; i++)
            {
                if(i % 40 == 0)
                {
                    Console.WriteLine();
                    j = 0;
                }
                if (j >= cycles[i].Value - 1 && j <= cycles[i].Value + 1)
                {
                    Console.Write("#");
                    j++;
                }
                else
                {
                    Console.Write(".");
                    j++;
                }
            }

            Console.WriteLine(result);
        }
    }
}
