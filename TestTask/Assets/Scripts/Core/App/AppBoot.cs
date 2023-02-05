using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    [CreateAssetMenu(fileName = "AppBoot", menuName = "IgnSDK/AppBoot")]
    public class AppBoot : ScriptableObject
    {
        [SF] private App app;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BootUp()
        {
            //! This makes sure that every scene have App script

            var appBoot = Resources.Load("AppBoot") as AppBoot;

            if (appBoot == null)
            {
                Debug.LogError($"No AppBoot found! Please create one in Resources root and attach App object to it!");
                return;
            }

            Instantiate(appBoot.app);

        }
    }
}