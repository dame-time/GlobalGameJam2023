using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Core.FileSystem
{
    public sealed class FileSystemReader
    {
        public static string ApplicationPath 
        {
            get 
            {
                var path = Application.dataPath;
                var newPath = string.Empty;

                if (Application.platform == RuntimePlatform.OSXPlayer)
                {
                    var splittedPath = path.Split('/');
                    for (var i = 0; i < splittedPath.Length - 2; i++)
                        newPath += splittedPath[i] + "/";
                }
                else if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    var splittedPath = path.Split('\\');
                    for (var i = 0; i < splittedPath.Length - 1; i++)
                        newPath += splittedPath[i] + "\\";
                }
                else {
                    newPath = Application.dataPath;
                }

                Debug.Log(newPath);

                return newPath;
            }
        }

        public static List<string> GetFolderStructure() {
            var folderStructure = new List<string>();
            var folders = ApplicationPath.Split(Path.DirectorySeparatorChar).ToList();

            foreach (var folder in folders)
                folderStructure.Add(folder);

            folderStructure.Reverse();

            return folderStructure;
        }

        public static List<string> GetFilesInFolder(string folderPath) {
            var files = new List<string>();
            var filesInFolder = System.IO.Directory.GetFiles(folderPath);

            foreach (var file in filesInFolder)
                files.Add(file);

            return files;
        }

        public static string GetPathFromFolderDepth(int depth) {
            var folderStructure = GetFolderStructure();
            folderStructure.Reverse();
            
            var folderPath = string.Empty;

            for (var i = 0; i < folderStructure.Count - depth; i++)
                folderPath += folderStructure[i] + Path.DirectorySeparatorChar;

            return folderPath;
        }

        public static string ReadFile(string filePath) {
            var fileContent = string.Empty;

            try
            {
                fileContent = System.IO.File.ReadAllText(filePath);
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

            return fileContent;
        }
    }
}
