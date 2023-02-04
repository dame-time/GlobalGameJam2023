using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core 
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private int maxPathLength = 50;
        [SerializeField] private int minPathLength = 20;

        private List<string> folderStructure;
        private int currentLevel;
        private string currentFolder;

        private List<string> defaultPaths;

        public int CurrentLevel => currentLevel;
        public string CurrentFolder => currentFolder;
        public List<string> DefaultPaths => defaultPaths;

        private void Awake() 
        {
            folderStructure = FileSystem.FileSystemReader.GetFolderStructure();

            defaultPaths = new List<string>();
            PrecomputePathsWhereConfigDoesntExist();

            currentLevel = 0;
            currentFolder = folderStructure[currentLevel];

            GenerateAllConfigurationFiles();
        }

        private void PrecomputePathsWhereConfigDoesntExist()
        {
            var possiblePaths = UnityEngine.Random.Range(minPathLength, maxPathLength + 1);
            for (int i = 0; i < possiblePaths; i++)
            {
                var configuration = RandomStringGenerator.GenerateRandomString(UnityEngine.Random.Range(minPathLength, maxPathLength + 1));
                
                defaultPaths.Add(configuration);
            }
        }

        private void GenerateAllConfigurationFiles()
        {
            for (int i = 0; i < folderStructure.Count; i++)
            {
                var folderPath = FileSystem.FileSystemReader.GetPathFromFolderDepth(i);
                var pathConfiguration = RandomStringGenerator.GenerateRandomString(UnityEngine.Random.Range(minPathLength, maxPathLength + 1));
                
                try
                {
                    FileSystem.FileSystemWriter.WriteToFile(folderPath, pathConfiguration);
                }
                catch (Exception)
                {
                    break;
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
