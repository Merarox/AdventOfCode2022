using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AdventOfCode2022.Days.Day07;
using static AdventOfCode2022.Days.Day11;

namespace AdventOfCode2022.Days
{
    public class Day11 : IDays
    {
        public class Monkey
        {
            public long Inspects;
            public List<long> Items = new List<long>();
            public int Divisible { get; set; }
            public bool Oper { get; set; }
            public int Number { get; set; }
            public int TrueCondition { get; set; }
            public int FalseCondition { get; set; }

            public long Operation()
            {
                //oper(false): + | oper(true): *
                Inspects++;
                long number = Number == -1 ? Items.ElementAt(0) : Number;
                long newNumber = Oper ? Items.First() * number : Items.First() + number;
                newNumber = Convert.ToInt32(Math.Floor((decimal)newNumber/3));
                Items.RemoveAt(0);
                return newNumber;
            }
            public long OperationDontWorry(long factor)
            {
                //oper(false): + | oper(true): *
                long newNumber = 0;
                Inspects++;
                long number = Number == -1 ? Items.ElementAt(0) : Number;
                newNumber = Oper ? Items.First() * number : Items.First() + number;
                newNumber = newNumber % factor;
                Items.RemoveAt(0);
                return newNumber;
            }
        }

        public List<Monkey> ParseInput(string[] input)
        {
            List<Monkey> monkeys = new List<Monkey>();
            Monkey curentMonkey;

            for (int i = 0; i < input.Length - 1; i += 7)
            {
                curentMonkey = new Monkey();
                string[] items = input[i + 1].Remove(0, 18).Replace(" ", string.Empty).Split(',');
                foreach (string item in items)
                {
                    curentMonkey.Items.Add(Int32.Parse(item));
                }
                if (input[i + 2].ElementAt(23).ToString() == "*")
                    curentMonkey.Oper = true;
                else
                    curentMonkey.Oper = false;

                if (input[i + 2].Remove(0, 25).ToString() == "old")
                    curentMonkey.Number = -1;
                else
                    curentMonkey.Number = Int32.Parse(input[i + 2].Remove(0, 25));

                curentMonkey.Divisible = Int32.Parse(input[i + 3].Remove(0, 20));
                curentMonkey.TrueCondition = Int32.Parse(input[i + 4].Remove(0, 28));
                curentMonkey.FalseCondition = Int32.Parse(input[i + 5].Remove(0, 29));

                monkeys.Add(curentMonkey);
            }
            return monkeys;
        }

        public void PrintItems(ref List<Monkey> monkeys)
        {
            for (int j = 0; j < monkeys.Count; j++)
            {
                Console.Write($"Monkey {j}: ");
                monkeys[j].Items.ForEach(x => Console.Write(x + ", "));
                Console.WriteLine();
            }
        }

        public void PrintCount(ref List<Monkey> monkeys)
        {
            for (int i = 0; i < monkeys.Count; i++)
            {
                Console.WriteLine($"Monkey {i} inspected items {monkeys[i].Inspects} times.");
            }
        }

        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input11.txt");
            List<Monkey> monkeys = ParseInput(lines);

            for(int i = 0; i < 20; i++)
            {
                foreach(Monkey monkey in monkeys)
                {
                    int count = monkey.Items.Count();
                    for (int j = 0 ; j < count; j++)
                    {
                        long newNumber = monkey.Operation();
                        //Console.WriteLine($"newNumber: {newNumber} Divisible: {monkey.Divisible}");
                        if (newNumber % monkey.Divisible == 0)
                        {
                            monkeys[monkey.TrueCondition].Items.Add(newNumber);
                        }
                        else
                        {
                            monkeys[monkey.FalseCondition].Items.Add(newNumber);
                        }
                    }
                }
            }
            Monkey[] number = monkeys.OrderByDescending(x => x.Inspects).Take(2).ToArray();
            Console.WriteLine($"Result: {number.ElementAt(0).Inspects * number.ElementAt(1).Inspects}");
        }

        public void PartTwo()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input11.txt");
            List<Monkey> monkeys = ParseInput(lines);

            long factor = 1;
            monkeys.ForEach(x => factor *= x.Divisible);

            for (int i = 0; i < 10000; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    int count = monkey.Items.Count();
                    for (int j = 0; j < count; j++)
                    {
                        long newNumber = monkey.OperationDontWorry(factor);
                        if (newNumber % monkey.Divisible == 0)
                        {
                            monkeys[monkey.TrueCondition].Items.Add(newNumber);
                        }
                        else
                        {
                            monkeys[monkey.FalseCondition].Items.Add(newNumber);
                        }
                    }
                }
            }
            Monkey[] number = monkeys.OrderByDescending(x => x.Inspects).Take(2).ToArray();
            Console.WriteLine($"Result: {number.ElementAt(0).Inspects * number.ElementAt(1).Inspects}");
        }
    }
}
