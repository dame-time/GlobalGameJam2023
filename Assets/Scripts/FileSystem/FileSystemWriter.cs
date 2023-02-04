using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.FileSystem
{
    public sealed class FileSystemWriter
    {
        public static void WriteToFile(string filePath, string content) 
        {
            string endPath = filePath + Path.DirectorySeparatorChar + "levelConfiguration.txt";

            // Check if file exists and if it does, delete it then create a new one with the name and content provided
            if (System.IO.File.Exists(endPath))
                return;

            System.IO.File.WriteAllText(endPath, content);
        }
    }
}
