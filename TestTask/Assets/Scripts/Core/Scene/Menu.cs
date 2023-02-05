using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class Menu : Scene
    {
        public static Menu Current { get; private set; }

        // Serialized Fields

        [SF] private Transform cameraTarget;

        // MenuSceneManager

        protected override void OnInit()
        {
            Current = this;

            App.CinemachineManager.SetActiveCamera(VirtualCamera.Menu);

            App.Database.audioDB.mainMenuMusic.Play2D();
        }
    }
}
