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

                var index = Random.Range(0, freeTiles.Count);
                var randomTile = freeTiles[index];

                var obstacleHolder = randomTile.tile;
                var obstaclePosition = ComputeObstaclePosition(obstacleHolder);

                var obstacleIsContained = obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().Count > 0;
                if (obstacleIsContained)
                {
                    var obstacle = Instantiate(obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().First().obstacle, obstaclePosition, Quaternion.identity);
                    obstacle.transform.parent = obstacleHolder.transform;
                    obstacle.transform.localScale = obstacles.Where(e => e.key.Contains(splittedFile[splittedFile.Length - 1])).ToList().First().obstacle.transform.localScale;
                }
                else 
                {
                    Debug.Log(splittedFile[splittedFile.Length - 1]);
                }

                for (int k = 0; k < terrainGenerator.GeneratedTiles.Count; k++)
                {
                    var tile = terrainGenerator.GeneratedTiles[k];
                    if (tile.tile.transform.position == randomTile.tile.transform.position)
                    {
                        TileStatus t;
                        t.isBusy = true;
                        t.tile = randomTile.tile;
                        terrainGenerator.GeneratedTiles[k] = t;
                        break;
                    }
                }
            }
        }
    
        private Vector3 ComputeObstaclePosition(GameObject tile) 
        {
            var tilePosition = tile.transform.position;
            var tileScale = tile.transform.localScale;

            var obstaclePosition = new Vector3(tile.transform.position.x + XObstacleOffset, YObstacleOffset, ZObstacleOffset);

            return obstaclePosition;
        }
    }
}
