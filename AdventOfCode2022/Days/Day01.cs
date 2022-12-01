using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day01 : IDays
    {
        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input01.txt");
            List<int> calories = new List<int>();
            int calorie = 0;

            foreach(string line in lines)
            {
                if(line == "")
                {
                    calories.Add(calorie);
                    calorie = 0;
                }
                else
                    calorie += Int32.Parse(line);
            }

            Console.WriteLine(calories.Max());
        }

        public void PartTwo()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input01.txt");
            List<int> calories = new List<int>();
            int calorie = 0;

            foreach (string line in lines)
            {
                if (line == "")
                {
                    calories.Add(calorie);
                    calorie = 0;
                }
                else
                    calorie += Int32.Parse(line);
            }

            int result = calories.OrderByDescending(x => x).Take(3).Sum();

            Console.WriteLine(result);
        }
    }
}
