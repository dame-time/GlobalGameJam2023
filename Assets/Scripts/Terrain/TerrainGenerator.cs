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

    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] private List<Tile> tiles;

        private void Start() {
            GenerateTerrain();
        }

        public void GenerateTerrain() {
            var fileContent = FileSystemReader.ReadFile(FileSystemReader.GetPathFromFolderDepth(FindObjectOfType<GameManager>().CurrentLevel) + "levelConfiguration.txt");
            var path = fileContent.ToCharArray().ToList();

            for (int i = 0; i < path.Count; i++)
                Instantiate(tiles.Where(e => e.key.Contains(path[i])).ToList().First().tile, new Vector3(i * 5, 0, 0), Quaternion.identity);
        }
    }   
}
