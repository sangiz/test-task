using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IgnSDK.Editor
{
    public static class IgnEditor
    {
        /// <summary>
        /// Wrapper for AttributeColor in the editor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color Color(AttributeColor color)
        {
            switch (color)
            {
                case AttributeColor.Black:
                    return UnityEngine.Color.black;
                case AttributeColor.White:
                    return UnityEngine.Color.white;
                case AttributeColor.Green:
                    return UnityEngine.Color.green;
                case AttributeColor.Orange:
                    return new Color(1f, 0.52f, 0.08f);
            }

            return UnityEngine.Color.magenta;
        }

        /// <summary>
        /// Default editor text with rich text enabled style
        /// </summary>
        /// <param name="textSize"></param>
        /// <returns></returns>
        public static GUIStyle EditorText(int textSize = 10)
        {
            GUIStyle nStyle =
                new GUIStyle()
                {
                    fontSize = textSize,
                    normal = new GUIStyleState()
                    {
                        textColor = UnityEngine.Color.white
                    },
                    richText = true
                };
            return nStyle;
        }

        /// <summary>
        /// Usually used for labels but has more variations
        /// </summary>
        /// <param name="textAnchor"></param>
        /// <param name="textColor"></param>
        /// <param name="fontSize"></param>
        /// <param name="bgEnabled"></param>
        /// <returns></returns>
        public static GUIStyle EditorStyle(
            TextAnchor textAnchor = TextAnchor.MiddleLeft,
            AttributeColor textColor = AttributeColor.White,
            int fontSize = 14,
            bool bgEnabled = false)
        {
            GUIStyle nStyle =
                new GUIStyle()
                {
                    normal = new GUIStyleState()
                    {
                        background = bgEnabled ? EditorTools.Inst.editorBackground : null,
                        textColor = Color(textColor)
                    },
                    alignment = textAnchor,
                    fontSize = fontSize,
                    richText = true
                };

            if (bgEnabled)
            {
                bool leftAnchor = textAnchor.ToString().Contains("Left");
                nStyle.normal.background.wrapMode = TextureWrapMode.Clamp;
                nStyle.border = new RectOffset(20, 20, 20, 20);
                nStyle.padding = new RectOffset(leftAnchor ? 10 : 0, 0, 0, 4);
            }

            return nStyle;
        }

        public static string SearchBar(ref string searchString)
        {
            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            searchString = GUILayout.TextField(searchString, GUI.skin.FindStyle("ToolbarSeachTextField"));
            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                // Remove focus if cleared
                searchString = "";
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();

            return searchString;
        }

        public static GUIStyle EditorStyleSimple(
            TextAnchor textAnchor = TextAnchor.MiddleLeft,
            AttributeColor textColor = AttributeColor.White,
            int fontSize = 14)
        {
            GUIStyle nStyle =
                new GUIStyle()
                {
                    normal = new GUIStyleState()
                    {
                        background = null,
                        textColor = Color(textColor)
                    },
                    alignment = textAnchor,
                    fontSize = fontSize,
                    richText = true
                };

            return nStyle;
        }

        /// <summary>
        /// Creates GUIContent Icon with Name
        /// </summary>
        /// <param name="text">Preferred text</param>
        /// <param name="iconId">https://github.com/halak/unity-editor-icons</param>
        /// <param name="toolTip">Optional hover tooltip</param>
        /// <returns>Complete GUIContent</returns>
        public static GUIContent CreateNamedIcon(string text, string iconId, string toolTip = null)
        {
            var icon = new GUIContent();
            if (string.IsNullOrEmpty(iconId) == false)
            {
                icon = new GUIContent(EditorGUIUtility.IconContent($"{iconId}"));
            }

            return new GUIContent($"{text}", icon.image, $"{toolTip}");
        }

        public static void DrawHorizontalLine(Color color, int thickness = 1, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        public static void DrawVerticalLine(Color color, int thickness = 1, int padding = 10, int length = 100)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(padding + thickness), GUILayout.Height(length));
            r.width = thickness;
            r.x += padding / 2;
            r.height -= 10;
            EditorGUI.DrawRect(r, color);
        }

        public static void SeparatorLine(float space = 0f)
        {
            GUIStyle nStyle =
                new GUIStyle()
                {
                    normal = new GUIStyleState()
                    {
                        background = EditorTools.Inst.editorBackground,
                    },
                    border = new RectOffset(0, 0, 0, 0),
                    padding = new RectOffset(0, 0, 0, 0)
                };
            nStyle.normal.background.wrapMode = TextureWrapMode.Clamp;

            EditorGUILayout.Space(space);
            EditorGUILayout.LabelField(string.Empty, nStyle, GUILayout.Height(1));
            EditorGUILayout.Space(space);
        }

        /// <summary>
        /// Standart header label / clickable
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <returns></returns>
        public static bool HeaderLabel(
            string text,
            AttributeColor textColor = AttributeColor.White,
            TextAnchor textAnchor = TextAnchor.MiddleCenter, float height = 35f, int fontSize = 20)
        {
            EditorGUILayout.BeginHorizontal("helpBox");
            var button = GUILayout.Button(text, EditorStyle(textAnchor,
                    textColor,
                    fontSize,
                    false),
                GUILayout.Height(height));
            EditorGUILayout.EndHorizontal();

            return button;
        }

        // Text

        /// <summary>
        /// Auto add outline and shadow to our texts
        /// </summary>
        /// <param name="command"></param>
        [MenuItem("CONTEXT/Text/Add Outline + Shadow")]
        public static void AddTextVisuals(MenuCommand command)
        {
            var text = (Text)command.context;
            var outline = text.gameObject.AddComponent<Outline>();
            var shadow = text.gameObject.AddComponent<Shadow>();
            outline.effectColor = UnityEngine.Color.black;
            shadow.effectColor = UnityEngine.Color.black;
            shadow.effectDistance = new Vector2(0f, -1.45f);
        }

        [MenuItem("CONTEXT/TMP_Text/Setup Text")]
        public static void AddTMPVisuals(MenuCommand command)
        {
            var text = (TMP_Text)command.context;
            text.font = EditorTools.Inst.TMPText;
            text.alignment = TextAlignmentOptions.Midline;
            text.alignment = TextAlignmentOptions.Center;
        }

        // GameObject

        [MenuItem("GameObject/UI/Create Button", false, 16)]
        public static void CreateButton()
        {
            var selectedGameObject = Selection.activeTransform;

            var newGameObject = new GameObject();
            newGameObject.name = "Button";
            newGameObject.AddComponent<Button>();
            newGameObject.transform.SetParent(selectedGameObject);
            newGameObject.transform.localScale = Vector3.one;
            var rect = newGameObject.GetComponent<RectTransform>();
            rect.anchoredPosition3D = Vector3.zero;
            Selection.activeObject = newGameObject;
            EditorUtility.SetDirty(newGameObject);
        }

        // Other

        /// <summary>
        /// Toggle AnimBool extension
        /// </summary>
        /// <param name="aBool"></param>
        public static void Toggle(this AnimBool aBool)
        {
            aBool.target = !aBool.target;
        }

        /// <summary>
        /// Create an animation bool and attach Repaint callback to it
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        public static AnimBool CreateAnimBool(EditorWindow editor)
        {
            var aBool = new AnimBool();
            aBool.valueChanged.AddListener(editor.Repaint);
            return aBool;
        }

        // GUI

        /// <summary>
        /// Draw a gui tab with a clickable animated label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="animBool"></param>
        /// <param name="content"></param>
        public static void DrawGUI(string label, ref AnimBool animBool, Action content)
        {
            GUI.color = animBool.value ? UnityEngine.Color.yellow : UnityEngine.Color.white;
            if (HeaderLabel(label))
                animBool.Toggle();
            GUI.color = UnityEngine.Color.white;

            if (EditorGUILayout.BeginFadeGroup(animBool.faded))
            {
                EditorGUILayout.BeginVertical("box");
                content();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(10);
            }

            EditorGUILayout.EndFadeGroup();
        }

        public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        //Scene view
        public static void FocusObject(GameObject o)
        {
            Selection.SetActiveObjectWithContext(o, o);
            SceneView.lastActiveSceneView.FrameSelected();
        }

        //Serialized
        public static object GetValue(this SerializedProperty p)
        {
            switch (p.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return (object)p.intValue;
                case SerializedPropertyType.Boolean:
                    return (object)p.boolValue;
                case SerializedPropertyType.Float:
                    return (object)p.floatValue;
                case SerializedPropertyType.String:
                    return (object)p.stringValue;
                case SerializedPropertyType.Color:
                    return (object)p.colorValue;
                case SerializedPropertyType.ObjectReference:
                    return (object)p.objectReferenceValue;
                case SerializedPropertyType.LayerMask:
                    return (object)p.intValue;
                case SerializedPropertyType.Enum:
                    return (object)p.enumValueIndex;
                case SerializedPropertyType.Vector2:
                    return (object)p.vector2Value;
                case SerializedPropertyType.Vector3:
                    return (object)p.vector3Value;
                case SerializedPropertyType.Vector4:
                    return (object)p.vector4Value;
                case SerializedPropertyType.Rect:
                    return (object)p.rectValue;
                case SerializedPropertyType.ArraySize:
                    return (object)p.intValue;
                case SerializedPropertyType.Character:
                    return (object)p.stringValue;
                case SerializedPropertyType.AnimationCurve:
                    return (object)p.animationCurveValue;
                case SerializedPropertyType.Bounds:
                    return (object)p.boundsValue;
                case SerializedPropertyType.Quaternion:
                    return (object)p.quaternionValue;
                default:
                    return (object)0;
            }
        }

        public static List<SerializedProperty> GetSerializedFieldsRecursively(SerializedObject serializedObject)
        {
            var allProperties = new List<SerializedProperty>();

            var sObject = new SerializedObject(serializedObject.targetObject);
            var objectIterator = sObject.GetIterator().Copy();

            while (objectIterator.NextVisible(true))
            {
                allProperties.Add(objectIterator.Copy());
            }

            return allProperties;
        }
    }
    public enum AttributeColor
    {
        White,
        Black,
        Green,
        Orange
    }
}
