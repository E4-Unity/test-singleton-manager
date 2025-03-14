using System.Collections.Generic;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Eu4ng.Manager.Singleton
{
    public abstract class DeveloperSettings<T> : ScriptableObject where T : DeveloperSettings<T>
    {
        static readonly List<string> s_PathHierarchy = new List<string>()
        {
            "Assets",
            "Resources",
            "DeveloperSettings"
        };

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

        private static T LoadScriptableObject()
        {
            T instance = Resources.Load<T>("");
            if(instance != null) LogSingletonManager.Log(typeof(T).Name + " is loaded.");

            return instance;
        }

#if UNITY_EDITOR
        private static T CreateScriptableObject()
        {
            // 폴더 생성
            string directory = string.Empty;
            foreach (string folder in s_PathHierarchy)
            {
                directory = Path.Combine(directory, folder);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    LogSingletonManager.Log("Directory(" + directory + ") is created.");
                }
            }

            // 스크립터블 오브젝트 생성
            s_Instance = CreateInstance<T>();
            AssetDatabase.CreateAsset(s_Instance, Path.Combine(directory, typeof(T).Name + ".asset"));
            AssetDatabase.SaveAssets();

            LogSingletonManager.Log(typeof(T).Name + " is created.");

            return s_Instance;
        }
#endif
    }
}
