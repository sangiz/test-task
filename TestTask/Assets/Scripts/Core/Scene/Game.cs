using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class Game : Scene
    {
        public static Game Current { get; private set; }

        [SF] private Transform previewCube;

        // GameSceneManager

        protected override void OnInit()
        {
            Current = this;

            InputManager.OnMenuAction += OnPauseClick;
            InputManager.OnCancelAction += OnPauseClick;

            App.CinemachineManager.GetCamera(VirtualCamera.GamePlay).Follow = previewCube;
        }

        private void OnPauseClick() 
        {
            App.UIManager.OpenDialog<PauseDialog>();
        }
    }
}
