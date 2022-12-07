using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day07 : Day
    {
        Dictionary<string, List<object>> allFolders = new();

        record Folder(string path, List<object> content);
        record File(string name, long size);

        public override dynamic PartOne()
        {
            InitFolders();

            long sum = 0;

            foreach (string path in allFolders.Keys)
            {
                long size = GetFullFolderSizeByPath(path);
                if (size <= 100000)
                    sum += size;
            }

            return sum;
        }

        public override dynamic PartTwo()
        {
            long totalSpaceAvailable = 70000000;
            long spaceNeeded = 30000000;
            long spaceAvailable = totalSpaceAvailable - GetFullFolderSizeByPath("/");

            long minSizeToDelete = long.MaxValue;

            foreach (string folder in allFolders.Keys)
            {
                long folderSize = GetFullFolderSizeByPath(folder);
                if (spaceAvailable + folderSize >= spaceNeeded)
                {
                    if (folderSize < minSizeToDelete)
                        minSizeToDelete = folderSize;
                }
            }
            return minSizeToDelete;
        }

        private long GetFullFolderSizeByPath(string path)
        {
            long size = 0;

            string[] folders = allFolders.Keys.Where(x => x.StartsWith(path)).ToArray();

            foreach (string folder in folders)
            {
                foreach (File file in allFolders[folder].Where(x => x is File))
                    size += file.size;
            }

            return size;
        }

        private void InitFolders()
        {
            string currentPath = "/";
            allFolders = new();
            allFolders.Add("/", new());

            foreach (string line in input.Split(Environment.NewLine)[1..])
            {
                if (line.StartsWith("$"))
                {
                    Regex commandMatcher = new(@"\$ (\S+) ?(\S+)");
                    MatchCollection matches = commandMatcher.Matches(line);
                    string command = matches[0].Groups[1].ToString();
                    if (command == "cd")
                    {
                        string destination = matches[0].Groups[2].ToString();

                        if (destination == "..")
                        {
                            currentPath = currentPath.Substring(0, currentPath.Length - currentPath.Split("/")[^2].Length - 1);
                        }
                        else
                        {
                            currentPath = currentPath + destination + "/";
                            allFolders.Add(currentPath, new());
                        }

                    }
                    else //ls
                    {

                    }
                }
                else
                {
                    if (!line.StartsWith("dir"))
                        allFolders[currentPath].Add(new File(line.Split(" ")[1], long.Parse(line.Split(" ")[0])));
                    else
                        allFolders[currentPath].Add(new Folder(currentPath + line.Split(" ")[1] + "/", new()));
                }
            }
        }

    }
}
