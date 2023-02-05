using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.FileSystem;
using Core;
using System.Linq;
using Core.SceneManagement;

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

        private Vector3 startTilePosition;

        private GameManager gameManager;

        private List<TileStatus> generatedTiles;

        public List<TileStatus> GeneratedTiles {
            get { return generatedTiles; }
            set { generatedTiles = value; }
        }

        public Vector3 StartTilePosition {
            get { return startTilePosition; }
        }

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

            if (path.Count > 0 && path[0] != '*')
                path.Insert(0, '*');

            if (path.Count > 0 && path[path.Count - 1] != '!')
                path.Add('!');

            float cachedXStep = 0.0f;
            for (int i = 0; i < path.Count; i++)
            {
                var tileToGenerate = tiles.Where(e => e.key.Contains(path[i])).ToList().First().tile;

                var xStep = 5.0f;
                if (tileToGenerate.GetComponent<BoxCollider>())
                    xStep = tileToGenerate.GetComponent<BoxCollider>().size.x;

                var generatedTile = Instantiate(tileToGenerate, new Vector3(i * xStep, 0, 0), tileToGenerate.transform.rotation);
                if (generatedTile.GetComponent<BoxCollider>())
                    generatedTile.transform.position = new Vector3(cachedXStep + (xStep * generatedTile.transform.localScale.x)/2, tileToGenerate.transform.position.y, tileToGenerate.transform.position.z);

                if (i == 0)
                    GameManager.Instance.PlayerStartingPosition = generatedTile.transform.position;

                cachedXStep += xStep * generatedTile.transform.localScale.x;
                generatedTiles.Add(new TileStatus() { isBusy = false, tile = generatedTile });
            }

            gameManager.LoadPlayer();
        }
    }   
}
