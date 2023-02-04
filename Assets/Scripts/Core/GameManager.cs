using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core 
{
    public sealed class GameManager : MonoBehaviour
    {
        private List<string> folderStructure;
        private int currentLevel;
        private string currentFolder;

        public int CurrentLevel => currentLevel;
        public string CurrentFolder => currentFolder;

        private void Awake() 
        {
            folderStructure = FileSystem.FileSystemReader.GetFolderStructure();

            currentLevel = 0;
            currentFolder = folderStructure[currentLevel];

            GenerateAllConfigurationFiles();
        }

        private void GenerateAllConfigurationFiles()
        {
            for (int i = 0; i < folderStructure.Count; i++)
            {
                var folderPath = FileSystem.FileSystemReader.GetPathFromFolderDepth(i);

                try
                {
                    FileSystem.FileSystemWriter.WriteToFile(folderPath, "*AABBBCCCCAABB");
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }

        public void LoadNextLevel() 
        {
            if (currentLevel < folderStructure.Count - 1)
                currentLevel++;

            currentFolder = folderStructure[currentLevel];
            // Debug.Log(FileSystem.FileSystemReader.GetPathFromFolderDepth(currentLevel));
            // foreach (var file in FileSystem.FileSystemReader.GetFilesInFolder(FileSystem.FileSystemReader.GetPathFromFolderDepth(currentLevel)))
            //     Debug.Log(file);
        }
    }
}
