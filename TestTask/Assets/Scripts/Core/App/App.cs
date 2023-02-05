using System;

using UnityEngine;

namespace IgnSDK
{
    public sealed class App : MonoBehaviour
    {
        // Core modules

        public static Scene Scene;
        public static CoroutineManager CoroutineManager { get; private set; }

        // Properties

        public static UIManager UIManager => Scene.UIManager;
        public static bool Paused => Time.timeScale == 0;

        // Managers

        public static CinemachineManager CinemachineManager { get; private set; }
        public static SoundManager SoundManager { get; private set; }
        public static PostProcessingManager PostProcessingManager { get; private set; }

        public static DatabaseConfig Database { get; private set; }

        // Save data

        public static SettingsSaveData settingsSaveData { get; private set; }
        public static GameSaveData gameSaveData { get; private set; }

        // Events

        public static event Action<bool> OnGamePausedChanged;

        // App

        private void Awake()
        {
            DontDestroyOnLoad(this);

            CoroutineManager = CoroutineManager.Create();
            CoroutineManager.transform.parent = transform;

            Async.Delay(Time.deltaTime, () =>
            {
                Database = Resources.Load("Database") as DatabaseConfig;

                SetupSaveData();
                SetupManagers();

                UnityEngine.SceneManagement.SceneManager.activeSceneChanged += (oldScene, newScene) =>
                {
                    if (oldScene == newScene)
                        return;

                    Time.timeScale = 1;
                    SetupSceneManager();
                };
            });
        }

        private void SetupSaveData() 
        {
            DataManager.Init();
            settingsSaveData = DataManager.RetrieveOrCreateData<SettingsSaveData>();
            gameSaveData = DataManager.RetrieveOrCreateData<GameSaveData>();
        }

        private void SetupManagers() 
        {
            CinemachineManager = GetComponentInChildren<CinemachineManager>();
            PostProcessingManager = GetComponentInChildren<PostProcessingManager>();
            SoundManager = GetComponentInChildren<SoundManager>();

            SoundManager.Init();
            PostProcessingManager.Init();

            SetupSceneManager();
        }

        private void SetupSceneManager()
        {
            var newSceneManager = FindObjectOfType<Scene>();

            if (newSceneManager != null)
            {
                Scene = newSceneManager;

                Scene.Init();
            } 
        }

        private void Update()
        {
            if (Scene)
            {
                Scene.Process(Time.deltaTime);
            }
        }

        public static void PauseGame()
        {
            Time.timeScale = 0;

            OnGamePausedChanged?.Invoke(Paused);
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1;

            OnGamePausedChanged?.Invoke(Paused);
        }

        public static void TogglePause()
        {
            if (!Paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
}
