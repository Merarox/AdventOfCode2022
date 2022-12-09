using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day09 : IDays
    {
        public struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Position(int y, int x)
            {
                this.Y = y;
                this.X = x;
            }
        }

        public class Board
        {
            List<Position> ropes = new List<Position>();
            List<Position> visitedPositions = new List<Position>();

            public Board(int tailsCount)
            {
                for(int i = 0; i <= tailsCount; i++)
                {
                    this.ropes.Add(new Position(0, 0));
                }
                //Add 0,0 for the beginning Tail Position
                visitedPositions.Add(new Position(0, 0));
            }

            public void Move(int y, int x)
            {
                //Column Movement
                for(int i = 0; i < Math.Abs(y); i++)
                {
                    for(int j = 0; j < ropes.Count; j++)
                    {
                        if (j == 0)
                        {                        
                            //Head
                            int newY = y > 0 ? ropes[j].Y + 1 : ropes[j].Y - 1;
                            ropes[j] = new Position(newY, ropes[j].X);
                        }
                        else
                        {
                            CalcTailPosition(j);
                        }
                    }
                }

                //Row Movement
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    for (int j = 0; j < ropes.Count; j++)
                    {
                        if (j == 0)
                        {
                            //Head
                            int newX = x > 0 ? ropes[j].X + 1 : ropes[j].X - 1;
                            ropes[j] = new Position(ropes[j].Y, newX);
                        }
                        else
                        {
                            CalcTailPosition(j);
                        }
                    }
                }
            }

            public void CalcTailPosition(int element)
            {
                //Tail Diagonally
                if ((Math.Abs(ropes[element - 1].Y - ropes[element].Y) > 1 && Math.Abs(ropes[element - 1].X - ropes[element].X) >= 1) || Math.Abs(ropes[element - 1].Y - ropes[element].Y) >= 1 && Math.Abs(ropes[element - 1].X - ropes[element].X) > 1)
                {
                    int newX = ropes[element - 1].X > ropes[element].X ? ropes[element].X + 1 : ropes[element].X - 1;
                    int newY = ropes[element - 1].Y > ropes[element].Y ? ropes[element].Y + 1 : ropes[element].Y - 1;
                    ropes[element] = new Position(newY, newX);
                    if (element == ropes.Count - 1)
                        visitedPositions.Add(ropes[element]);
                }
                //Tail Column Check
                else if (Math.Abs(ropes[element - 1].Y - ropes[element].Y) > 1)
                {
                    int newY = ropes[element - 1].Y > ropes[element].Y ? ropes[element].Y + 1 : ropes[element].Y - 1;
                    ropes[element] = new Position(newY, ropes[element].X);
                    if (element == ropes.Count - 1)
                        visitedPositions.Add(ropes[element]);
                }
                //Tail Row Check
                else if (Math.Abs(ropes[element - 1].X - ropes[element].X) > 1)
                {

                    int newX = ropes[element - 1].X > ropes[element].X ? ropes[element].X + 1 : ropes[element].X - 1;
                    ropes[element] = new Position(ropes[element].Y, newX);
                    if (element == ropes.Count - 1)
                        visitedPositions.Add(ropes[element]);
                }
            }

            public int CountPositions()
            {
                return visitedPositions.GroupBy(x => new {x.Y, x.X}).Distinct().Count();
            }
        }

        public void ProcessInput(ref Board board, string[] lines)
        {
            foreach (string line in lines)
            {
                string[] input = line.Split(' ');
                switch (input[0])
                {
                    case "U": board.Move(-Int32.Parse(input[1]), 0); break;
                    case "D": board.Move(Int32.Parse(input[1]), 0); break;
                    case "L": board.Move(0, -Int32.Parse(input[1])); break;
                    case "R": board.Move(0, Int32.Parse(input[1])); break;
                    default: break;
                }
            }
        }

        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input09.txt");
            Board board = new Board(1);
            ProcessInput(ref board, lines);
            Console.WriteLine(board.CountPositions());
        }

        public void PartTwo()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input09.txt");
            Board board = new Board(9);
            ProcessInput(ref board, lines);
            Console.WriteLine(board.CountPositions());
        }
    }
}