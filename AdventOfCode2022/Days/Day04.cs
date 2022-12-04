using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day04 : IDays
    {
        struct Pairs
        {
            public int fromA { get; set; }
            public int toA { get; set; }
            public int fromB { get; set; }
            public int toB { get; set; }

            public Pairs(int fromA, int toA, int fromB, int toB)
            {
                this.fromA = fromA;
                this.toA = toA;
                this.fromB = fromB;
                this.toB = toB;
            }
        }

        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input04.txt");
            List<Pairs> pairs = new List<Pairs>();
            int result = 0;

            foreach (string line in lines)
            {
                string[] elements = line.Split(new char[] {',', '-'});
                pairs.Add(new Pairs(int.Parse(elements[0]), int.Parse(elements[1]), int.Parse(elements[2]), int.Parse(elements[3])));
            }

            foreach (Pairs pair in pairs)
            {
                if((pair.fromA <= pair.fromB && pair.toA >= pair.toB) || (pair.fromA >= pair.fromB && pair.toA <= pair.toB))
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }

        public void PartTwo()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input04.txt");
            List<Pairs> pairs = new List<Pairs>();
            int result = 0;

            foreach (string line in lines)
            {
                string[] elements = line.Split(new char[] { ',', '-' });
                pairs.Add(new Pairs(int.Parse(elements[0]), int.Parse(elements[1]), int.Parse(elements[2]), int.Parse(elements[3])));
            }

            foreach (Pairs pair in pairs)
            {
                if((pair.fromA <= pair.fromB && pair.toA >= pair.fromB) || (pair.fromB <= pair.fromA && pair.toB >= pair.fromA))
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }
    }
}
