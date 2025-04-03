using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using Eu4ng.Utilities.Editor;
#endif

namespace Eu4ng.Manager.Singleton
{
    public abstract class DeveloperSettings : ScriptableObject
    {
        protected const string RESOURCES_PATH = "Assets/Resources";
        protected const string SETTINGS_PATH = "DeveloperSettings";

        protected bool IsInitialized { get; set; }

        protected void Initialize()
        {
            if (IsInitialized) return;

            OnInitialize();
        }

        protected abstract void OnInitialize();
    }

    public abstract class DeveloperSettings<T> : DeveloperSettings where T : DeveloperSettings<T>
    {
        static T s_Instance;

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

        static T LoadScriptableObject()
        {
            s_Instance = Resources.Load<T>(Path.Combine(SETTINGS_PATH, typeof(T).Name));
            if (s_Instance != null)
            {
                LogSingletonManager.Log(typeof(T).Name + " is loaded.");

                s_Instance.Initialize();
            }

            return s_Instance;
        }

#if UNITY_EDITOR
        static T CreateScriptableObject()
        {
            // 폴더 생성
            string directory = DirectoryManager.CreateDirectory(RESOURCES_PATH + "/" + SETTINGS_PATH);

            // 스크립터블 오브젝트 생성
            s_Instance = CreateInstance<T>();
            AssetDatabase.CreateAsset(s_Instance, Path.Combine(directory, typeof(T).Name + ".asset"));
            AssetDatabase.SaveAssets();

            LogSingletonManager.Log(typeof(T).Name + " is created.");

            s_Instance.Initialize();

            return s_Instance;
        }
#endif
    }
}
