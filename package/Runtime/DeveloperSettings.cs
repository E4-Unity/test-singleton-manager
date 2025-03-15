using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Eu4ng.Manager.Singleton
{
    public abstract class DeveloperSettings<T> : ScriptableObject where T : DeveloperSettings<T>
    {
        private const string RESOURCES_PATH = "Assets/Resources";
        private const string SETTINGS_PATH = "DeveloperSettings";

        private static T s_Instance;

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                return s_Instance ?? LoadScriptableObject() ?? CreateScriptableObject();
#else
                return s_Instance ?? LoadScriptableObject();
#endif
            }
        }

        private static string GetDirectory(string path)
        {
            string directory = string.Empty;
            string[] folders = path.Split('/');
            foreach (string folder in folders)
            {
                directory = Path.Combine(directory, folder);
            }

            return directory;
        }

        private static T LoadScriptableObject()
        {
            string directory = GetDirectory(SETTINGS_PATH);

            s_Instance = Resources.Load<T>(Path.Combine(directory, typeof(T).Name));
            if (s_Instance != null) LogSingletonManager.Log(typeof(T).Name + " is loaded.");

            return s_Instance;
        }

#if UNITY_EDITOR
        private static T CreateScriptableObject()
        {
            // 폴더 생성
            string directory = CreateDirectory(RESOURCES_PATH + "/" + SETTINGS_PATH);

            // 스크립터블 오브젝트 생성
            s_Instance = CreateInstance<T>();
            AssetDatabase.CreateAsset(s_Instance, Path.Combine(directory, typeof(T).Name + ".asset"));
            AssetDatabase.SaveAssets();

            LogSingletonManager.Log(typeof(T).Name + " is created.");

            return s_Instance;
        }

        private static string CreateDirectory(string path)
        {
            string directory = string.Empty;
            string[] folders = path.Split('/');
            foreach (string folder in folders)
            {
                directory = Path.Combine(directory, folder);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    LogSingletonManager.Log("Directory(" + directory + ") is created.");
                }
            }

            return directory;
        }
#endif
    }
}
