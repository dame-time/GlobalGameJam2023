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

        private GameManager gameManager;

        private List<TileStatus> generatedTiles;

        public List<TileStatus> GeneratedTiles {
            get { return generatedTiles; }
            set { generatedTiles = value; }
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

            for (int i = 0; i < path.Count; i++)
            {
                var tileToGenerate = tiles.Where(e => e.key.Contains(path[i])).ToList().First().tile;

                var xStep = 5.0f;
                if (tileToGenerate.GetComponent<BoxCollider>())
                    xStep = tileToGenerate.GetComponent<BoxCollider>().size.x;

                //Debug.Log(xStep);

                var generatedTile = Instantiate(tileToGenerate, new Vector3(i * xStep, 0, 0), tileToGenerate.transform.rotation);
                if (generatedTile.GetComponent<BoxCollider>())
                    generatedTile.transform.position = new Vector3(i * generatedTile.GetComponent<BoxCollider>().size.x * generatedTile.transform.localScale.x, 0, 0);
                generatedTiles.Add(new TileStatus() { isBusy = false, tile = generatedTile });
            }

            gameManager.LoadPlayer();
        }
    }   
}
