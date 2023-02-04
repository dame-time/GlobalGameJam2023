using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.FileSystem;
using Core;
using System.Linq;
using Generator.Terrain;

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

        private TerrainGenerator terrainGenerator;

        private Dictionary<string, Vector3> busyPositions;

        private void Start() 
        {
            terrainGenerator = FindObjectOfType<TerrainGenerator>();
            busyPositions = new Dictionary<string, Vector3>();

            GenerateObstacles();
        }

        public void GenerateObstacles() 
        {
            var folder = FileSystemReader.GetPathFromFolderDepth(FindObjectOfType<GameManager>().CurrentLevel);
            var filesInFolder = FileSystemReader.GetFilesInFolder(folder);

            foreach (var file in filesInFolder) 
            {
                var splittedFile = file.Split(".");
                
                var obstacleHolder = PickRandomTileFromTerrain();
                // Instantiate(obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().First().obstacle, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }

        private GameObject PickRandomTileFromTerrain() => terrainGenerator.GeneratedTiles[Random.Range(0, terrainGenerator.GeneratedTiles.Count)];
    
        private Vector3 ComputeObstaclePosition(Tile tile) 
        {
            var tilePosition = tile.tile.transform.position;
            var tileScale = tile.tile.transform.localScale;

            var obstacleOffset = new Vector3(
                                                Random.Range(-tileScale.x, tileScale.x), 
                                                Random.Range(-tileScale.y, tileScale.y),
                                                Random.Range(-tileScale.z, tileScale.z)
                                            );
            var obstaclePosition = new Vector3(tilePosition.x + tileScale.x / 2, tilePosition.y + tileScale.y / 2, tilePosition.z + tileScale.z / 2);

            return obstaclePosition;
        }
    }
}
