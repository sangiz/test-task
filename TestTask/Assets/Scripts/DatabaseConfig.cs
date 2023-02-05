using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IgnSDK
{
    [CreateAssetMenu(menuName = "IgnSDK/AppConfig", fileName = "AppConfig")]
    public class DatabaseConfig : ScriptableObject
    {
        [Header("SCENES")]
        public List<SceneWrapper> scenes;

        [Header("AUDIO")]
        public AudioDB audioDB;
    }

    [Serializable]
    public struct SceneWrapper
    {
        public int id;
        public AssetReference sceneRef;
    }
}
