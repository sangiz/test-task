using DG.Tweening;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace IgnSDK
{
    public class Scene : MonoBehaviour
    {
        // Core modules that is unique to each scene

        public InputManager InputManager { get; private set; }
        public UIManager UIManager { get; private set; }

        // Properties

        public float LoadingProgress { get; private set; }

        // Private Fields
        private static bool gameReady = false;

        private AsyncOperationHandle<SceneInstance> asyncOperationHandle;
        private Tween loadingTween;
        private bool switchingScene = false;

        // Events

        public event Action OnSceneLoadingStart;
        public event Action OnSceneLoadingCompleted;

        // Scene

        public static void Ready(Action done)
        {
            Async.WaitUntil(() => gameReady, done);
        }

        public void Init()
        {
            InputManager = GetComponentInChildren<InputManager>();
            UIManager = GetComponentInChildren<UIManager>();
            UIManager.Init();

            Ready(() =>
            {
                

                var scene = SceneManager.GetActiveScene();

                if (scene.buildIndex == Constants.INTRO_SCENE)
                {
                    LoadScene(Constants.MAIN_SCENE);
                }
            });
          
            App.CinemachineManager.Setup();

            OnInit();

            gameReady = true;
        }

        protected virtual void OnInit()
        {
            
        }

        public virtual void Process(float delta)
        {
            if (switchingScene)
                return;

            UIManager.Process(delta);
        }

        public void LoadScene(int index) 
        {
            var scene = App.Database.scenes.FirstOrDefault(m => m.id == index);

            LoadSceneAsync(scene.sceneRef, OnSceneLoadingDone);

        }

        private void OnSceneLoadingDone()
        {
            if (asyncOperationHandle.IsValid())
            {
                var res = asyncOperationHandle.Result.ActivateAsync();

                res.completed += (res) =>
                {
                    switchingScene = false;

                    App.SoundManager.SetPaused(false);
                    OnSceneLoadingCompleted?.Invoke();
                };     
            }
        }

        private IEnumerator SceneLoadCoroutine(Action onCompleted)
        {
            App.SoundManager.StopAll();
            UIManager.OpenDialog<LoadingDialog>();

            switchingScene = true;
            LoadingProgress = 0;

            yield return new WaitForSecondsRealtime(0.1f);

            while (LoadingProgress != 1)
            {
                var progress = asyncOperationHandle.PercentComplete;

                if (progress > LoadingProgress && loadingTween == null)
                {
                    // Failsafe to load in 0.5f or slower
                    var duration = (progress - LoadingProgress) / 2;
                    loadingTween = DOTween.To(() => LoadingProgress, x => LoadingProgress = x, progress, duration).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                    {
                        loadingTween = null;
                    });
                }

                yield return null;
            }

            yield return new WaitForSecondsRealtime(0.1f);

            onCompleted?.Invoke();
        }

        private void LoadSceneAsync(AssetReference sceneRef, Action done = null)
        {
            OnSceneLoadingStart?.Invoke();

            LoadingProgress = 0;

            if (sceneRef.IsValid())
            {
                Addressables.Release(asyncOperationHandle);
            }

            asyncOperationHandle = sceneRef.LoadSceneAsync(LoadSceneMode.Single, false);
            App.CoroutineManager.Begin(SceneLoadCoroutine(done));
        }
    }
}
