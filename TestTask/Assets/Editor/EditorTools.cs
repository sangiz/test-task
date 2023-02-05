using TMPro;
using UnityEditor;
using UnityEngine;

namespace IgnSDK.Editor
{
    [CreateAssetMenu(fileName = "EditorTools", menuName = "IgnSDK/EditorTools")]
    public class EditorTools : ScriptableObject
    {
        private static EditorTools editorTools;

        public TMP_FontAsset TMPText;
        public Font editorFont;
        public Texture2D editorBackground;
        public Sprite arrowUp;

        public EditorColor[] editorColors;

        public static Color Color(string name)
        {
            foreach (var t in Inst.editorColors)
            {
                if (t.colorName == name)
                    return t.color;
            }

            return UnityEngine.Color.white;
        }

        public static EditorTools Inst
        {
            get
            {
                if (editorTools == null)
                    editorTools = (EditorTools)AssetDatabase.LoadAssetAtPath("Assets/Configs/EditorTools.asset", typeof(EditorTools));

                return editorTools;
            }
        }
    }

    [System.Serializable]
    public class EditorColor
    {
        public string colorName;
        public Color color;
    }
}
