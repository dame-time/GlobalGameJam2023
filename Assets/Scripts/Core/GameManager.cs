using System;
using System.Collections.Generic;
using Cinemachine;
using Core.SceneManagement;
using Core.UserInterface;
using Patterns;
using UnityEngine;
using UserInterface.Buttons;
using Utils;

namespace Core 
{
    public sealed class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int maxPathLength = 50;
        [SerializeField] private int minPathLength = 20;

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector3 playerStartingPosition;

        [SerializeField] private float cinemachineXOffset = 1.5f;
        [SerializeField] private float cinemachineYOffset = 1.5f;
        
        [SerializeField] private PauseMenuButtons pauseMenuButtons;

        private UI uiObject;

        private List<string> folderStructure;
        private int currentLevel;
        private string currentFolder;

        private List<string> defaultPaths;

        private GameObject player;
        private GameObject playerCamera;

        private CinemachineVirtualCamera virtualCamera;


        public int CurrentLevel => currentLevel;
        public string CurrentFolder => currentFolder;
        public List<string> DefaultPaths => defaultPaths;

        public GameObject PlayerPrefab => playerPrefab;
        public Vector3 PlayerStartingPosition => playerStartingPosition;

        public GameObject Player => player;

        private void Awake() 
        {
            if (FindObjectsOfType(typeof(GameManager)).Length > 1)
            {
                Destroy(this.gameObject);
                return;
            }

            folderStructure = FileSystem.FileSystemReader.GetFolderStructure();

            defaultPaths = new List<string>();
            PrecomputePathsWhereConfigDoesntExist();

            currentLevel = 0;
            currentFolder = folderStructure[currentLevel];

            GenerateAllConfigurationFiles();

            uiObject = FindObjectOfType<UI>();
            uiObject.SetPathText(FileSystem.FileSystemReader.GetPathFromFolderDepth(currentLevel));
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuButtons.gameObject.SetActive(!pauseMenuButtons.gameObject.activeSelf);
                if (pauseMenuButtons.gameObject.activeSelf)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            }
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
            else
            {
                PlayWinScreen();
                Debug.Log("You won!");
                return;
            }

            currentFolder = folderStructure[currentLevel];

            SceneLoader.LoadNewScene();

            uiObject.SetPathText(FileSystem.FileSystemReader.GetPathFromFolderDepth(currentLevel));

            Debug.Log("Current Folder: " + currentFolder);
            // Debug.Log(FileSystem.FileSystemReader.GetPathFromFolderDepth(currentLevel));
            // foreach (var file in FileSystem.FileSystemReader.GetFilesInFolder(FileSystem.FileSystemReader.GetPathFromFolderDepth(currentLevel)))
            //     Debug.Log(file);
        }

        public void LoadPlayer() 
        {
            player = Instantiate(playerPrefab, playerStartingPosition, Quaternion.identity);
            playerCamera = new GameObject("Player Camera");
            playerCamera.transform.position = Camera.main.transform.position;

            virtualCamera = playerCamera.AddComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = player.transform;
            virtualCamera.AddCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(cinemachineXOffset, cinemachineYOffset, 0);
            virtualCamera.m_Lens.FieldOfView = 49;
        }

        public void GameOver()
        {
            player.transform.position = playerStartingPosition;
        }

        public void PlayWinScreen()
        {
            // SceneLoader.LoadWinScreen();
        }
    }
}
