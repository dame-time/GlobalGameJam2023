using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.FileSystem;
using Core;
using System.Linq;

namespace Generator.Terrain
{
    [System.Serializable]
    public struct Tile
    {
        public GameObject tile;
        public string key;
    }

    public struct TileStatus
    {
        public bool isBusy;
        public GameObject tile;
    }

    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] private List<Tile> tiles;

        private GameManager gameManager;

        private List<TileStatus> generatedTiles;

        public List<TileStatus> GeneratedTiles => generatedTiles;

        private void Start() {
            gameManager = FindObjectOfType<GameManager>();

            GenerateTerrain();
        }

        public void GenerateTerrain() 
        {
            generatedTiles = new List<TileStatus>();

            var fileContent = FileSystemReader.ReadFile(FileSystemReader.GetPathFromFolderDepth(FindObjectOfType<GameManager>().CurrentLevel) + "levelConfiguration.txt");
            if (fileContent.Equals(string.Empty))
                fileContent = gameManager.DefaultPaths[Random.Range(0, gameManager.DefaultPaths.Count)];

            var path = fileContent.ToCharArray().ToList();

            for (int i = 0; i < path.Count; i++)
            {
                var generatedTile = Instantiate(tiles.Where(e => e.key.Contains(path[i])).ToList().First().tile, new Vector3(i * 5, 0, 0), Quaternion.identity);
                generatedTiles.Add(new TileStatus() { isBusy = false, tile = generatedTile });
            }

            gameManager.LoadPlayer();
        }
    }   
}
