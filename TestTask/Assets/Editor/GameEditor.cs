using UnityEditor;
using UnityEngine;
using static IgnSDK.Editor.IgnEditor;

namespace IgnSDK
{
    public class GameEditor : EditorWindow
    {
		private static GameEditor currentWindow;

		//[MenuItem("Window/Game/Editor")]
  //      public static void ShowWindow()
  //      {
  //          GetWindow<GameEditor>("Game Editor");
  //      }

		[MenuItem(itemName: "Game Editor", menuItem = "Game/Game Editor")]
		private static void Init()
		{
			currentWindow = GetWindow<GameEditor>();
			currentWindow.titleContent = new GUIContent("Game Editor");
			currentWindow.position = new Rect(0, 0, 600, 600);
		}

		private void OnGUI()
		{
			if (currentWindow == null)
			{
				currentWindow = GetWindow<GameEditor>();
			}

			SaveDataGUI();
			SeparatorLine(10);

			if (Application.isPlaying == false)
			{
				EditorGUILayout.HelpBox("Game is not running", MessageType.Info);
				return;
			}	
		}

		// Save data

		private void SaveDataGUI()
		{
			HeaderLabel($"GAME SAVE");

			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Delete SETTINGS data"))
			{
				if (EditorUtility.DisplayDialog("Confirm", "DELETE SETTINGS DATA?", "Yes", "No"))
				{
					DataManager.DeleteData<SettingsSaveData>();
					Debug.Log("All settings data has been deleted");
				}
			}

			if (GUILayout.Button("Delete GAME data"))
			{
				if (EditorUtility.DisplayDialog("Confirm", "DELETE GAME DATA?", "Yes", "No"))
				{
					DataManager.DeleteData<GameSaveData>();
					Debug.Log("All game data has been deleted");
				}
			}

			EditorGUILayout.EndHorizontal();
		}
	}
}
