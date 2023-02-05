using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IgnSDK
{
    public static class DataManager
    {
        private static readonly Dictionary<Type, object> dataRegistry = new Dictionary<Type, object>();

        private static readonly string EDITOR_SAVE_PATH = Path.Combine(Application.dataPath, "Saves");


        public static void Init()
        {
#if UNITY_EDITOR
            if (Directory.Exists(EDITOR_SAVE_PATH) == false)
            {
                PlayerPrefs.DeleteAll();
                Directory.CreateDirectory(EDITOR_SAVE_PATH);
            }
#endif
        }

        private static void SaveRawData<T>(T data, Type key)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key.Name, json);
            PlayerPrefs.Save();

#if UNITY_EDITOR
            File.WriteAllText(GetEditorSavePath(key.Name), json);
#endif
        }

        private static void DeleteRawData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
            }

#if UNITY_EDITOR
            if (File.Exists(GetEditorSavePath(key)))
            {
                File.Delete(GetEditorSavePath(key));
            }
#endif
        }

        private static string GetEditorSavePath(string key)
        {
            return Path.Combine(EDITOR_SAVE_PATH, key + ".json");
        }

        private static void RegisterData<T>(T data)
        {
            dataRegistry.Add(data.GetType(), data);
            SaveData(data);
        }
        
        private static T LoadRawData<T>() where T : class, new()
        {
            string dataKey = typeof(T).Name;

            string json = "";

            if (HasData<T>())
            {
                json = PlayerPrefs.GetString(dataKey);
            }

#if UNITY_EDITOR
            if (File.Exists(GetEditorSavePath(dataKey)))
            {
                json = File.ReadAllText(GetEditorSavePath(dataKey));
            }
            else
            {
                return new T();
            }
#endif
            T data = JsonUtility.FromJson<T>(json);

            if (data != null)
            {
                return data;
            }

            return new T();
        }

        private static bool HasData<T>()
        {
            return PlayerPrefs.HasKey(typeof(T).Name);
        }

        public static void SaveData<T>(T data)
        {
            SaveRawData(data, data.GetType());
        }

        public static T RetrieveOrCreateData<T>() where T : class, new()
        {
            if (dataRegistry.TryGetValue(typeof(T), out object data) == false)
            {
                // Registry doesn't contain such data, load raw data and register instead
                data = LoadRawData<T>();
                RegisterData(data);
            }

            return data as T;
        }

        public static void DeleteData<T>() where T : class, new()
        {
            DeleteRawData(typeof(T).Name);
        }
    }
}
