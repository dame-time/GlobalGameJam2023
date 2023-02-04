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

        [SerializeField] private float XObstacleOffset = 0.0f;
        [SerializeField] private float YObstacleOffset = 2.0f;
        [SerializeField] private float ZObstacleOffset = 0.0f;

        [SerializeField] private int seed = 1;

        private TerrainGenerator terrainGenerator;

        private Dictionary<string, Vector3> busyPositions;

        private void Start() 
        {
            terrainGenerator = GetComponent<TerrainGenerator>();
            busyPositions = new Dictionary<string, Vector3>();

            // Random.InitState(seed);

            GenerateObstacles();
        }

        public void GenerateObstacles() 
        {
            var folder = FileSystemReader.GetPathFromFolderDepth(FindObjectOfType<GameManager>().CurrentLevel);
            var filesInFolder = FileSystemReader.GetFilesInFolder(folder);

            for (int i = 0; i < terrainGenerator.GeneratedTiles.Count; i++)
            {
                if (i >= filesInFolder.Count)
                    break;
                    
                var file = filesInFolder[i];
                var splittedFile = file.Split(".");

                var freeTiles = terrainGenerator.GeneratedTiles.Where(e => !e.isBusy).ToList();
                if (freeTiles.Count == 0)
                    break;

                var randomTile = freeTiles[Random.Range(0, freeTiles.Count)];

                var obstacleHolder = randomTile.tile;
                var obstaclePosition = ComputeObstaclePosition(obstacleHolder);

                var obstacleIsContained = obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().Count > 0;
                if (obstacleIsContained)
                {
                    var obstacle = Instantiate(obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().First().obstacle, obstaclePosition, Quaternion.identity);
                    obstacle.transform.parent = obstacleHolder.transform;
                }
                else 
                {
                    Debug.Log("TODO: Generate default obstacle");
                }

                randomTile.isBusy = true;
            }
        }
    
        private Vector3 ComputeObstaclePosition(GameObject tile) 
        {
            var tilePosition = tile.transform.position;
            var tileScale = tile.transform.localScale;

            var obstaclePosition = new Vector3(tilePosition.x + XObstacleOffset, tileScale.y + YObstacleOffset, ZObstacleOffset);

            return obstaclePosition;
        }
    }
}
