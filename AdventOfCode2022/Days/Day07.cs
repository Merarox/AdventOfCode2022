using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day07 : IDays 
    {
        struct Files
        {
            public string Name { get; set; }
            public int Size { get; set; }

            public Files(string name, int size)
            {
                this.Name = name;
                this.Size = size;
            }
        }
        public class Directory
        {
            public string Name { get; set; }
            List<Directory> directories = null;
            List<Files> files = null;
            Directory prevDirectory = null;
            public int sizecombined = 0;
            List<int> sizes = new List<int>();

            public Directory(string name)
            {
                this.Name = name;
            }
            public Directory(string name, Directory prevDirectory)
            {
                this.Name = name;
                this.prevDirectory = prevDirectory;
            }

            public Directory GetPrevDirectory()
            {
                return prevDirectory;
            }

            public void AddData(string name, int size)
            {
                if (files == null)
                {
                    files = new List<Files>();
                }
                files.Add(new Files(name, size));
            }

            public void AddDirectory(string name)
            {
                if(directories == null)
                {
                    directories = new List<Directory>();
                }
                directories.Add(new Directory(name, this));
            }

            public int SetFileSizeList()
            {
                int sum = 0;
                if (directories != null)
                {
                    foreach (Directory directory in directories)
                    {
                        sum += directory.SetFileSizeList();
                    }
                }
                if(files != null && prevDirectory != null)
                {
                    int value = sum + files.Sum(f => f.Size);
                    sizes.Add(value);
                    sizes.ForEach(x => prevDirectory.sizes.Add(x));
                    return value;
                }
                else
                {
                    if (prevDirectory != null)
                    {
                        sizes.Add(sum);
                        sizes.ForEach(x => prevDirectory.sizes.Add(x));
                    }
                    else
                    {
                        if(files != null)
                        {
                            sizecombined = sum + files.Sum(f => f.Size);
                        }
                        else
                        {
                            sizecombined = sum;
                        }
                    }
                }

                return sum;
            }

            public List<int> GetSizes()
            {
                return sizes;
            }

            public Directory getDirectory(string name)
            {
                return directories.Find(x => x.Name == name);
            }
        }

        public void PartOne()
        {
            string[] lines = File.ReadAllLines(@"../../../Inputs/input07.txt");
            Directory headDirectory = new Directory("/");
            Directory currentDirectory = headDirectory;
            foreach (string line in lines)
            {
                if(line.StartsWith("$ cd"))
                {
                    string dir = line.Remove(0, 5);
                    if (dir != "/")
                    {
                        if (dir == "..")
                            currentDirectory = currentDirectory.GetPrevDirectory();
                        else
                        {
                            currentDirectory = currentDirectory.getDirectory(line.Remove(0, 5));
                        }
                        
                    }
                }
                else if(line.StartsWith("$ ls"))
                {
                    //ignore
                }
                else if(line.StartsWith("dir"))
                {
                    currentDirectory.AddDirectory(line.Remove(0, 4));
                }
                else
                {
                    string[] file = line.Split(" ");
                    currentDirectory.AddData(file[1], Int32.Parse(file[0]));
                }
            }
            headDirectory.SetFileSizeList();
            int result = headDirectory.GetSizes().Where(x => x <= 100000).Sum();
            Console.WriteLine($"Result: {result}");
        }

        public void PartTwo()
        {
            const int SPACE = 70000000;
            const int UPDATESPACE = 30000000;
            string[] lines = File.ReadAllLines(@"../../../Inputs/input07.txt");
            Directory headDirectory = new Directory("/");
            Directory currentDirectory = headDirectory;
            foreach (string line in lines)
            {
                if (line.StartsWith("$ cd"))
                {
                    string dir = line.Remove(0, 5);
                    if (dir != "/")
                    {
                        if (dir == "..")
                            currentDirectory = currentDirectory.GetPrevDirectory();
                        else
                        {
                            currentDirectory = currentDirectory.getDirectory(line.Remove(0, 5));
                        }

                    }
                }
                else if (line.StartsWith("$ ls"))
                {
                    //ignore
                }
                else if (line.StartsWith("dir"))
                {
                    currentDirectory.AddDirectory(line.Remove(0, 4));
                }
                else
                {
                    string[] file = line.Split(" ");
                    currentDirectory.AddData(file[1], Int32.Parse(file[0]));
                }
            }

            headDirectory.SetFileSizeList();
            int freeSpace = SPACE - headDirectory.sizecombined;
            int neededSpace = UPDATESPACE - freeSpace;
            int closest = headDirectory.GetSizes().Where(x => x > neededSpace).First();
            Console.WriteLine($"Result: {closest}");
        }
    }
}
