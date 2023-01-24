using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public static List<string> Solve(List<string> inputStrings)
        {
            Directory rootDirectory = new Directory("dir");

            foreach (string line in inputStrings)
            {
                string[] directories = line.Split('\\');
                Directory currentDir = rootDirectory;

                foreach (string directory in directories)
                {
                    currentDir = currentDir.FindDirectory(directory);
                }
            }

            return rootDirectory.OrderDirectories(0, new List<string>());
        }
    }

    public class Directory
    {
        public string DirectoryName;         //Словарь директорий, которые после текущей
        public Dictionary<string, Directory> DirectoriesDict = new Dictionary<string, Directory>();

        public Directory(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public Directory FindDirectory(string directory)
        {
            if (DirectoriesDict.TryGetValue(directory, out Directory resDirectory))
            {
                return resDirectory;
            }

            DirectoriesDict[directory] = new Directory(directory);

            return DirectoriesDict[directory];
        }

        public List<string> OrderDirectories(int spaces, List<string> directoriesList)
        {
            if (DirectoryName != "dir")
            {
                directoriesList.Add(new string(' ', spaces) + DirectoryName);
                spaces++;
            }

            foreach (Directory directory in DirectoriesDict.Values.OrderBy(d => d.DirectoryName, StringComparer.Ordinal))
            {
                directoriesList = directory.OrderDirectories(spaces, directoriesList);
            }

            return directoriesList;
        }
    }
}