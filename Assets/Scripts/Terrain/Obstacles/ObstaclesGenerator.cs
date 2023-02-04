using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.FileSystem;
using Core;
using System.Linq;

namespace Generator.Obstacles
{
    [System.Serializable]
    public struct Obstacle
    {
        public GameObject obstacle;
        public string key;
    }

    public class ObstaclesGenerator : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> obstacles;

        private void Start() 
        {
            GenerateObstacles();
        }

        public void GenerateObstacles() 
        {
            var folder = FileSystemReader.GetPathFromFolderDepth(FindObjectOfType<GameManager>().CurrentLevel);
            var filesInFolder = FileSystemReader.GetFilesInFolder(folder);

            foreach (var file in filesInFolder) 
            {
                var splittedFile = file.Split(".");
                Instantiate(obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().First().obstacle, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}
