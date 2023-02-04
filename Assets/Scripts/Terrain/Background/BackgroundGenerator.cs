using System.Collections.Generic;
using UnityEngine;

namespace Generator.Background
{
    public class BackgroundGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tilePrefabs;

        [SerializeField] private int waveSize = 10;
        [SerializeField] private int gridSize = 10;

        private void Start() {
            GenerateBackground();
        }

        public void GenerateBackground()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    // Choose a random tile prefab
                    GameObject selectedPrefab = ChooseRandomTilePrefab(tilePrefabs);

                    // Calculate the position of the tile
                    Vector3 tilePosition = new Vector3(i * 6 * Random.Range(1, 4), j * 2 * Random.Range(0, 2), 20);

                    // Instantiate the selected tile prefab
                    GameObject tile = Instantiate(selectedPrefab, tilePosition, Quaternion.identity);

                    // Apply wave collapse effect to the tile
                    WaveCollapse(tile.transform);
                }
            }
        }

        private GameObject ChooseRandomTilePrefab(List<GameObject> tiles)
        {
            int randomIndex = Random.Range(0, tiles.Count);
            return tiles[randomIndex];
        }

        private void WaveCollapse(Transform tileTransform)
        {
            // Generate a random wave size
            int randomWaveY = Random.Range(1, waveSize);
            int randomWaveX = Random.Range(1, waveSize);

            // Apply wave collapse effect to the tile
            tileTransform.localScale = new Vector3(tileTransform.localScale.x + randomWaveX, tileTransform.localScale.y + randomWaveY, tileTransform.localScale.z);
        }
    }
}

