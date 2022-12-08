using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day08 : IDays
    {
        public bool IsTreeVisible(int[,] trees, int y, int x)
        {
            int highestNumber = 0;

            //Check Row Left
            for (int i = 0; i < x; i++)
            {
                highestNumber = trees[y, i] > highestNumber ? trees[y, i] : highestNumber;
            }

            if (highestNumber < trees[y, x])
                return true;
            else
                highestNumber = 0;

            //Check Row Right
            for (int i = x + 1; i < trees.GetLength(1); i++)
            {
                highestNumber = trees[y, i] > highestNumber ? trees[y, i] : highestNumber;
            }

            if (highestNumber < trees[y, x])
                return true;
            else
                highestNumber = 0;

            //Check Column Top
            for (int i = 0; i < y; i++)
            {
                highestNumber = trees[i, x] > highestNumber ? trees[i, x] : highestNumber;
            }

            if (highestNumber < trees[y, x])
                return true;
            else
                highestNumber = 0;

            //Check Column Bottom
            for (int i = y + 1; i < trees.GetLength(0); i++)
            {
                highestNumber = trees[i, x] > highestNumber ? trees[i, x] : highestNumber;
            }

            if (highestNumber < trees[y, x])
                return true;
            else
                highestNumber = 0;

            return false;
        }

        public int CalcScenicScore(int[,] trees, int y, int x)
        {
            int result = 1;
            int number = 0;

            //Check Row Left
            for (int i = x - 1; i >= 0; i--)
            {
                number++;
                if (trees[y, x] <= trees[y, i])
                {
                    break;
                }
            }

            result *= number;
            number = 0;

            //Check Row Right
            for (int i = x + 1; i < trees.GetLength(1); i++)
            {
                number++;
                if (trees[y, x] <= trees[y, i])
                {
                    break;
                }
            }

            result *= number;
            number = 0;

            //Check Column Top
            for (int i = y - 1; i >= 0; i--)
            {
                number++;
                if (trees[y, x] <= trees[i, x])
                {
                    break;
                }
            }

            result *= number;
            number = 0;

            //Check Column Bottom
            for (int i = y + 1; i < trees.GetLength(0); i++)
            {
                number++;
                if (trees[y, x] <= trees[i, x])
                {
                    break;
                }
            }

            result *= number;

            return result;
        }

        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input08.txt");
            int[,] trees = new int[lines.Count(), lines[0].Length];
            int count = 0;
            
            for(int i = 0; i < lines.Count(); i++)
            {
                for(int j = 0; j < lines[i].Length; j++)
                {
                    trees[i, j] = Int32.Parse(lines[i].ElementAt(j).ToString());
                }
            }

            for (int i = 0; i < trees.GetLength(0); i++)
            {
                for (int j = 0; j < trees.GetLength(1); j++)
                {
                    if (i == 0 || i >= trees.GetLength(0) - 1 || j == 0 ||  j >= trees.GetLength(1) - 1)
                    {
                        count++;
                    }
                    else
                    {
                        if(IsTreeVisible(trees, i, j))
                        {
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine($"Result: {count}");
        }

        public void PartTwo()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input08.txt");
            int[,] trees = new int[lines.Count(), lines[0].Length];
            int scenicScore = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    trees[i, j] = Int32.Parse(lines[i].ElementAt(j).ToString());
                }
            }

            for (int i = 0; i < trees.GetLength(0); i++)
            {
                for (int j = 0; j < trees.GetLength(1); j++)
                {
                    if (!(i == 0 || i >= trees.GetLength(0) - 1 || j == 0 || j >= trees.GetLength(1) - 1))
                    {
                        int score = CalcScenicScore(trees, i, j);
                        scenicScore = scenicScore < score ? score : scenicScore;
                    }
                }
            }

            Console.WriteLine($"Result: {scenicScore}");
        }
    }
}
